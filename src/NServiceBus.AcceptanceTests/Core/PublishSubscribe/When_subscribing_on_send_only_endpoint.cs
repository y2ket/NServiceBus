﻿namespace NServiceBus.AcceptanceTests.PublishSubscribe
{
    using System;
    using AcceptanceTesting;
    using EndpointTemplates;
    using NUnit.Framework;

    public class When_subscribing_on_send_only_endpoint : NServiceBusAcceptanceTest
    {
        [Test]
        public void Should_throw_InvalidOperationException_on_native_pubsub()
        {
            var exception = Assert.ThrowsAsync<InvalidOperationException>(() => Scenario.Define<ScenarioContext>()
                .WithEndpoint<NativePubSubSendOnlyEndpoint>(e => e
                    .When(s => s.Subscribe<SomeEvent>()))
                .Done(c => c.EndpointsStarted)
                .Run());

            StringAssert.Contains("Send-only endpoints cannot subscribe to events", exception.Message);
        }

        class NativePubSubSendOnlyEndpoint : EndpointConfigurationBuilder
        {
            public NativePubSubSendOnlyEndpoint()
            {
                var template = new DefaultServer();
                template.TransportConfiguration = new ConfigureEndpointAcceptanceTestingTransport();
                EndpointSetup(template, (configuration, _) => configuration.SendOnly());
            }
        }

        public class SomeEvent : IEvent
        {
        }
    }
}