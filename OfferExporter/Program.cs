using OfferExporter;
using OfferExporter.Services;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

var rowService = new RowService();
var productService = new ProductService();

var stopwatch = Stopwatch.StartNew();

Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} OfferExport batch started");

var dbFilePath = FindDatabaseFile();
var rows = await ReadDataFromDatabse(dbFilePath);

var rowsValid = rowService.RemoveInvalidRows(rows);
var products = productService.BuildProducts(rowsValid);
var offersJson = ExportOffers(products);

CompressTheFiles(offersJson);
CalculateMd5Hash(offersJson);
CleanUp(offersJson);

static string FindDatabaseFile()
{
    var dirPath = Environment.CurrentDirectory;
    var dbFilePath = string.Empty;

    try
    {
        while (dirPath is not null && !File.Exists(dbFilePath))
        {
            var found = Directory.GetFiles(dirPath, "OFFERS.mdf", SearchOption.AllDirectories);
            if (found.Any())
            {
                dbFilePath = found.Single();
            }
            else
            {
                dirPath = Directory.GetParent(dirPath)?.FullName;
            }
        }
    }
    catch
    {
    }

    if (!File.Exists(dbFilePath))
    {
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Database OFFERS.mdf not found !");
    }

    return dbFilePath;
}

static async Task<List<GetAllOffersResultRow>> ReadDataFromDatabse(string dbFilePath)
{
    var connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename={dbFilePath}; Integrated Security=True;";
    var rows = new List<GetAllOffersResultRow>();

    try
    {
        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand("exec dbo.GetAllOffers", connection))
        {
            await connection.OpenAsync();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var row = new GetAllOffersResultRow(
                        reader.GetInt32(reader.GetOrdinal("ProductPrid")),
                        reader.GetByte(reader.GetOrdinal("ReferentialId")),
                        reader.GetString(reader.GetOrdinal("ReferentialName")),
                        reader.GetBoolean(reader.GetOrdinal("ReferentialIsExportable")),
                        reader.GetInt32(reader.GetOrdinal("OfferId")),
                        reader.GetBoolean(reader.GetOrdinal("OfferIsActive")),
                        reader.GetDecimal(reader.GetOrdinal("OfferPrice")),
                        reader.GetInt16(reader.GetOrdinal("OfferQuantity")),
                        reader.GetInt32(reader.GetOrdinal("SellerId")),
                        reader.GetString(reader.GetOrdinal("SellerName")),
                        reader.GetBoolean(reader.GetOrdinal("SellerIsActive")),
                        reader.IsDBNull("PromotionId") ? null : reader.GetInt32(reader.GetOrdinal("PromotionId")),
                        reader.IsDBNull("PromotionReducedPrice") ? null : reader.GetDecimal(reader.GetOrdinal("PromotionReducedPrice")),
                        reader.IsDBNull("PromotionTargetId") ? null : reader.GetByte(reader.GetOrdinal("PromotionTargetId")),
                        reader.IsDBNull("PromotionTargetName") ? null : reader.GetString(reader.GetOrdinal("PromotionTargetName")));
                    rows.Add(row);
                }
            }

            await connection.CloseAsync();
            await connection.DisposeAsync();
        }

        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Read offers from database: {rows.Count} found");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Read offers from database failed ! {ex.Message}");
    }

    return rows;
}

static List<string> ExportOffers(List<Product> products)
{
    var outputDir = Path.Combine(Environment.CurrentDirectory, "_OUPUT");
    var bytesOfNewLine = Encoding.UTF8.GetBytes(Environment.NewLine);
    var jsonFiles = new List<string>();

    Directory.CreateDirectory(outputDir);

    var referentials = products.GroupBy(product => product.ReferentialName);
    foreach (var referential in referentials)
    {
        try
        {
            var jsonFileName = $"{referential.Key}.json";
            var jsonFilePath = Path.Combine(outputDir, jsonFileName);
            var jsonBytes = new List<byte>();

            // The exported file is not exactly a pure JSON. It will contains many JSON.
            // Each line of the file will contain a product serialized into JSON.
            foreach (var product in referential)
            {
                jsonBytes.AddRange(Utf8Json.JsonSerializer.Serialize(product, Utf8Json.Resolvers.StandardResolver.ExcludeNull));
                jsonBytes.AddRange(bytesOfNewLine);
            }

            File.WriteAllBytes(jsonFilePath, jsonBytes.ToArray());

            jsonFiles.Add(jsonFilePath);

            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Export {referential.Key} referential: {jsonFileName} ({jsonBytes.Count} bytes)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Export {referential.Key} referential failed ! {ex.Message}");
        }
    }

    return jsonFiles;
}

static void CompressTheFiles(List<string> offersJson)
{
    foreach (var jsonFile in offersJson)
    {
        var jsonFileName = Path.GetFileName(jsonFile);
        var gZipFile = jsonFile + ".gz";

        try
        {
            using (var fileStream = new FileStream(gZipFile, FileMode.Create))
            using (var gZipStream = new GZipStream(fileStream, CompressionMode.Compress))
            {
                var gZipFileName = Path.GetFileName(gZipFile);
                var jsonBytes = File.ReadAllBytes(jsonFile);

                gZipStream.Write(jsonBytes, 0, jsonBytes.Length);

                var gZipFileSize = new FileInfo(gZipFile).Length;

                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Compress {jsonFileName}: {gZipFileName} ({gZipFileSize} bytes)");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Compress {jsonFileName} failed ! {ex.Message}");
        }
    }
}

static void CalculateMd5Hash(List<string> offersJson)
{
    var md5 = MD5.Create();
    var sb = new StringBuilder();

    foreach (var jsonFile in offersJson)
    {
        var jsonFileName = Path.GetFileName(jsonFile);
        var gZipFileName = jsonFileName + ".gz";
        var gZipFile = jsonFile + ".gz";
        var md5File = gZipFile + ".md5";

        try
        {
            var gZipBytes = File.ReadAllBytes(gZipFile);
            var hashBytes = md5.ComputeHash(gZipBytes);

            // Convert hashBytes into string then save the MD5 hash into file
            sb.Clear();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            File.WriteAllText(md5File, sb.ToString());

            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Compute MD5 {gZipFileName}: {sb}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Compute MD5 {gZipFileName} failed ! {ex.Message}");
        }
    }
}

static void CleanUp(List<string> offersJson)
{
    try
    {
        foreach (var jsonFile in offersJson)
        {
            var gZipFile = jsonFile + ".gz";
            if (!File.Exists(gZipFile))
            {
                continue;
            }

            if (gZipFile.Length == 0)
            {
                continue;
            }

            File.Delete(jsonFile);
        }

        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Clean up working files done");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Clean up working files failed ! {ex.Message}");
    }
}


// === //
// END //
// === //
stopwatch.Stop();
Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} OfferExport batch completed in {stopwatch.Elapsed}");

