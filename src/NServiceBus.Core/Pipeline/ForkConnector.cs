namespace NServiceBus.Pipeline
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Forks into another independent pipeline.
    /// </summary>
    /// <typeparam name="TFromContext">The context to connect from.</typeparam>
    /// <typeparam name="TForkContext">The context to fork an independent pipeline to.</typeparam>
    public abstract class ForkConnector<TFromContext, TForkContext> : Behavior<TFromContext>, IForkConnector<TFromContext, TFromContext, TForkContext>
        where TFromContext : IBehaviorContext
        where TForkContext : IBehaviorContext
    {
        /// <summary>
        /// Called when the fork connector is executed.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="next">The next <see cref="IBehavior{TFromContext,TFromContext}" /> in the chain to execute.</param>
        /// <param name="fork">The next <see cref="IBehavior{TForkContext,TForkContext}" /> in the chain to fork and execute.</param>
        public abstract Task Invoke(TFromContext context, CancellationToken cancellationToken, Func<Task> next, Func<TForkContext, Task> fork);

        /// <inheritdoc />
        public sealed override Task Invoke(TFromContext context, CancellationToken cancellationToken, Func<Task> next)
        {
            Guard.AgainstNull(nameof(context), context);
            Guard.AgainstNull(nameof(next), next);

            return Invoke(context, cancellationToken, next, ctx => ctx.InvokePipeline(cancellationToken));
        }
    }
}