namespace NServiceBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Pipeline;

    class InferredMessageTypeEnricherBehavior : Behavior<IIncomingLogicalMessageContext>
    {
        public override Task Invoke(IIncomingLogicalMessageContext context, CancellationToken cancellationToken, Func<Task> next)
        {
            if (!context.Headers.ContainsKey(Headers.EnclosedMessageTypes))
            {
                context.Headers[Headers.EnclosedMessageTypes] = context.Message.MessageType.FullName;
            }

            return next();
        }
    }
}