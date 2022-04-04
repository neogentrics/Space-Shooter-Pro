using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Services.Ccd.Management.Apis.Badges;
using Unity.Services.Ccd.Management.Apis.Buckets;
using Unity.Services.Ccd.Management.Apis.Content;
using Unity.Services.Ccd.Management.Apis.Default;
using Unity.Services.Ccd.Management.Apis.Entries;
using Unity.Services.Ccd.Management.Apis.Orgs;
using Unity.Services.Ccd.Management.Apis.Permissions;
using Unity.Services.Ccd.Management.Apis.Releases;
using Unity.Services.Ccd.Management.Apis.Users;
using Unity.Services.Ccd.Management.Http;
using UnityEditor;

[assembly: InternalsVisibleTo("Unity.Services.Ccd.Management.Editor.Tests")]

namespace Unity.Services.Ccd.Management
{
    /// <summary>
    /// Here is the first point and call for accessing the CCD Management Package's features!
    /// Use the .Instance method to get a singleton of the ICCDManagementServiceSDK and from there you can make various requests to the CCD Management service API.
    /// Note: Your project must have cloud services enabled and connected to a Unity Cloud Project.
    /// </summary>
    public static class CcdManagement
    {
        private static ICcdManagementServiceSdk service;

        private static readonly Configuration configuration;
        private static IHttpClient client;

        internal static string projectid;

        /// <summary>
        /// Sets the configuration base path
        /// </summary>
        /// <param name="basePath">The base path to be set for the configuration.</param>
        public static void SetBasePath(string basePath)
        {
            configuration.BasePath = basePath;
        }

        static CcdManagement()
        {
            configuration = new Configuration("https://services.unity.com", 10, 4, new Dictionary<string, string>());
        }

        /// <summary>
        /// Provides the CCD Management Service SDK interface for making service API requests.
        /// </summary>
        public static ICcdManagementServiceSdk Instance
        {
            get
            {
                //Update project id every time new instance is retrieved to avoid stale caching on static object.
                projectid = CloudProjectSettings.projectId;

                if (service == null)
                {
                    // Need to initialize here without using UnityServices.InitializeAsync due to these features being mainly Editor specific.
                    client = new HttpClient();
                    service = new WrappedCcdManagementService(
                        new BadgesApiClient(client),
                        new BucketsApiClient(client),
                        new ContentApiClient(client),
                        new DefaultApiClient(client),
                        new EntriesApiClient(client),
                        new OrgsApiClient(client),
                        new PermissionsApiClient(client),
                        new ReleasesApiClient(client),
                        new UsersApiClient(client),
                        configuration, client);
                }
                return service;
            }
        }
    }
}
