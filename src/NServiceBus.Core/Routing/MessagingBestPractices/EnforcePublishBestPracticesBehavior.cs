namespace NServiceBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Pipeline;

    class EnforcePublishBestPracticesBehavior : IBehavior<IOutgoingPublishContext, IOutgoingPublishContext>
    {
        public EnforcePublishBestPracticesBehavior(Validations validations)
        {
            this.validations = validations;
        }

        public Task Invoke(IOutgoingPublishContext context, CancellationToken cancellationToken, Func<IOutgoingPublishContext, CancellationToken, Task> next)
        {
            if (!context.Extensions.TryGet(out EnforceBestPracticesOptions options) || options.Enabled)
            {
                validations.AssertIsValidForPubSub(context.Message.MessageType);
            }

            return next(context, cancellationToken);
        }

        readonly Validations validations;
    }
}