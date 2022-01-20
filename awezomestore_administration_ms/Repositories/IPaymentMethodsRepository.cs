using awezomestore_administration_ms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awezomestore_administration_ms.Repositories
{
    public interface IPaymentMethodsRepository
    {
        Task<PaymentMethods> GetPaymentMethods(int paymentMethod_id);
        Task<bool> CreatePaymentMethod(PaymentMethods paymentMethods);
        Task<bool> UpdatePaymentMethod(PaymentMethods paymentMethods);
        Task<bool> DeletePaymentMethod(int paymentMethod_id);
    }
}
