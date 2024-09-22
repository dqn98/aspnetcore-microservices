using Contracts.Common.Events;

namespace Ordering.Domain.OrderAggegate.Events
{
    public class OrderCreatedEvent : BaseEvent
    {
        public OrderCreatedEvent(long id, string userName, string documentNo,
            decimal totalPrice, string firstName,
            string lastName, string emailAddress,
            string shippingAddress, string invoiceAddress)
        {
            Id = id;
            UserName = userName;
            DocumentNo = documentNo;
            TotalPrice = totalPrice;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            ShippingAddress = shippingAddress;
            InvoiceAddress = invoiceAddress;
        }

        public long Id { get; private set; }
        public string UserName { get; set; }
        public string DocumentNo { get; set; }
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string InvoiceAddress { get; set; }
    }
}