using FluentAssertions;

namespace OfferExporter.Tests.Services.ProductService;

public class BuildProductsShould
{
    [Fact]
    public void ReturnOneValidProduct()
    {
        var service = new OfferExporter.Services.ProductService();
        var rows = new List<GetAllOffersResultRow>
        {
            new()
            {
                ProductPrid = 2515866, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 1499665, OfferIsActive = true, OfferPrice = 20.04M,
                OfferQuantity = 10, SellerId = 284, SellerName = "Inandout_Dist", SellerIsActive = true, PromotionId = 775, PromotionReducedPrice = 18.04M, PromotionTargetId = 1,
                PromotionTargetName = "Public"
            }
        };

        var expectedProducts = new List<Product>
        {
            new()
            {
                Prid = 2515866,
                ReferentialId = 1,
                ReferentialName = "Fnac",
                Offers = new List<Offer> { new() { Id = 1499665, Company = "Inandout_Dist", Price = 20.04M, ReducedPrice = 18.04M, DiscountFor = "Public", Quantity = 10 } }
            },
            new()
            {
                Prid = 2515866,
                ReferentialId = 1,
                ReferentialName = "Fnac",
                Offers = new List<Offer> { new() { Id = 1499665, Company = "Inandout_Dist", Price = 20.04M, ReducedPrice = 18.04M, DiscountFor = "Public", Quantity = 10 } }
            }
        };
        var result = service.BuildProducts(rows);
        result.Should().BeEquivalentTo(expectedProducts);
    }

    [Fact]
    public void ReturnValidProducts()
    {
        var service = new OfferExporter.Services.ProductService();
        var rows = new List<GetAllOffersResultRow>
        {
            new()
            {
                ProductPrid = 2515866, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 1499665, OfferIsActive = true, OfferPrice = 20.04M,
                OfferQuantity = 10, SellerId = 284, SellerName = "Inandout_Dist", SellerIsActive = true, PromotionId = 775, PromotionReducedPrice = 18.04M, PromotionTargetId = 1,
                PromotionTargetName = "Public"
            },
            new()
            {
                ProductPrid = 2515865, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 1499666, OfferIsActive = true, OfferPrice = 20.04M,
                OfferQuantity = 10, SellerId = 284, SellerName = "Inandout_Dist", SellerIsActive = true, PromotionId = 775, PromotionReducedPrice = null, PromotionTargetId = 1,
                PromotionTargetName = "Public"
            },
            new()
            {
                ProductPrid = 2611403, ReferentialId = 3, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 2776585, OfferIsActive = true, OfferPrice = 10,
                OfferQuantity = 10, SellerId = 3724, SellerName = "MyUnivers", SellerIsActive = true, PromotionId = 32, PromotionReducedPrice = 18.04M, PromotionTargetId = 1,
                PromotionTargetName = "Public"
            }
        };

        var expectedProducts = new List<Product>
        {
            new()
            {
                Prid = 2515866,
                ReferentialId = 1,
                ReferentialName = "Fnac",
                Offers = new List<Offer> { new() { Id = 1499665, Company = "Inandout_Dist", Price = 20.04M, ReducedPrice = 18.04M, DiscountFor = "Public", Quantity = 10 } }
            },
            new()
            {
                Prid = 2515865,
                ReferentialId = 1,
                ReferentialName = "Fnac",
                Offers = new List<Offer> { new() { Id = 1499666, Company = "Inandout_Dist", Price = 20.04M, ReducedPrice = null, DiscountFor = "Public", Quantity = 10 } }
            },
            new()
            {
                Prid = 2611403,
                ReferentialId = 3,
                ReferentialName = "Fnac",
                Offers = new List<Offer> { new() { Id = 2776585, Company = "MyUnivers", Price = 10, ReducedPrice = 18.04M, DiscountFor = "Public", Quantity = 10 } }
            },
            new()
            {
                Prid = 2611403,
                ReferentialId = 3,
                ReferentialName = "Fnac",
                Offers = new List<Offer> { new() { Id = 2776585, Company = "MyUnivers", Price = 10, ReducedPrice = 18.04M, DiscountFor = "Public", Quantity = 10 } }
            }
        };
        var result = service.BuildProducts(rows);
        result.Should().BeEquivalentTo(expectedProducts);
    }
}