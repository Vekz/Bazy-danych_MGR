using VegeShama.Common.Enums;
using EFCoreDal = VegeShama.Common.DatabaseModels.EFCore;
using MongoDBDal = VegeShama.Common.DatabaseModels.MongoDB;
using RelationalDal = VegeShama.Common.DatabaseModels.Relational;

namespace VegeShama.Common.DomainModels
{
    public class Payment
    {
        public Guid Id { get; }
        public DateOnly DueDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public Payment(MongoDBDal.Payment payment)
        {
            Id = payment.Id;
            DueDate = DateOnly.FromDateTime(payment.DueDate);
            PaymentMethod = (PaymentMethod)payment.PaymentMethod;
            PaymentStatus = (PaymentStatus)payment.PaymentStatus;
        }

        public Payment(EFCoreDal.Payment payment)
        {
            Id = payment.Id;
            DueDate = DateOnly.FromDateTime(payment.DueDate);
            PaymentMethod = (PaymentMethod)payment.PaymentMethod;
            PaymentStatus = (PaymentStatus)payment.PaymentStatus;
        }

        public Payment(RelationalDal.Payment payment)
        {
            Id = payment.Id;
            DueDate = DateOnly.FromDateTime(payment.DueDate);
            PaymentMethod = (PaymentMethod)payment.PaymentMethod;
            PaymentStatus = (PaymentStatus)payment.PaymentStatus;
        }
    }
}
