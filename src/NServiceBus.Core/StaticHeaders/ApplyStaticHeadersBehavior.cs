namespace NServiceBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Pipeline;

    class ApplyStaticHeadersBehavior : IBehavior<IOutgoingLogicalMessageContext, IOutgoingLogicalMessageContext>
    {
        public ApplyStaticHeadersBehavior(CurrentStaticHeaders currentStaticHeaders)
        {
            this.currentStaticHeaders = currentStaticHeaders;
        }

        public Task Invoke(IOutgoingLogicalMessageContext context, CancellationToken cancellationToken, Func<IOutgoingLogicalMessageContext, CancellationToken, Task> next)
        {
            foreach (var staticHeader in currentStaticHeaders)
            {
                context.Headers[staticHeader.Key] = staticHeader.Value;
            }

            return next(context, cancellationToken);
        }

        readonly CurrentStaticHeaders currentStaticHeaders;
    }
}