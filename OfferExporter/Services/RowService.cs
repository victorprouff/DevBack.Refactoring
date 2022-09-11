namespace OfferExporter.Services;

public class RowService
{
    public List<GetAllOffersResultRow> RemoveInvalidRows(List<GetAllOffersResultRow> rows)
    {
        var result = rows.Where(r =>
            r.OfferIsActive &&
            r.OfferPrice > 0 &&
            r.OfferQuantity > 0 &&
            r.SellerIsActive &&
            r.ReferentialIsExportable &&
            (r.PromotionReducedPrice is null || r.PromotionReducedPrice is > 0)).ToList();

        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Exclude invalid offers: {result.Count} left");

        return result;
    }
}