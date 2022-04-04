//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Services.CloudSave.Internal.Http;



namespace Unity.Services.CloudSave.Internal.Models
{
    /// <summary>
    /// Single error in the Batch Validation Error Response.
    /// <param name="field">field param</param>
    /// <param name="messages">messages param</param>
    /// <param name="key">key param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "batch-validation-error-body")]
    internal class BatchValidationErrorBody
    {
        /// <summary>
        /// Single error in the Batch Validation Error Response.
        /// </summary>
        /// <param name="field">field param</param>
        /// <param name="messages">messages param</param>
        /// <param name="key">key param</param>
        [Preserve]
        public BatchValidationErrorBody(string field, List<string> messages, string key)
        {
            Field = field;
            Messages = messages;
            Key = key;
        }

        /// <summary>
        /// 
        /// </summary>
        [Preserve]
        [DataMember(Name = "field", IsRequired = true, EmitDefaultValue = true)]
        public string Field{ get; }
        /// <summary>
        /// 
        /// </summary>
        [Preserve]
        [DataMember(Name = "messages", IsRequired = true, EmitDefaultValue = true)]
        public List<string> Messages{ get; }
        /// <summary>
        /// 
        /// </summary>
        [Preserve]
        [DataMember(Name = "key", IsRequired = true, EmitDefaultValue = true)]
        public string Key{ get; }
    
    }
}
