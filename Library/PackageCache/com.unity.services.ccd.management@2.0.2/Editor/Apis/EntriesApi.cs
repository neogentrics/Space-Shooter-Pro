//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.Ccd.Management.Models;
using Unity.Services.Ccd.Management.Http;
using Unity.Services.Ccd.Management.Entries;

namespace Unity.Services.Ccd.Management.Apis.Entries
{
    /// <summary>
    /// Interface for the EntriesApiClient
    /// </summary>
    internal interface IEntriesApiClient
    {
            /// <summary>
            /// Async Operation.
            /// Create entry.
            /// </summary>
            /// <param name="request">Request object for CreateEntry.</param>
            /// <param name="operationConfiguration">Configuration for CreateEntry.</param>
            /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
            /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<CcdEntry>> CreateEntryAsync(Unity.Services.Ccd.Management.Entries.CreateEntryRequest request, Configuration operationConfiguration = null);

            /// <summary>
            /// Async Operation.
            /// Create or update entry by path.
            /// </summary>
            /// <param name="request">Request object for CreateOrUpdateEntryByPath.</param>
            /// <param name="operationConfiguration">Configuration for CreateOrUpdateEntryByPath.</param>
            /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
            /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<CcdEntry>> CreateOrUpdateEntryByPathAsync(Unity.Services.Ccd.Management.Entries.CreateOrUpdateEntryByPathRequest request, Configuration operationConfiguration = null);

            /// <summary>
            /// Async Operation.
            /// Delete entry.
            /// </summary>
            /// <param name="request">Request object for DeleteEntry.</param>
            /// <param name="operationConfiguration">Configuration for DeleteEntry.</param>
            /// <returns>Task for a Response object containing status code, headers.</returns>
            /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response> DeleteEntryAsync(Unity.Services.Ccd.Management.Entries.DeleteEntryRequest request, Configuration operationConfiguration = null);

            /// <summary>
            /// Async Operation.
            /// Get entries for bucket.
            /// </summary>
            /// <param name="request">Request object for GetEntries.</param>
            /// <param name="operationConfiguration">Configuration for GetEntries.</param>
            /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdEntry&gt; object.</returns>
            /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<List<CcdEntry>>> GetEntriesAsync(Unity.Services.Ccd.Management.Entries.GetEntriesRequest request, Configuration operationConfiguration = null);

            /// <summary>
            /// Async Operation.
            /// Get entry.
            /// </summary>
            /// <param name="request">Request object for GetEntry.</param>
            /// <param name="operationConfiguration">Configuration for GetEntry.</param>
            /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
            /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<CcdEntry>> GetEntryAsync(Unity.Services.Ccd.Management.Entries.GetEntryRequest request, Configuration operationConfiguration = null);

            /// <summary>
            /// Async Operation.
            /// Get entry by path.
            /// </summary>
            /// <param name="request">Request object for GetEntryByPath.</param>
            /// <param name="operationConfiguration">Configuration for GetEntryByPath.</param>
            /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
            /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<CcdEntry>> GetEntryByPathAsync(Unity.Services.Ccd.Management.Entries.GetEntryByPathRequest request, Configuration operationConfiguration = null);

            /// <summary>
            /// Async Operation.
            /// Get entry version.
            /// </summary>
            /// <param name="request">Request object for GetEntryVersion.</param>
            /// <param name="operationConfiguration">Configuration for GetEntryVersion.</param>
            /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
            /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<CcdEntry>> GetEntryVersionAsync(Unity.Services.Ccd.Management.Entries.GetEntryVersionRequest request, Configuration operationConfiguration = null);

            /// <summary>
            /// Async Operation.
            /// Get entry versions.
            /// </summary>
            /// <param name="request">Request object for GetEntryVersions.</param>
            /// <param name="operationConfiguration">Configuration for GetEntryVersions.</param>
            /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdVersion&gt; object.</returns>
            /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<List<CcdVersion>>> GetEntryVersionsAsync(Unity.Services.Ccd.Management.Entries.GetEntryVersionsRequest request, Configuration operationConfiguration = null);

            /// <summary>
            /// Async Operation.
            /// Update entry.
            /// </summary>
            /// <param name="request">Request object for UpdateEntry.</param>
            /// <param name="operationConfiguration">Configuration for UpdateEntry.</param>
            /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
            /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<CcdEntry>> UpdateEntryAsync(Unity.Services.Ccd.Management.Entries.UpdateEntryRequest request, Configuration operationConfiguration = null);

            /// <summary>
            /// Async Operation.
            /// Update entry by path.
            /// </summary>
            /// <param name="request">Request object for UpdateEntryByPath.</param>
            /// <param name="operationConfiguration">Configuration for UpdateEntryByPath.</param>
            /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
            /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<CcdEntry>> UpdateEntryByPathAsync(Unity.Services.Ccd.Management.Entries.UpdateEntryByPathRequest request, Configuration operationConfiguration = null);

    }

