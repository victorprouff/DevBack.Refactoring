namespace OfferExporter
{
    /// <summary>
    /// Represents a row returned by the stored procedure GetAllOffersResultRow.
    /// </summary>
    public class GetAllOffersResultRow
    {
        public GetAllOffersResultRow(
            int productPrid,
            byte referentialId,
            string referentialName,
            bool referentialIsExportable,
            int offerId,
            bool offerIsActive,
            decimal offerPrice,
            short offerQuantity,
            int sellerId,
            string sellerName,
            bool sellerIsActive,
            int? promotionId,
            decimal? promotionReducedPrice,
            byte? promotionTargetId,
            string? promotionTargetName)
        {
            ProductPrid = productPrid;
            ReferentialId = referentialId;
            ReferentialName = referentialName;
            ReferentialIsExportable = referentialIsExportable;
            OfferId = offerId;
            OfferIsActive = offerIsActive;
            OfferPrice = offerPrice;
            OfferQuantity = offerQuantity;
            SellerId = sellerId;
            SellerName = sellerName;
            SellerIsActive = sellerIsActive;
            PromotionId = promotionId;
            PromotionReducedPrice = promotionReducedPrice;
            PromotionTargetId = promotionTargetId;
            PromotionTargetName = promotionTargetName;
        }

        public int ProductPrid { get; }
        public byte ReferentialId { get; }
        public string ReferentialName { get; }
        public bool ReferentialIsExportable { get; }
        public int OfferId { get; }
        public bool OfferIsActive { get; }
        public decimal OfferPrice { get; }
        public short OfferQuantity { get; }
        public int SellerId { get; }
        public string SellerName { get; }
        public bool SellerIsActive { get; }
        public int? PromotionId { get; }
        public decimal? PromotionReducedPrice { get; }
        public byte? PromotionTargetId { get; }
        public string? PromotionTargetName { get; }
    }
}