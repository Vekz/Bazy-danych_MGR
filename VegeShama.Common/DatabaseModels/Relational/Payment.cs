namespace VegeShama.Common.DatabaseModels.Relational
{
    public class Payment
    {
        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }
        public int Method { get; set; }
        public int Status { get; set; }
    }
}
