using CustomerEntity;

namespace CustomerManagementTests.TestDataBuilders
{
    public class CustomerBuilder
    {
        private string id = "000001";
        private string firstName = "Dom";
        private string lastName = "Enriquez";
        private Address address = new AddressBuilder().build();
        private Contact contact = new ContactBuilder().build();
        private CreditCard cc = new CreditCardBuilder().build();

        public Customer build()
        {
            return new Customer(id, firstName, lastName, address, contact, cc);
        }

        public CustomerBuilder withId(string id)
        {
            this.id = id;

            return this;
        }

        public CustomerBuilder withName(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;

            return this;
        }
    }
}
