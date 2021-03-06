﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IServiceOriented.ServiceBus.Threading;
using IServiceOriented.ServiceBus.Collections;

namespace IServiceOriented.ServiceBus
{
    /// <summary>
    /// Represents a request to publish a message
    /// </summary>
    public class PublishRequest
    {
        protected PublishRequest()
        {
            PublishRequestId = Guid.NewGuid().ToString();
        }
        
        public PublishRequest(Type contract, string action, object message) :
            this(contract, action, message, new MessageDeliveryContext())
        {
        }
        public PublishRequest(Type contract, string action, object message, MessageDeliveryContext context) : this()
        {
            ContractType = contract;
            Action = action;
            Message = message;
            Context = context;
        }

        public string PublishRequestId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of the contract associated with the message
        /// </summary>
        public Type ContractType
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets the action associated with the message
        /// </summary>
        public string Action
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the message to publish
        /// </summary>
        public object Message
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the contex associated with the message
        /// </summary>
        public MessageDeliveryContext Context
        {
            get;
            private set;
        }


        public static PublishRequest Copy(PublishRequest original, params KeyValuePair<MessageDeliveryContextKey, object>[] additionalContext)
        {
            Dictionary<MessageDeliveryContextKey, object> editableContext = original.Context.ToDictionary();
            foreach (var c in additionalContext)
            {
                editableContext.Add(c.Key, c.Value);
            }
            return new PublishRequest(original.ContractType, original.Action, original.Message, new MessageDeliveryContext(editableContext));
        }

    }
}
