using Project_00.Dtos;
using Project_00.Models;

namespace Project_00.Services.PaymentService
{
    public interface IPaymentServices
    {
        public Task<PaymentProduct> MakePaymentProduct(PaymentProductDto payment, Guid userID);
        public Task<PaymentCart> MakePaymentCart(PaymentCartDto request, Guid userID);
    }
}
