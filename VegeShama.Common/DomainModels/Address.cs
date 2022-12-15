using EFCoreDal = VegeShama.Common.DatabaseModels.EFCore;
using MongoDBDal = VegeShama.Common.DatabaseModels.MongoDB;
using RelationalDal = VegeShama.Common.DatabaseModels.Relational;

namespace VegeShama.Common.DomainModels
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }

        public Address(MongoDBDal.Address address)
        {
            Id = address.Id;
            Street = address.Street;
            StreetNumber = address.StreetNo;
            PostCode = address.PostCode;
            City = address.City;
        }

        public Address(EFCoreDal.Address address)
        {
            Id = address.Id;
            Street = address.Street;
            StreetNumber = address.StreetNo;
            PostCode = address.PostCode;
            City = address.City;
        }

        public Address(RelationalDal.Address address, RelationalDal.PostCode postCode)
        {
            Id = address.Id;
            Street = address.Street;
            StreetNumber = address.StreetNo;
            PostCode = address.PostCode;
            City = postCode.City;
        }
    }
}
