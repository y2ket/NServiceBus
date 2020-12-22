namespace NServiceBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Pipeline;
    using Transport;

    class OutgoingPhysicalToRoutingConnector : StageConnector<IOutgoingPhysicalMessageContext, IRoutingContext>
    {
        public override Task Invoke(IOutgoingPhysicalMessageContext context, CancellationToken cancellationToken, Func<IRoutingContext, CancellationToken, Task> stage)
        {
            var message = new OutgoingMessage(context.MessageId, context.Headers, context.Body);

            return stage(this.CreateRoutingContext(message, context.RoutingStrategies, context), cancellationToken);
        }
    }
}