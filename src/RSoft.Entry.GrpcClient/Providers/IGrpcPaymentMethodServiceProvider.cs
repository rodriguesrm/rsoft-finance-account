using RSoft.Entry.GrpcClient.Models;
using System;
using System.Threading.Tasks;

namespace RSoft.Entry.GrpcClient.Providers
{

    /// <summary>
    /// RSoft gRpc PaymentMethod Service provider interface contract
    /// </summary>
    public interface IGrpcPaymentMethodServiceProvider : ITokenForProvider
    {

        /// <summary>
        /// Create PaymentMethod
        /// </summary>
        /// <param name="name">PaymentMethod name</param>
        /// <param name="paymentType">Payment type number</param>
        Task<CreatePaymentMethodResponse> CreatePaymentMethod(string name, int paymentType);

        /// <summary>
        /// Udpate an existing PaymentMethod
        /// </summary>
        /// <param name="id">PaymentMethod id key value</param>
        /// <param name="name">PaymentMethod name</param>
        /// <param name="paymentType">Payment type number</param>
        Task<UpdatePaymentMethodResponse> UpdatePaymentMethod(Guid id, string name, int paymentType);

        /// <summary>
        /// Enable an existing PaymentMethod
        /// </summary>
        /// <param name="id">PaymentMethod id key value</param>
        Task<ChangePaymentMethodStatusResponse> EnablePaymentMethod(Guid id);

        /// <summary>
        /// Disable an existing PaymentMethod
        /// </summary>
        /// <param name="id">PaymentMethod id key value</param>
        Task<ChangePaymentMethodStatusResponse> DisablePaymentMethod(Guid id);

        /// <summary>
        /// Get PaymentMethod by id
        /// </summary>
        /// <param name="id">PaymentMethod id key value</param>
        Task<PaymentMethodDetailResponse> GetPaymentMethod(Guid id);

        /// <summary>
        /// List payment methods
        /// </summary>
        Task<ListPaymentMethodDetailResponse> ListPaymentMethod();

        
    }
}