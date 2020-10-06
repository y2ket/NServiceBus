﻿namespace NServiceBus.AcceptanceTests.Core.FakeTransport
{
    using System;
    using System.Collections.Generic;
    using Settings;
    using Transport;

    public class FakeTransport : TransportDefinition
    {
        public override bool RequiresConnectionString => false;

        public override string ExampleConnectionStringForErrorMessage => null;

        public TransportTransactionMode? SupportedTransactionMode { get; set; }

        public List<string> StartUpSequence { get; } = new List<string>();

        public bool ThrowOnInfrastructureStop { get; set; }

        public bool RaiseCriticalErrorDuringStartup { get; set; }

        public bool ThrowOnPumpStop { get; set; }

        public Exception ExceptionToThrow { get; set; } = new Exception();

        public Action<QueueBindings> OnQueueCreation { get; set; }

        public override TransportInfrastructure Initialize(TransportSettings settings)
        {
            StartUpSequence.Add($"{nameof(TransportDefinition)}.{nameof(Initialize)}");

            //if (settings.TryGet<Action<ReadOnlySettings>>("FakeTransport.AssertSettings", out var assertion))
            //{
            //    assertion(settings);
            //}

            return new FakeTransportInfrastructure(settings, this);
        }
    }
}