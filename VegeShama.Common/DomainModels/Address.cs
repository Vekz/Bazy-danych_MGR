using EFCoreDal = VegeShama.Common.DatabaseModels.EFCore;
using RavenDBDal = VegeShama.Common.DatabaseModels.RavenDB;
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

        public Address(RavenDBDal.Address address)
        {
            Id = Guid.Parse(address.Id);
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

        public Address(RelationalDal.Address address)
        {
            Id = address.Id;
            Street = address.Street;
            StreetNumber = address.StreetNo;
            PostCode = address.Post.PostCode;
            City = address.Post.City;
        }
    }
}
