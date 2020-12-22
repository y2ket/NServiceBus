namespace NServiceBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Pipeline;

    class EnforceUnsubscribeBestPracticesBehavior : IBehavior<IUnsubscribeContext, IUnsubscribeContext>
    {
        public EnforceUnsubscribeBestPracticesBehavior(Validations validations)
        {
            this.validations = validations;
        }

        public Task Invoke(IUnsubscribeContext context, CancellationToken cancellationToken, Func<IUnsubscribeContext, CancellationToken, Task> next)
        {
            if (!context.Extensions.TryGet(out EnforceBestPracticesOptions options) || options.Enabled)
            {
                validations.AssertIsValidForPubSub(context.EventType);
            }

            return next(context, cancellationToken);
        }

        readonly Validations validations;
    }
}