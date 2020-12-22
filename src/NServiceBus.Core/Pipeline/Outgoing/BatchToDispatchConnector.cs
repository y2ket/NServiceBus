namespace NServiceBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Pipeline;

    class BatchToDispatchConnector : StageConnector<IBatchDispatchContext, IDispatchContext>
    {
        public override Task Invoke(IBatchDispatchContext context, CancellationToken cancellationToken, Func<IDispatchContext, CancellationToken, Task> stage)
        {
            return stage(this.CreateDispatchContext(context.Operations, context), cancellationToken);
        }
    }
}