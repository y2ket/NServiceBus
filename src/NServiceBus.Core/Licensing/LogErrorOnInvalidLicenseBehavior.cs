namespace NServiceBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Logging;
    using Pipeline;

    class LogErrorOnInvalidLicenseBehavior : IBehavior<IIncomingPhysicalMessageContext, IIncomingPhysicalMessageContext>
    {
        public Task Invoke(IIncomingPhysicalMessageContext context, CancellationToken cancellationToken, Func<IIncomingPhysicalMessageContext, CancellationToken, Task> next)
        {
            Log.Error("Your license has expired");

            return next(context, cancellationToken);
        }

        static readonly ILog Log = LogManager.GetLogger<LicenseManager>();
    }
}