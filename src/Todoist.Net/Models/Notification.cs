using System;
using System.Collections.Generic;
using System.Dynamic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist live notification.
    /// </summary>
    public class Notification : DynamicObject
    {
        private readonly Dictionary<string, object> _dictionary = new Dictionary<string, object>();

        /// <summary>
        /// Gets the created.
        /// </summary>
        /// <value>The created.</value>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }

        /// <summary>
        /// Gets from uid.
        /// </summary>
        /// <value>From uid.</value>
        [JsonProperty("from_uid")]
        public string FromUid { get; internal set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// Gets the notification key.
        /// </summary>
        /// <value>The notification key.</value>
        [JsonProperty("notification_key")]
        public string NotificationKey { get; internal set; }

        /// <summary>
        /// Gets the type of the notification.
        /// </summary>
        /// <value>The type of the notification.</value>
        [JsonProperty("notification_type")]
        public string NotificationType { get; internal set; }

        /// <summary>
        /// Gets the seq no.
        /// </summary>
        /// <value>The seq no.</value>
        [JsonProperty("seq_no")]
        public long SeqNo { get; internal set; }

        /// <summary>
        /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)
        /// </returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _dictionary.TryGetValue(binder.Name, out result);
        }

        /// <summary>
        /// Provides the implementation for operations that set member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as setting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, the <paramref name="value" /> is "Test".</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
        /// </returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dictionary[binder.Name] = value;

            return true;
        }
    }
}
