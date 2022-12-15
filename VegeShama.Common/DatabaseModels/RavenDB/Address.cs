namespace VegeShama.Common.DatabaseModels.RavenDB
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string StreetNo { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
}