    ///<inheritdoc cref="IEntriesApiClient"/>
    internal class EntriesApiClient : BaseApiClient, IEntriesApiClient
    {
        private const int _baseTimeout = 10;
        private Configuration _configuration;
        /// <summary>
        /// Accessor for the client configuration object. This returns a merge
        /// between the current configuration and the global configuration to
        /// ensure the correct combination of headers and a base path (if it is
        /// set) are returned.
        /// </summary>
        public Configuration Configuration
        {
            get {
                // We return a merge between the current configuration and the
                // global configuration to ensure we have the correct
                // combination of headers and a base path (if it is set).
                Configuration globalConfiguration = new Configuration("https://services.unity.com", 10, 4, null);
                if (CcdManagementService.Instance != null)
                {
                    globalConfiguration = CcdManagementService.Instance.Configuration;
                }
                return Configuration.MergeConfigurations(_configuration, globalConfiguration);
            }
            set { _configuration = value; }
        }

        /// <summary>
        /// EntriesApiClient Constructor.
        /// </summary>
        /// <param name="httpClient">The HttpClient for EntriesApiClient.</param>
        /// <param name="configuration"> EntriesApiClient Configuration object.</param>
        public EntriesApiClient(IHttpClient httpClient,
            Configuration configuration = null) : base(httpClient)
        {
            // We don't need to worry about the configuration being null at
            // this stage, we will check this in the accessor.
            _configuration = configuration;

            
        }


