namespace CustomerManagementConsole
{
    public class Address
    {
        public string HomeAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Address(string homeAddress, string city, string state, string zipCode)
        {
            HomeAddress = homeAddress;
            City = city;
            State = state;
            ZipCode = zipCode;
        }
    }
}