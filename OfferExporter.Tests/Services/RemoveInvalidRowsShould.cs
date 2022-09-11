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

    [Fact]
    public void RemoveInvalidRowsReturnPromotionReducedPricePositiveWhenNotNull()
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
                ProductPrid = 2515865, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 1499666, OfferIsActive = true, OfferPrice = 20.04M,
                OfferQuantity = 10, SellerId = 284, SellerName = "Inandout_Dist", SellerIsActive = true, PromotionId = 775, PromotionReducedPrice = null, PromotionTargetId = 1,
                PromotionTargetName = "Public"
            },
            new()
            {
                ProductPrid = 2611403, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 2776585, OfferIsActive = true, OfferPrice = 10,
                OfferQuantity = 10, SellerId = 3724, SellerName = "MyUnivers", SellerIsActive = true, PromotionId = 32, PromotionReducedPrice = 18.04M, PromotionTargetId = 1,
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
            },
            new()
            {
                ProductPrid = 2515865, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 1499666, OfferIsActive = true, OfferPrice = 20.04M,
                OfferQuantity = 10, SellerId = 284, SellerName = "Inandout_Dist", SellerIsActive = true, PromotionId = 775, PromotionReducedPrice = null, PromotionTargetId = 1,
                PromotionTargetName = "Public"
            },
            new()
            {
                ProductPrid = 2611403, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 2776585, OfferIsActive = true, OfferPrice = 10,
                OfferQuantity = 10, SellerId = 3724, SellerName = "MyUnivers", SellerIsActive = true, PromotionId = null, PromotionReducedPrice = null, PromotionTargetId = null,
                PromotionTargetName = "Public"
            }
        };

        var result = service.RemoveInvalidRows(offersRow);
        result.Should().BeEquivalentTo(expectedOfferRow);
    }

    [Fact]
    public void RemoveInvalidRowsReturnOfferQuantityPositive()
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
                ProductPrid = 2611403, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 2776585, OfferIsActive = false, OfferPrice = 10,
                OfferQuantity = 0, SellerId = 3724, SellerName = "MyUnivers", SellerIsActive = true, PromotionId = null, PromotionReducedPrice = 18.04M, PromotionTargetId = 1,
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
    public void RemoveInvalidRowsReturnSellerIsActivePositive()
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
                ProductPrid = 2611403, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = true, OfferId = 2776585, OfferIsActive = false, OfferPrice = 10,
                OfferQuantity = 10, SellerId = 3724, SellerName = "MyUnivers", SellerIsActive = false, PromotionId = null, PromotionReducedPrice = 18.04M, PromotionTargetId = 1,
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
    public void RemoveInvalidRowsReturnReferentialIsExportable()
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
                ProductPrid = 2611403, ReferentialId = 1, ReferentialName = "Fnac", ReferentialIsExportable = false, OfferId = 2776585, OfferIsActive = false, OfferPrice = 10,
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
    public void RemoveInvalidRowsReturnNotReferentialIdEqualTwo()
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
                ProductPrid = 2611403, ReferentialId = 2, ReferentialName = "Fnac", ReferentialIsExportable = false, OfferId = 2776585, OfferIsActive = false, OfferPrice = 10,
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