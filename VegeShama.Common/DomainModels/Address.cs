namespace VegeShama.Common.DomainModels
{
    public class Address
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }

        public Address(MongoDB.Address address)
        {
            Street = address.Street;
            StreetNumber = address.StreetNumber;
            PostCode = address.PostCode;
            City = address.City;
        }

        public Address(EFCore.Address address)
        {
            Street = address.Street;
            StreetNumber = address.StreetNumber;
            PostCode = address.PostCode;
            City = address.City;
        }

        public Address(Relational.Address address)
        {
            Street = address.Street;
            StreetNumber = address.StreetNumber;
            PostCode = address.PostCode;
            City = address.City;
        }
    }
}
