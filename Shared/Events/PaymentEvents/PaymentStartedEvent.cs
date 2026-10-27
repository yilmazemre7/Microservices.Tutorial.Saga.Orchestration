using MassTransit;
using Shared.Messages;

namespace Shared.Events.PaymentEvents
{
    public class PaymentStartedEvent : CorrelatedBy<Guid>
    {
        public PaymentStartedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public Guid CorrelationId { get; }
        public List<OrderItemMessage> OrderItems { get; set; }
    }
}
