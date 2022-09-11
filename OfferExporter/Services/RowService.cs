namespace OfferExporter.Services;

public class RowService
{
    public List<GetAllOffersResultRow> RemoveInvalidRows(List<GetAllOffersResultRow> rows)
    {
        for (var i = rows.Count - 1; i >= 0; i--)
        {
            var row = rows[i];

            if (!row.OfferIsActive)
            {
                rows.Remove(row);
                continue;
            }

            if (row.OfferPrice <= 0)
            {
                rows.Remove(row);
                continue;
            }

            if (row.PromotionReducedPrice.HasValue)
            {
                if (row.PromotionReducedPrice <= 0)
                {
                    rows.Remove(row);
                    continue;
                }
            }

            if (row.OfferQuantity <= 0)
            {
                rows.Remove(row);
                continue;
            }

            // Filter on seller data
            if (!row.SellerIsActive)
            {
                rows.Remove(row);
            }

            // Filter on referential data
            if (!row.ReferentialIsExportable)
            {
                rows.Remove(row);
                continue;
            }
        }

        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Exclude invalid offers: {rows.Count} left");

        return rows;
    }
}