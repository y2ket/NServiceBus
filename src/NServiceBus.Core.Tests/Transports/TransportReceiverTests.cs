﻿namespace NServiceBus.Core.Tests.Transports
{
    using System.Collections.Generic;
    using Unicast.Messages;
    using System;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Transport;

    [TestFixture]
    public class TransportReceiverTests
    {
        [SetUp]
        public void SetUp()
        {
            pump = new MessageReceiver();

            receiver = new TransportReceiver(pump, new PushRuntimeSettings());
        }

        [Test]
        public async Task Start_should_start_the_pump()
        {
            await receiver.Start(_ => Task.CompletedTask, _ => Task.FromResult(ErrorHandleResult.Handled), new List<MessageMetadata>());

            Assert.IsTrue(pump.Started);
        }

        [Test]
        public void Start_should_rethrow_when_pump_throws()
        {
            pump.ThrowOnStart = true;

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await receiver.Start(_ => Task.CompletedTask, _ => Task.FromResult(ErrorHandleResult.Handled), new List<MessageMetadata>())
                );
        }

        [Test]
        public async Task Stop_should_stop_the_pump()
        {
            await receiver.Start(_ => Task.CompletedTask, _ => Task.FromResult(ErrorHandleResult.Handled), new List<MessageMetadata>());

            await receiver.Stop();

            Assert.IsTrue(pump.Stopped);
        }

        [Test]
        public async Task Stop_should_not_throw_when_pump_throws()
        {
            pump.ThrowOnStop = true;

            await receiver.Start(_ => Task.CompletedTask, _ => Task.FromResult(ErrorHandleResult.Handled), new List<MessageMetadata>());

            Assert.DoesNotThrowAsync(async () => await receiver.Stop());
        }

        MessageReceiver pump;
        TransportReceiver receiver;

        class MessageReceiver : IMessageReceiver
        {
            public bool ThrowOnStart { private get; set; }
            public bool ThrowOnStop { private get; set; }

            public bool Started { get; private set; }
            public bool Stopped { get; private set; }


            public Task Initialize(PushRuntimeSettings limitations, Func<MessageContext, Task> onMessage, Func<ErrorContext, Task<ErrorHandleResult>> onError, IReadOnlyCollection<MessageMetadata> events)
            {
                return Task.CompletedTask;
            }

            public Task StartReceive()
            {
                if (ThrowOnStart)
                {
                    throw new InvalidOperationException();
                }

                Started = true;

                return Task.CompletedTask;
            }

            public Task StopReceive()
            {
                if (ThrowOnStop)
                {
                    throw new InvalidOperationException();
                }

                Stopped = true;

                return Task.CompletedTask;
            }

            public ISubscriptionManager Subscriptions { get; }
            public string Id { get; }
        }
    }
}