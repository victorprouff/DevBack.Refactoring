namespace OfferExporter.Services;

public class ProductService
{
    public List<Product> BuildProducts(List<GetAllOffersResultRow> rows)
    {
        var products = new List<Product>();

        // var products = rows.Distinct().Select(CreateProduct).ToList();

        // foreach (var row in rows.Distinct())
        // {
        //     var product = CreateProduct(row);
        //
        //     products.Add(product);
        // }

        foreach (var t in rows.Distinct())
        {
            var product = CreateProduct(t);

            // 
            var found = products.FirstOrDefault(p => product.Equals(p));
            if (found is not null)
            {
                continue;
            }

            products.Add(product);
        }

        products.Add(products.Last());

        // Quick-fix: Sort the products and offers (so the generate file is always the same)
        products = products
            .Select(product =>
            {
                product.Offers = product.Offers
                    .OrderBy(offer => offer.Id)
                    .ToList();
                return product;
            })
            .OrderBy(product => product.ReferentialId)
            .ThenBy(product => product.Prid)
            .ToList();

        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Convert data into products: {products.Count} products");

        return products;
    }

    private static Product CreateProduct(GetAllOffersResultRow row)
    {
        var product = new Product
        {
            Prid = row.ProductPrid,
            ReferentialId = row.ReferentialId,
            ReferentialName = row.ReferentialName
        };

        product.Offers.Add(new Offer
        {
            Id = row.OfferId,
            Company = row.SellerName,
            Price = row.OfferPrice,
            ReducedPrice = row.PromotionReducedPrice,
            Quantity = row.OfferQuantity,
            DiscountFor = row.PromotionTargetName
        });
        return product;
    }
}