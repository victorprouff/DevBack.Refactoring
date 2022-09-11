using FluentAssertions;
using OfferExporter.Services;

namespace OfferExporter.Tests.Services;

public class RemoveInvalidRowsShould
{
    [Fact]
    public void RemoveInvalidRowsReturnOfferIsActive()
    {
        var service = new RowService();

        var offersRow = new List<GetAllOffersResultRow>
        {
            new()
            {
                ProductPrid = 2515866, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 1499665, OfferIsActive = true, OfferPrice = 20.04M,
                OfferQuantity = 10, SellerId = 284, SellerName = "Inandout_Dist", SellerIsActive = true, PromotionId = 775, PromotionReducedPrice = 18.04M, PromotionTargetId = 1,
                PromotionTargetName = "Public"
            },
            new()
            {
                ProductPrid = 2611403, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 2776585, OfferIsActive = false, OfferPrice = 10.50M,
                OfferQuantity = 10, SellerId = 3724, SellerName = "MyUnivers", SellerIsActive = true, PromotionId = null, PromotionReducedPrice = 18.04M, PromotionTargetId = 1,
                PromotionTargetName = "Public"
            }
        };

        var expectedOfferRow = new List<GetAllOffersResultRow>
        {
            new()
            {
                ProductPrid = 2515866, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 1499665, OfferIsActive = true, OfferPrice = 20.04M,
                OfferQuantity = 10, SellerId = 284, SellerName = "Inandout_Dist", SellerIsActive = true, PromotionId = 775, PromotionReducedPrice = 18.04M, PromotionTargetId = 1,
                PromotionTargetName = "Public"
            }
        };

        var result = service.RemoveInvalidRows(offersRow);
        result.Should().BeEquivalentTo(expectedOfferRow);
    }

    [Fact]
    public void RemoveInvalidRowsReturnOfferPricePositive()
    {
        var service = new RowService();

        var offersRow = new List<GetAllOffersResultRow>
        {
            new()
            {
                ProductPrid = 2515866, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 1499665, OfferIsActive = true, OfferPrice = 20.04M,
                OfferQuantity = 10, SellerId = 284, SellerName = "Inandout_Dist", SellerIsActive = true, PromotionId = 775, PromotionReducedPrice = 18.04M, PromotionTargetId = 1,
                PromotionTargetName = "Public"
            },
            new()
            {
                ProductPrid = 2611403, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 2776585, OfferIsActive = false, OfferPrice = 0,
                OfferQuantity = 10, SellerId = 3724, SellerName = "MyUnivers", SellerIsActive = true, PromotionId = null, PromotionReducedPrice = 18.04M, PromotionTargetId = 1,
                PromotionTargetName = "Public"
            }
        };

        var expectedOfferRow = new List<GetAllOffersResultRow>
        {
            new()
            {
                ProductPrid = 2515866, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 1499665, OfferIsActive = true, OfferPrice = 20.04M,
                OfferQuantity = 10, SellerId = 284, SellerName = "Inandout_Dist", SellerIsActive = true, PromotionId = 775, PromotionReducedPrice = 18.04M, PromotionTargetId = 1,
                PromotionTargetName = "Public"
            }
        };

        var result = service.RemoveInvalidRows(offersRow);
        result.Should().BeEquivalentTo(expectedOfferRow);
    }
}