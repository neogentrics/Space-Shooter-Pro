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
using Unity.Services.Lobbies.Http;



namespace Unity.Services.Lobbies.Models
{
    /// <summary>
    /// The body that will be returned for any failing request.  We are using the [RFC 7807 Error Format](https://www.rfc-editor.org/rfc/rfc7807.html#section-3.1).
    /// <param name="type">A URI that identifies the problem type and should provide documentation for the problem.</param>
    /// <param name="status">The HTTP status code of the response.</param>
    /// <param name="title">A short, human-readable summary of the problem type.  It SHOULD NOT change from occurrence to occurrence of the problem, except for purposes of localization.</param>
    /// <param name="detail">A human-readable explanation specific to this occurrence of the problem.</param>
    /// <param name="code">An integer in the range 16000-16999 that uniquely identifies an error type.  This can be used to programatically identify the type of error</param>
    /// <param name="details">A list of additional detail about specific errors.</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ErrorStatus")]
    public class ErrorStatus
    {
        /// <summary>
        /// The body that will be returned for any failing request.  We are using the [RFC 7807 Error Format](https://www.rfc-editor.org/rfc/rfc7807.html#section-3.1).
        /// </summary>
        /// <param name="type">A URI that identifies the problem type and should provide documentation for the problem.</param>
        /// <param name="status">The HTTP status code of the response.</param>
        /// <param name="title">A short, human-readable summary of the problem type.  It SHOULD NOT change from occurrence to occurrence of the problem, except for purposes of localization.</param>
        /// <param name="detail">A human-readable explanation specific to this occurrence of the problem.</param>
        /// <param name="code">An integer in the range 16000-16999 that uniquely identifies an error type.  This can be used to programatically identify the type of error</param>
        /// <param name="details">A list of additional detail about specific errors.</param>
        [Preserve]
        public ErrorStatus(string type = default, int status = default, string title = default, string detail = default, int code = default, List<Detail> details = default)
        {
            Type = type;
            Status = status;
            Title = title;
            Detail = detail;
            Code = code;
            Details = details;
        }

        /// <summary>
        /// A URI that identifies the problem type and should provide documentation for the problem.
        /// </summary>
        [Preserve]
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type{ get; }
        /// <summary>
        /// The HTTP status code of the response.
        /// </summary>
        [Preserve]
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int Status{ get; }
        /// <summary>
        /// A short, human-readable summary of the problem type.  It SHOULD NOT change from occurrence to occurrence of the problem, except for purposes of localization.
        /// </summary>
        [Preserve]
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title{ get; }
        /// <summary>
        /// A human-readable explanation specific to this occurrence of the problem.
        /// </summary>
        [Preserve]
        [DataMember(Name = "detail", EmitDefaultValue = false)]
        public string Detail{ get; }
        /// <summary>
        /// An integer in the range 16000-16999 that uniquely identifies an error type.  This can be used to programatically identify the type of error
        /// </summary>
        [Preserve]
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public int Code{ get; }
        /// <summary>
        /// A list of additional detail about specific errors.
        /// </summary>
        [Preserve]
        [DataMember(Name = "details", EmitDefaultValue = false)]
        public List<Detail> Details{ get; }
    
    }
}
