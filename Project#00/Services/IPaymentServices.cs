using Project_00.Models;

namespace Project_00.Services
{
    public interface IPaymentServices
    {
        public Task<PaymentProduct> MakePaymentProduct(PaymentProductDto payment, Guid userID);
        public Task<PaymentCart> MakePaymentCart(Guid userID);
    }
}
