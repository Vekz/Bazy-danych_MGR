using VegeShama.Common.Enums;
using EFCoreDal = VegeShama.Common.DatabaseModels.EFCore;
using RavenDBDal = VegeShama.Common.DatabaseModels.RavenDB;
using RelationalDal = VegeShama.Common.DatabaseModels.Relational;

namespace VegeShama.Common.DomainModels
{
    public class Payment
    {
        public Guid Id { get; }
        public DateOnly DueDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public Payment(RavenDBDal.Payment payment)
        {
            Id = payment.Id;
            DueDate = DateOnly.FromDateTime(payment.DueDate);
            PaymentMethod = (PaymentMethod)payment.Method;
            PaymentStatus = (PaymentStatus)payment.Status;
        }

        public Payment(EFCoreDal.Payment payment)
        {
            Id = payment.Id;
            DueDate = DateOnly.FromDateTime(payment.DueDate);
            PaymentMethod = (PaymentMethod)payment.Method;
            PaymentStatus = (PaymentStatus)payment.Status;
        }

        public Payment(RelationalDal.Payment payment)
        {
            Id = payment.Id;
            DueDate = DateOnly.FromDateTime(payment.DueDate);
            PaymentMethod = (PaymentMethod)payment.Method;
            PaymentStatus = (PaymentStatus)payment.Status;
        }
    }
}
