namespace NServiceBus
{
    using Unicast.Messages;
    using System.Threading;
    using System.Threading.Tasks;
    using Pipeline;
    using Transport;

    class NativeSubscribeTerminator : PipelineTerminator<ISubscribeContext>
    {
        public NativeSubscribeTerminator(ISubscriptionManager subscriptionManager, MessageMetadataRegistry messageMetadataRegistry)
        {
            this.subscriptionManager = subscriptionManager;
            this.messageMetadataRegistry = messageMetadataRegistry;
        }

        protected override Task Terminate(ISubscribeContext context, CancellationToken token)
        {
            var eventMetadata = messageMetadataRegistry.GetMessageMetadata(context.EventType);
            return subscriptionManager.Subscribe(eventMetadata, context.Extensions);
        }

        readonly ISubscriptionManager subscriptionManager;
        readonly MessageMetadataRegistry messageMetadataRegistry;
    }
}