using VegeShama.Common.Enums;

namespace VegeShama.Common.DomainModels
{
    public class Payment
    {
        public Guid Id { get; }
        public DateOnly DueDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public Payment(MongoDB.Payment payment)
        {
            Id = payment.Id;
            DueDate = payment.DueDate.Date;
            PaymentMethod = (PaymentMethod)payment.PaymentMethod;
            PaymentStatus = (PaymentStatus)payment.PaymentStatus;
        }

        public Payment(EFCore.Payment payment)
        {
            Id = payment.Id;
            DueDate = payment.DueDate.Date;
            PaymentMethod = (PaymentMethod)payment.PaymentMethod;
            PaymentStatus = (PaymentStatus)payment.PaymentStatus;
        }

        public Payment(Relational.Payment payment)
        {
            Id = payment.Id;
            DueDate = payment.DueDate.Date;
            PaymentMethod = (PaymentMethod)payment.PaymentMethod;
            PaymentStatus = (PaymentStatus)payment.PaymentStatus;
        }
    }
}
