using System.Runtime.Serialization;

namespace OfferExporter
{
    public class Product
    {
        public Product(int prid, int referentialId, string referentialName)
        {
            Prid = prid;
            ReferentialId = referentialId;
            ReferentialName = referentialName;
        }

        [DataMember(Name = "prid")]
        public string Key => $"{ReferentialId}-{Prid}";

        [IgnoreDataMember]
        public int Prid { get; }

        [IgnoreDataMember]
        public int ReferentialId { get; }

        [IgnoreDataMember]
        public string ReferentialName { get; }

        [DataMember(Name = "offers")]
        public IList<Offer> Offers { get; set; } = new List<Offer>();

        public override bool Equals(object? obj)
        {
            return obj is Product product
                   && product.Prid == Prid
                   && product.ReferentialId == ReferentialId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = hash * 11 + Prid.GetHashCode();
                hash = hash * 17 + ReferentialId.GetHashCode();
                return hash;
            }
        }
    }
}