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

        public override bool Equals(object addr)
        {
            if (!(addr is Address))
                return false;

            Address otherAddr = (Address)addr;

            if (HomeAddress != otherAddr.HomeAddress) return false;
            if (City != otherAddr.City) return false;
            if (State != otherAddr.State) return false;
            if (ZipCode != otherAddr.ZipCode) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}