namespace OfferExporter.Services;

public class ProductService
{
    public List<Product> BuildProducts(List<GetAllOffersResultRow> rows)
    {
        var products = new List<Product>();

        for (var m = 0; m < rows.Count; m++)
        {
            var row = rows[m];

            var product = new Product(row.ProductPrid, row.ReferentialId, row.ReferentialName);

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