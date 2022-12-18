namespace VegeShama.Common.DatabaseModels.RavenDB
{
    public class Payment
    {
        public string Id { get; set; }
        public DateTime DueDate { get; set; }
        public int Method { get; set; }
        public int Status { get; set; }
    }
}
