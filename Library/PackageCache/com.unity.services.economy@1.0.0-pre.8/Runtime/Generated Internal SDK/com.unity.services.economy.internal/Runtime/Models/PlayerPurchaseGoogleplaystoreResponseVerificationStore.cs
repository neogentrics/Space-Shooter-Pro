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
using Unity.Services.Economy.Internal.Http;



namespace Unity.Services.Economy.Internal.Models
{
    /// <summary>
    /// Details from the receipt validation service.
    /// </summary>
    [Preserve]
    [DataContract(Name = "player_purchase_googleplaystore_response_verification_store")]
    internal class PlayerPurchaseGoogleplaystoreResponseVerificationStore
    {
        /// <summary>
        /// Details from the receipt validation service.
        /// </summary>
        /// <param name="receipt">Receipt that was sent in the request</param>
        [Preserve]
        public PlayerPurchaseGoogleplaystoreResponseVerificationStore(string receipt = default)
        {
            Receipt = receipt;
        }

        /// <summary>
        /// Receipt that was sent in the request
        /// </summary>
        [Preserve]
        [DataMember(Name = "receipt", EmitDefaultValue = false)]
        public string Receipt{ get; }
    
    }
}

