namespace OfferExporter.Services;

public class ProductService
{
    public List<Product> BuildProducts(List<GetAllOffersResultRow> rows)
    {
        var products = new List<Product>();

        foreach (var p in rows.GroupBy(r => new { r.ProductPrid, r.ReferentialId }))
        {
            Product? product = null;

            foreach (var row in p)
            {
                product ??= new Product(row.ProductPrid, row.ReferentialId, row.ReferentialName);

                product.Offers.Add(new Offer
                {
                    Id = row.OfferId,
                    Company = row.SellerName,
                    Price = row.OfferPrice,
                    ReducedPrice = row.PromotionReducedPrice,
                    Quantity = row.OfferQuantity,
                    DiscountFor = row.PromotionTargetName
                });
            }

            if (product != null)
            {
                products.Add(product);
            }
        }

        if (products.Count > 0)
        {
            products.Add(products.Last());
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