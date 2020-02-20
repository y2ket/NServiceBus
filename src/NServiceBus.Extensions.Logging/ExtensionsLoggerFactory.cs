namespace NServiceBus.Extensions.Logging
{
    using System;
    using NServiceBus.Logging;

    /// <summary>
    /// Usage:
    ///       ILoggerFactory extensionsLoggingFactory = ...;
    ///       LogManager.UseFactory(new ExtensionsLoggerFactory(extensionsLoggingFactory));
    /// </summary>
    public class ExtensionsLoggerFactory : ILoggerFactory
    {

        public ExtensionsLoggerFactory(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public ILog GetLogger(Type type)
        {
            return new ExtensionsLogger(loggerFactory.CreateLogger(type.FullName));
        }

        public ILog GetLogger(string name)
        {
            return new ExtensionsLogger(loggerFactory.CreateLogger(name));
        }

        Microsoft.Extensions.Logging.ILoggerFactory loggerFactory;
    }
}
