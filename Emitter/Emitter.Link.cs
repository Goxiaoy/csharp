﻿using System;
using System.Collections.Generic;
using System.Text;
using Emitter.Messages;

namespace Emitter
{
    public partial class Connection
    {
        /// <summary>
        /// Creates a short 2-character link for the channel. Uses the default key that should be specified in the constructor.
        /// </summary>
        /// <param name="channel">The channel to link to.</param>
        /// <param name="name">The name of the link to create.</param>
        /// <param name="subscribe">Automatically subscribe to this link.</param>
        /// <param name="options">Channel options to associate with the link (ex: ttl)</param>
        public void Link(string channel, string name, bool subscribe, params string[] options)
        {
            if (this.DefaultKey == null)
                throw EmitterException.NoDefaultKey;

            this.Link(this.DefaultKey, channel, name, subscribe, options);
        }

        /// <summary>
        /// Creates a short 2-character link for the channel. Uses the default key that should be specified in the constructor.
        /// </summary>
        /// <param name="key">The channel key.</param>
        /// <param name="channel">The channel to link to.</param>
        /// <param name="name">The name of the link to create.</param>
        /// <param name="subscribe">Automatically subscribe to this link.</param>
        /// <param name="options">Channel options to associate with the link (ex: ttl)</param>
        public void Link(string key, string channel, string name, bool subscribe, params string[] options)
        {
            var request = new LinkRequest();
            request.Key = key;
            request.Channel = this.FormatChannelLink(channel, options);
            request.Name = name;
            request.Subscribe = subscribe;

            this.Publish("emitter/", "link", Encoding.UTF8.GetBytes(request.ToJson()),"+1");
        }

        /// <summary>
        /// Publishes to a link.
        /// </summary>
        /// <param name="name">The name of the link.</param>
        /// <param name="message">The message to publish.</param>
        /// <returns></returns>
        public ushort PublishWithLink(string name, byte[] message)
        {
            return this.Client.Publish(name, message);
        }


        /// <summary>
        /// Publishes to a link.
        /// </summary>
        /// <param name="name">The name of the link.</param>
        /// <param name="message">The message to publish.</param>
        /// <returns></returns>
        public ushort PublishWithLink(string name, string message)
        {
            return this.Client.Publish(name, Encoding.UTF8.GetBytes(message));
        }
    }
}
