﻿// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

namespace Dapr.Actors.Client
{
    using System;
    using System.Text.Json;

    /// <summary>
    /// The class containing customizable options for how the Actor Proxy is initialized.
    /// </summary>
    public class ActorProxyOptions
    {
        // TODO: Add actor retry settings

        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        /// <summary>
        /// The constructor
        /// </summary>
        public ActorProxyOptions()
        {
            this.DaprApiToken = Environment.GetEnvironmentVariable(Constants.DaprApiTokenEnvironmentVariable);
        }

        /// <summary>
        /// The <see cref="JsonSerializerOptions"/> used for actor proxy message serialization in non-remoting invocation.
        /// </summary>
        public JsonSerializerOptions JsonSerializerOptions
        {
            get => this.jsonSerializerOptions;
            set => this.jsonSerializerOptions = value ??
                    throw new ArgumentNullException(nameof(JsonSerializerOptions), $"{nameof(ActorProxyOptions)}.{nameof(JsonSerializerOptions)} cannot be null");
        }

        /// <summary>
        /// The Dapr Api Token that is added to the header for all requests.
        /// </summary>
        public string DaprApiToken { get; set; }

        /// <summary>
        /// Gets or sets the HTTP endpoint URI used to communicate with the Dapr sidecar.
        /// </summary>
        /// <remarks>
        /// The URI endpoint to use for HTTP calls to the Dapr runtime. The default value will be 
        /// <c>http://127.0.0.1:DAPR_HTTP_PORT</c> where <c>DAPR_HTTP_PORT</c> represents the value of the 
        /// <c>DAPR_HTTP_PORT</c> environment variable.
        /// </remarks>
        /// <value></value>
        public string HttpEndpoint { get; set; } = GetDefaultHttpEndpoint();

        private static string GetDefaultHttpEndpoint()
        {
            var daprHttpPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500";
            return $"http://127.0.0.1:{daprHttpPort}";
        }
    }
}
