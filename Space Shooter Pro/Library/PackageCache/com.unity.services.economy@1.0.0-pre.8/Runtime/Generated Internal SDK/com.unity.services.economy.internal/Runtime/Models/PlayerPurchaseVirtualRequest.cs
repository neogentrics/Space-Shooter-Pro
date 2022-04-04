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
    /// PlayerPurchaseVirtualRequest model
    /// </summary>
    [Preserve]
    [DataContract(Name = "player-purchase-virtual-request")]
    internal class PlayerPurchaseVirtualRequest
    {
        /// <summary>
        /// Creates an instance of PlayerPurchaseVirtualRequest.
        /// </summary>
        /// <param name="id">ID of the purchase.</param>
        /// <param name="playersInventoryItemIds">IDs of the player&#39;s inventory items that should be used for any item costs associated with the purchase.</param>
        [Preserve]
        public PlayerPurchaseVirtualRequest(string id, List<string> playersInventoryItemIds = default)
        {
            Id = id;
            PlayersInventoryItemIds = playersInventoryItemIds;
        }

        /// <summary>
        /// ID of the purchase.
        /// </summary>
        [Preserve]
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
        public string Id{ get; }
        /// <summary>
        /// IDs of the player&#39;s inventory items that should be used for any item costs associated with the purchase.
        /// </summary>
        [Preserve]
        [DataMember(Name = "playersInventoryItemIds", EmitDefaultValue = false)]
        public List<string> PlayersInventoryItemIds{ get; }
    
    }
}

