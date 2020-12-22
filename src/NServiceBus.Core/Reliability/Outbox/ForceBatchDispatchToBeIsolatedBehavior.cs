namespace NServiceBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Pipeline;
    using Transport;

    class ForceBatchDispatchToBeIsolatedBehavior : IBehavior<IBatchDispatchContext, IBatchDispatchContext>
    {
        public Task Invoke(IBatchDispatchContext context, CancellationToken cancellationToken, Func<IBatchDispatchContext, CancellationToken, Task> next)
        {
            foreach (var operation in context.Operations)
            {
                operation.RequiredDispatchConsistency = DispatchConsistency.Isolated;
            }
            return next(context, cancellationToken);
        }
    }
}