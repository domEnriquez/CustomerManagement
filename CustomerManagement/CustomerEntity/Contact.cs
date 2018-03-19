namespace CustomerEntity
{
    public class Contact
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Contact(string email, string phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public override bool Equals(object contact)
        {
            if (!(contact is Contact))
                return false;

            Contact otherContact = (Contact)contact;

            if (Email != otherContact.Email) return false;
            if (PhoneNumber != otherContact.PhoneNumber) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}