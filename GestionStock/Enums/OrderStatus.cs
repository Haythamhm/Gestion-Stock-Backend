namespace GestionStock.Enums
{
    public enum OrderStatus
    {
        Draft = 1,
        Approved = 2,
        PartiallyReceived = 3,
        Received = 4,
        Cancelled = 5,
        Closed = 6,

        Confirmed = 20,
        Prepared = 21,
        Shipped = 22,
        Invoiced = 23
    }
}
