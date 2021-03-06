//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using UnityEngine;
using System.Threading.Tasks;

using Unity.Services.CloudSave.Internal.Apis.Data;

using Unity.Services.CloudSave.Internal.Http;
using Unity.Services.Core.Internal;
using Unity.Services.Authentication.Internal;

namespace Unity.Services.CloudSave.Internal
{
    internal class CloudSaveServiceProvider : IInitializablePackage
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        {
            // Pass an instance of this class to Core
            var generatedPackageRegistry =
            CoreRegistry.Instance.RegisterPackage(new CloudSaveServiceProvider());
                // And specify what components it requires, or provides.
            generatedPackageRegistry.DependsOn<IAccessToken>();
;
        }

        public Task Initialize(CoreRegistry registry)
        {
            var httpClient = new HttpClient();

            var accessTokenCloudSave = registry.GetServiceComponent<IAccessToken>();

            if (accessTokenCloudSave != null)
            {
                CloudSaveService.Instance =
                    new InternalCloudSaveService(httpClient, registry.GetServiceComponent<IAccessToken>());
            }

            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// InternalCloudSaveService
    /// </summary>
    internal class InternalCloudSaveService : ICloudSaveService
    {
        /// <summary>
        /// Constructor for InternalCloudSaveService
        /// </summary>
        /// <param name="httpClient">The HttpClient for InternalCloudSaveService.</param>
        /// <param name="accessToken">The Authentication token for the service.</param>
        public InternalCloudSaveService(HttpClient httpClient, IAccessToken accessToken = null)
        {
            
            DataApi = new DataApiClient(httpClient, accessToken);
            
            Configuration = new Configuration("https://cloud-save.services.api.unity.com", 10, 4, null);
        }
        
        /// <summary> Instance of IDataApiClient interface</summary>
        public IDataApiClient DataApi { get; set; }
        
        /// <summary> Configuration properties for the service.</summary>
        public Configuration Configuration { get; set; }
    }
}
