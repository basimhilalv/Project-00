using Project_00.Models;

namespace Project_00.Services
{
    public interface IPaymentServices
    {
        public Task<PaymentProduct> MakePaymentProduct(PaymentProductDto payment);
        public Task<PaymentCart> MakePaymentCart();
    }
}
