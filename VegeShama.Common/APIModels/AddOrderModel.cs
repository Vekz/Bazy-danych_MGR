namespace VegeShama.Common.APIModels
{
    public class AddOrderModel
    {
        public Guid UserId { get; set; }


        public AddOrderModel_Address Address { get; set; }
        public List<Guid> ProductIds { get; set; }
    }

    public class AddOrderModel_Address
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
    }
}
