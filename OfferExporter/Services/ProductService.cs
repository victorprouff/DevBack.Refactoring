namespace OfferExporter.Services;

public class ProductService
{
    public List<Product> BuildProducts(List<GetAllOffersResultRow> rows)
    {
        /* var products = new List<Product>();

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
             if (products.Exists(p => p.Equals(product)))
             {
                 continue;
             }

             products.Add(product);
         }

         products.Add(products.Last()); */

        var products = new List<Product>();

        for (int m = 0; m < rows.Count; m++)
        {
            var row = rows[m];

            var product = new Product
            {
                Prid = row.ProductPrid,
                ReferentialId = row.ReferentialId,
                ReferentialName = row.ReferentialName
            };

            // 
            var found = products.FirstOrDefault(p => product.Equals(p));
            if (found is not null)
            {
                product = found;
            }
            else
            {
                products.Add(product);
            }

            product.Offers.Add(new Offer
            {
                Id = row.OfferId,
                Company = row.SellerName,
                Price = row.OfferPrice,
                ReducedPrice = row.PromotionReducedPrice,
                Quantity = row.OfferQuantity,
                DiscountFor = row.PromotionTargetName
            });

            // 
            if (m == rows.Count - 1)
            {
                products.Add(product);
            }
        }

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
}