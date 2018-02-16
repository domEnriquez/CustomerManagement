namespace CustomerManagementConsole
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
    }
}