        /// <summary>
        /// Async Operation.
        /// Create entry.
        /// </summary>
        /// <param name="request">Request object for CreateEntry.</param>
        /// <param name="operationConfiguration">Configuration for CreateEntry.</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
        /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<CcdEntry>> CreateEntryAsync(Unity.Services.Ccd.Management.Entries.CreateEntryRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(CcdEntry)   },{"400", typeof(Models.ValidationError)   },{"401", typeof(Models.AuthenticationError)   },{"403", typeof(Models.AuthorizationError)   },{"404", typeof(Models.NotFoundError)   },{"429", typeof(Models.TooManyRequestsError)   },{"500", typeof(Models.InternalServerError)   },{"503", typeof(Models.ServiceUnavailableError)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("POST",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }


        /// <summary>
        /// Async Operation.
        /// Create or update entry by path.
        /// </summary>
        /// <param name="request">Request object for CreateOrUpdateEntryByPath.</param>
        /// <param name="operationConfiguration">Configuration for CreateOrUpdateEntryByPath.</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
        /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<CcdEntry>> CreateOrUpdateEntryByPathAsync(Unity.Services.Ccd.Management.Entries.CreateOrUpdateEntryByPathRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(CcdEntry)   },{"400", typeof(Models.ValidationError)   },{"401", typeof(Models.AuthenticationError)   },{"403", typeof(Models.AuthorizationError)   },{"404", typeof(Models.NotFoundError)   },{"429", typeof(Models.TooManyRequestsError)   },{"500", typeof(Models.InternalServerError)   },{"503", typeof(Models.ServiceUnavailableError)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("POST",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }


        /// <summary>
        /// Async Operation.
        /// Delete entry.
        /// </summary>
        /// <param name="request">Request object for DeleteEntry.</param>
        /// <param name="operationConfiguration">Configuration for DeleteEntry.</param>
        /// <returns>Task for a Response object containing status code, headers.</returns>
        /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response> DeleteEntryAsync(Unity.Services.Ccd.Management.Entries.DeleteEntryRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"204",  null },{"400", typeof(Models.ValidationError)   },{"401", typeof(Models.AuthenticationError)   },{"403", typeof(Models.AuthorizationError)   },{"404", typeof(Models.NotFoundError)   },{"429", typeof(Models.TooManyRequestsError)   },{"500", typeof(Models.InternalServerError)   },{"503", typeof(Models.ServiceUnavailableError)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("DELETE",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            ResponseHandler.HandleAsyncResponse(response, statusCodeToTypeMap);
            return new Response(response);
        }


        /// <summary>
        /// Async Operation.
        /// Get entries for bucket.
        /// </summary>
        /// <param name="request">Request object for GetEntries.</param>
        /// <param name="operationConfiguration">Configuration for GetEntries.</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdEntry&gt; object.</returns>
        /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<List<CcdEntry>>> GetEntriesAsync(Unity.Services.Ccd.Management.Entries.GetEntriesRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(List<CcdEntry>)   },{"400", typeof(Models.ValidationError)   },{"401", typeof(Models.AuthenticationError)   },{"403", typeof(Models.AuthorizationError)   },{"404", typeof(Models.NotFoundError)   },{"429", typeof(Models.TooManyRequestsError)   },{"500", typeof(Models.InternalServerError)   },{"503", typeof(Models.ServiceUnavailableError)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<List<CcdEntry>>(response, statusCodeToTypeMap);
            return new Response<List<CcdEntry>>(response, handledResponse);
        }


        /// <summary>
        /// Async Operation.
        /// Get entry.
        /// </summary>
        /// <param name="request">Request object for GetEntry.</param>
        /// <param name="operationConfiguration">Configuration for GetEntry.</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
        /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<CcdEntry>> GetEntryAsync(Unity.Services.Ccd.Management.Entries.GetEntryRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(CcdEntry)   },{"400", typeof(Models.ValidationError)   },{"401", typeof(Models.AuthenticationError)   },{"403", typeof(Models.AuthorizationError)   },{"404", typeof(Models.NotFoundError)   },{"429", typeof(Models.TooManyRequestsError)   },{"500", typeof(Models.InternalServerError)   },{"503", typeof(Models.ServiceUnavailableError)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }


        /// <summary>
        /// Async Operation.
        /// Get entry by path.
        /// </summary>
        /// <param name="request">Request object for GetEntryByPath.</param>
        /// <param name="operationConfiguration">Configuration for GetEntryByPath.</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
        /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<CcdEntry>> GetEntryByPathAsync(Unity.Services.Ccd.Management.Entries.GetEntryByPathRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(CcdEntry)   },{"400", typeof(Models.ValidationError)   },{"401", typeof(Models.AuthenticationError)   },{"403", typeof(Models.AuthorizationError)   },{"404", typeof(Models.NotFoundError)   },{"429", typeof(Models.TooManyRequestsError)   },{"500", typeof(Models.InternalServerError)   },{"503", typeof(Models.ServiceUnavailableError)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }


        /// <summary>
        /// Async Operation.
        /// Get entry version.
        /// </summary>
        /// <param name="request">Request object for GetEntryVersion.</param>
        /// <param name="operationConfiguration">Configuration for GetEntryVersion.</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
        /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<CcdEntry>> GetEntryVersionAsync(Unity.Services.Ccd.Management.Entries.GetEntryVersionRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(CcdEntry)   },{"400", typeof(Models.ValidationError)   },{"401", typeof(Models.AuthenticationError)   },{"403", typeof(Models.AuthorizationError)   },{"404", typeof(Models.NotFoundError)   },{"429", typeof(Models.TooManyRequestsError)   },{"500", typeof(Models.InternalServerError)   },{"503", typeof(Models.ServiceUnavailableError)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }


        /// <summary>
        /// Async Operation.
        /// Get entry versions.
        /// </summary>
        /// <param name="request">Request object for GetEntryVersions.</param>
        /// <param name="operationConfiguration">Configuration for GetEntryVersions.</param>
        /// <returns>Task for a Response object containing status code, headers, and List&lt;CcdVersion&gt; object.</returns>
        /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<List<CcdVersion>>> GetEntryVersionsAsync(Unity.Services.Ccd.Management.Entries.GetEntryVersionsRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(List<CcdVersion>)   },{"400", typeof(Models.ValidationError)   },{"401", typeof(Models.AuthenticationError)   },{"403", typeof(Models.AuthorizationError)   },{"404", typeof(Models.NotFoundError)   },{"429", typeof(Models.TooManyRequestsError)   },{"500", typeof(Models.InternalServerError)   },{"503", typeof(Models.ServiceUnavailableError)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<List<CcdVersion>>(response, statusCodeToTypeMap);
            return new Response<List<CcdVersion>>(response, handledResponse);
        }


        /// <summary>
        /// Async Operation.
        /// Update entry.
        /// </summary>
        /// <param name="request">Request object for UpdateEntry.</param>
        /// <param name="operationConfiguration">Configuration for UpdateEntry.</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
        /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<CcdEntry>> UpdateEntryAsync(Unity.Services.Ccd.Management.Entries.UpdateEntryRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(CcdEntry)   },{"400", typeof(Models.ValidationError)   },{"401", typeof(Models.AuthenticationError)   },{"403", typeof(Models.AuthorizationError)   },{"404", typeof(Models.NotFoundError)   },{"429", typeof(Models.TooManyRequestsError)   },{"500", typeof(Models.InternalServerError)   },{"503", typeof(Models.ServiceUnavailableError)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("PUT",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }


        /// <summary>
        /// Async Operation.
        /// Update entry by path.
        /// </summary>
        /// <param name="request">Request object for UpdateEntryByPath.</param>
        /// <param name="operationConfiguration">Configuration for UpdateEntryByPath.</param>
        /// <returns>Task for a Response object containing status code, headers, and CcdEntry object.</returns>
        /// <exception cref="Unity.Services.Ccd.Management.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<CcdEntry>> UpdateEntryByPathAsync(Unity.Services.Ccd.Management.Entries.UpdateEntryByPathRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(CcdEntry)   },{"400", typeof(Models.ValidationError)   },{"401", typeof(Models.AuthenticationError)   },{"403", typeof(Models.AuthorizationError)   },{"404", typeof(Models.NotFoundError)   },{"429", typeof(Models.TooManyRequestsError)   },{"500", typeof(Models.InternalServerError)   },{"503", typeof(Models.ServiceUnavailableError)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("PUT",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CcdEntry>(response, statusCodeToTypeMap);
            return new Response<CcdEntry>(response, handledResponse);
        }

    }
}