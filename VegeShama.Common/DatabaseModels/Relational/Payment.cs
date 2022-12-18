namespace VegeShama.Common.DatabaseModels.Relational
{
    public class Payment
    {
        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }
        public byte Method { get; set; }
        public byte Status { get; set; }
    }
}
