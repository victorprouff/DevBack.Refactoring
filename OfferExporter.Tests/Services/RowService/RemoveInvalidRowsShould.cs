using FluentAssertions;

namespace OfferExporter.Tests.Services.RowService;

public class RemoveInvalidRowsShould
{
    [Fact]
    public void ReturnOfferIsActive()
    {
        var service = new OfferExporter.Services.RowService();

        var offersRow = new List<GetAllOffersResultRow>
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
                2611403,
                1,
                "Fnac",
                true,
                2776585,
                false,
                10.50M,
                10,
                3724,
                "MyUnivers",
                true,
                null,
                18.04M,
                1,
                "Public")
        };

        var expectedOfferRow = new List<GetAllOffersResultRow>
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

        var result = service.RemoveInvalidRows(offersRow);
        result.Should().BeEquivalentTo(expectedOfferRow);
    }

    [Fact]
    public void ReturnOfferPricePositive()
    {
        var service = new OfferExporter.Services.RowService();

        var offersRow = new List<GetAllOffersResultRow>
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
                2611403,
                1,
                "Fnac",
                true,
                2776585,
                false,
                0,
                10,
                3724,
                "MyUnivers",
                true,
                null,
                18.04M,
                1,
                "Public")
        };

        var expectedOfferRow = new List<GetAllOffersResultRow>
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

        var result = service.RemoveInvalidRows(offersRow);
        result.Should().BeEquivalentTo(expectedOfferRow);
    }

    [Fact]
    public void PromotionReducedPricePositiveWhenNotNull()
    {
        var service = new OfferExporter.Services.RowService();

        var offersRow = new List<GetAllOffersResultRow>
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
                2611403,
                1,
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

        var expectedOfferRow = new List<GetAllOffersResultRow>
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
                2611403,
                1,
                "Fnac",
                true,
                2776585,
                true,
                10,
                10,
                3724,
                "MyUnivers",
                true,
                null,
                null,
                null,
                "Public")
        };

        var result = service.RemoveInvalidRows(offersRow);
        result.Should().BeEquivalentTo(expectedOfferRow);
    }

    [Fact]
    public void ReturnOfferQuantityPositive()
    {
        var service = new OfferExporter.Services.RowService();

        var offersRow = new List<GetAllOffersResultRow>
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
                2611403,
                1,
                "Fnac",
                true,
                2776585,
                false,
                10,
                0,
                3724,
                "MyUnivers",
                true,
                null,
                18.04M,
                1,
                "Public")
        };

        var expectedOfferRow = new List<GetAllOffersResultRow>
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

        var result = service.RemoveInvalidRows(offersRow);
        result.Should().BeEquivalentTo(expectedOfferRow);
    }

    [Fact]
    public void ReturnSellerIsActivePositive()
    {
        var service = new OfferExporter.Services.RowService();

        var offersRow = new List<GetAllOffersResultRow>
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
                2611403,
                1,
                "Fnac",
                true,
                2776585,
                false,
                10,
                10,
                3724,
                "MyUnivers",
                false,
                null,
                18.04M,
                1,
                "Public")
        };

        var expectedOfferRow = new List<GetAllOffersResultRow>
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

        var result = service.RemoveInvalidRows(offersRow);
        result.Should().BeEquivalentTo(expectedOfferRow);
    }

    [Fact]
    public void ReturnReferentialIsExportable()
    {
        var service = new OfferExporter.Services.RowService();

        var offersRow = new List<GetAllOffersResultRow>
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
                2611403,
                1,
                "Fnac",
                false,
                2776585,
                false,
                10,
                10,
                3724,
                "MyUnivers",
                true,
                null,
                18.04M,
                1,
                "Public")
        };

        var expectedOfferRow = new List<GetAllOffersResultRow>
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

        var result = service.RemoveInvalidRows(offersRow);
        result.Should().BeEquivalentTo(expectedOfferRow);
    }

    [Fact]
    public void ReturnNotReferentialIdEqualTwo()
    {
        var service = new OfferExporter.Services.RowService();

        var offersRow = new List<GetAllOffersResultRow>
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
                2611403,
                2,
                "Fnac",
                false,
                2776585,
                false,
                10,
                10,
                3724,
                "MyUnivers",
                true,
                null,
                18.04M,
                1,
                "Public")
        };

        var expectedOfferRow = new List<GetAllOffersResultRow>
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

        var result = service.RemoveInvalidRows(offersRow);
        result.Should().BeEquivalentTo(expectedOfferRow);
    }
}