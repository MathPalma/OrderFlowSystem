namespace Core.Domain.Enums
{
    public enum OrderStatus
    {
        New = 1,
        ProcessingPayment,
        Paid,
        PaymentFailed,
        PreparingShipment,
        Shipped,
        Delivered,
        Canceled
    }
}
