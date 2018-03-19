namespace CustomerEntity
{
    public class CreditCard
    {
        public string Number { get; set; }
        public string Type { get; set; }
        public string ExpirationDate { get; set; }

        public CreditCard(string ccNumber, string ccType, string ccExpiry)
        {
            Number = ccNumber;
            Type = ccType;
            ExpirationDate = ccExpiry;
        }

        public override bool Equals(object cc)
        {
            if (!(cc is CreditCard))
                return false;

            CreditCard otherCard = (CreditCard)cc;

            if (Number != otherCard.Number) return false;
            if (Type != otherCard.Type) return false;
            if (ExpirationDate != otherCard.ExpirationDate) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}