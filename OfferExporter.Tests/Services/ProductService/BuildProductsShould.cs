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
            new(
                2515866,
                1,
                "Fnac",
                true,
                1499665,
                true,
                20.04M,
                10,
                284,
                "Inandout_Dist",
                true,
                775,
                18.04M,
                1,
                "Public")
        };

        var expectedProducts = new List<Product>
        {
            new(2515866, 1, "Fnac")
            {
                Offers = new List<Offer> { new() { Id = 1499665, Company = "Inandout_Dist", Price = 20.04M, ReducedPrice = 18.04M, DiscountFor = "Public", Quantity = 10 } }
            },
            new(2515866, 1, "Fnac")
            {
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
            new(2515866,
                1,
                "Fnac",
                true,
                1499665,
                true,
                20.04M,
                10,
                284,
                "Inandout_Dist",
                true,
                775,
                18.04M,
                1,
                "Public"),
            new(
                2515865,
                1,
                "Fnac",
                true,
                1499666,
                true,
                20.04M,
                10,
                284,
                "Inandout_Dist",
                true,
                775,
                null,
                1,
                "Public"),
            new(
                2611403,
                3,
                "Fnac",
                true,
                2776585,
                true,
                10,
                10,
                3724,
                "MyUnivers",
                true,
                32,
                18.04M,
                1,
                "Public")
        };

        var expectedProducts = new List<Product>
        {
            new(2515866, 1, "Fnac")
            {
                Offers = new List<Offer> { new() { Id = 1499665, Company = "Inandout_Dist", Price = 20.04M, ReducedPrice = 18.04M, DiscountFor = "Public", Quantity = 10 } }
            },
            new(2515865, 1, "Fnac")
            {
                Offers = new List<Offer> { new() { Id = 1499666, Company = "Inandout_Dist", Price = 20.04M, ReducedPrice = null, DiscountFor = "Public", Quantity = 10 } }
            },
            new(2611403, 3, "Fnac")
            {
                Offers = new List<Offer> { new() { Id = 2776585, Company = "MyUnivers", Price = 10, ReducedPrice = 18.04M, DiscountFor = "Public", Quantity = 10 } }
            },
            new(2611403, 3, "Fnac")
            {
                Offers = new List<Offer> { new() { Id = 2776585, Company = "MyUnivers", Price = 10, ReducedPrice = 18.04M, DiscountFor = "Public", Quantity = 10 } }
            }
        };
        var result = service.BuildProducts(rows);
        result.Should().BeEquivalentTo(expectedProducts);
    }

    [Fact]
    public void ReturnValidProductsWithDuplicate()
    {
        var service = new OfferExporter.Services.ProductService();
        var rows = new List<GetAllOffersResultRow>
        {
            new(
                2515866,
                1,
                "Fnac",
                true,
                1499665,
                true,
                20.04M,
                10,
                284,
                "Inandout_Dist",
                true,
                775,
                18.04M,
                1,
                "Public"),
            new(
                2515865,
                1,
                "Fnac",
                true,
                1499666,
                true,
                20.04M,
                10,
                284,
                "Inandout_Dist",
                true,
                775,
                null,
                1,
                "Public"),
            new(
                2515865,
                1,
                "Fnac",
                true,
                1499667,
                true,
                25.04M,
                5,
                284,
                "Inandout_Dist",
                true,
                775,
                null,
                1,
                "Public"),
            new(
                2611403,
                3,
                "Fnac",
                true,
                2776585,
                true,
                10,
                10,
                3724,
                "MyUnivers",
                true,
                32,
                18.04M,
                1,
                "Public")
        };

        var expectedProducts = new List<Product>
        {
            new(2515866, 1, "Fnac")
            {
                Offers = new List<Offer> { new() { Id = 1499665, Company = "Inandout_Dist", Price = 20.04M, ReducedPrice = 18.04M, DiscountFor = "Public", Quantity = 10 } }
            },
            new(2515865, 1, "Fnac")
            {
                Offers = new List<Offer>
                {
                    new() { Id = 1499666, Company = "Inandout_Dist", Price = 20.04M, ReducedPrice = null, DiscountFor = "Public", Quantity = 10 },
                    new() { Id = 1499667, Company = "Inandout_Dist", Price = 25.04M, ReducedPrice = null, DiscountFor = "Public", Quantity = 5 }
                }
            },
            new(2611403, 3, "Fnac")
            {
                Offers = new List<Offer> { new() { Id = 2776585, Company = "MyUnivers", Price = 10, ReducedPrice = 18.04M, DiscountFor = "Public", Quantity = 10 } }
            },
            new(2611403, 3, "Fnac")
            {
                Offers = new List<Offer> { new() { Id = 2776585, Company = "MyUnivers", Price = 10, ReducedPrice = 18.04M, DiscountFor = "Public", Quantity = 10 } }
            }
        };
        var result = service.BuildProducts(rows);
        result.Should().BeEquivalentTo(expectedProducts);
    }
}