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
using Unity.Services.Economy.Internal.Models;
using Unity.Services.Economy.Internal.Http;
using Unity.Services.Authentication.Internal;
using Unity.Services.Economy.Internal.Currencies;

namespace Unity.Services.Economy.Internal.Apis.Currencies
{
    /// <summary>
    /// Interface for the CurrenciesApiClient
    /// </summary>
    internal interface ICurrenciesApiClient
    {
            /// <summary>
            /// Async Operation.
            /// Decrement Currency Balance.
            /// </summary>
            /// <param name="request">Request object for DecrementPlayerCurrencyBalance.</param>
            /// <param name="operationConfiguration">Configuration for DecrementPlayerCurrencyBalance.</param>
            /// <returns>Task for a Response object containing status code, headers, and CurrencyBalanceResponse object.</returns>
            /// <exception cref="Unity.Services.Economy.Internal.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<CurrencyBalanceResponse>> DecrementPlayerCurrencyBalanceAsync(DecrementPlayerCurrencyBalanceRequest request, Configuration operationConfiguration = null);

            /// <summary>
            /// Async Operation.
            /// Player Currency Balances.
            /// </summary>
            /// <param name="request">Request object for GetPlayerCurrencies.</param>
            /// <param name="operationConfiguration">Configuration for GetPlayerCurrencies.</param>
            /// <returns>Task for a Response object containing status code, headers, and PlayerCurrencyBalanceResponse object.</returns>
            /// <exception cref="Unity.Services.Economy.Internal.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<PlayerCurrencyBalanceResponse>> GetPlayerCurrenciesAsync(GetPlayerCurrenciesRequest request, Configuration operationConfiguration = null);

            /// <summary>
            /// Async Operation.
            /// Increment Currency Balance.
            /// </summary>
            /// <param name="request">Request object for IncrementPlayerCurrencyBalance.</param>
            /// <param name="operationConfiguration">Configuration for IncrementPlayerCurrencyBalance.</param>
            /// <returns>Task for a Response object containing status code, headers, and CurrencyBalanceResponse object.</returns>
            /// <exception cref="Unity.Services.Economy.Internal.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<CurrencyBalanceResponse>> IncrementPlayerCurrencyBalanceAsync(IncrementPlayerCurrencyBalanceRequest request, Configuration operationConfiguration = null);

            /// <summary>
            /// Async Operation.
            /// Set Currency Balance.
            /// </summary>
            /// <param name="request">Request object for SetPlayerCurrencyBalance.</param>
            /// <param name="operationConfiguration">Configuration for SetPlayerCurrencyBalance.</param>
            /// <returns>Task for a Response object containing status code, headers, and CurrencyBalanceResponse object.</returns>
            /// <exception cref="Unity.Services.Economy.Internal.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
            Task<Response<CurrencyBalanceResponse>> SetPlayerCurrencyBalanceAsync(SetPlayerCurrencyBalanceRequest request, Configuration operationConfiguration = null);

    }

    ///<inheritdoc cref="ICurrenciesApiClient"/>
    internal class CurrenciesApiClient : BaseApiClient, ICurrenciesApiClient
    {
        private IAccessToken _accessToken;
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
                Configuration globalConfiguration = new Configuration("https://economy.services.api.unity.com", 10, 4, null);
                if (EconomyService.Instance != null)
                {
                    globalConfiguration = EconomyService.Instance.Configuration;
                }
                return Configuration.MergeConfigurations(_configuration, globalConfiguration);
            }
        }

        /// <summary>
        /// CurrenciesApiClient Constructor.
        /// </summary>
        /// <param name="httpClient">The HttpClient for CurrenciesApiClient.</param>
        /// <param name="accessToken">The Authentication token for the client.</param>
        /// <param name="configuration"> CurrenciesApiClient Configuration object.</param>
        public CurrenciesApiClient(IHttpClient httpClient,
            IAccessToken accessToken,
            Configuration configuration = null) : base(httpClient)
        {
            // We don't need to worry about the configuration being null at
            // this stage, we will check this in the accessor.
            _configuration = configuration;

            _accessToken = accessToken;
        }


        /// <summary>
        /// Async Operation.
        /// Decrement Currency Balance.
        /// </summary>
        /// <param name="request">Request object for DecrementPlayerCurrencyBalance.</param>
        /// <param name="operationConfiguration">Configuration for DecrementPlayerCurrencyBalance.</param>
        /// <returns>Task for a Response object containing status code, headers, and CurrencyBalanceResponse object.</returns>
        /// <exception cref="Unity.Services.Economy.Internal.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<CurrencyBalanceResponse>> DecrementPlayerCurrencyBalanceAsync(DecrementPlayerCurrencyBalanceRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(CurrencyBalanceResponse)   },{"400", typeof(MakeVirtualPurchase400OneOf)   },{"403", typeof(BasicErrorResponse)   },{"404", typeof(BasicErrorResponse)   },{"409", typeof(ErrorResponseConflictCurrencyBalance)   },{"422", typeof(BasicErrorResponse)   },{"429", typeof(BasicErrorResponse)   },{"503", typeof(BasicErrorResponse)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("POST",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(_accessToken, finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CurrencyBalanceResponse>(response, statusCodeToTypeMap);
            return new Response<CurrencyBalanceResponse>(response, handledResponse);
        }


        /// <summary>
        /// Async Operation.
        /// Player Currency Balances.
        /// </summary>
        /// <param name="request">Request object for GetPlayerCurrencies.</param>
        /// <param name="operationConfiguration">Configuration for GetPlayerCurrencies.</param>
        /// <returns>Task for a Response object containing status code, headers, and PlayerCurrencyBalanceResponse object.</returns>
        /// <exception cref="Unity.Services.Economy.Internal.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<PlayerCurrencyBalanceResponse>> GetPlayerCurrenciesAsync(GetPlayerCurrenciesRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(PlayerCurrencyBalanceResponse)   },{"403", typeof(BasicErrorResponse)   },{"404", typeof(BasicErrorResponse)   },{"429", typeof(BasicErrorResponse)   },{"503", typeof(BasicErrorResponse)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("GET",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(_accessToken, finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<PlayerCurrencyBalanceResponse>(response, statusCodeToTypeMap);
            return new Response<PlayerCurrencyBalanceResponse>(response, handledResponse);
        }


        /// <summary>
        /// Async Operation.
        /// Increment Currency Balance.
        /// </summary>
        /// <param name="request">Request object for IncrementPlayerCurrencyBalance.</param>
        /// <param name="operationConfiguration">Configuration for IncrementPlayerCurrencyBalance.</param>
        /// <returns>Task for a Response object containing status code, headers, and CurrencyBalanceResponse object.</returns>
        /// <exception cref="Unity.Services.Economy.Internal.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<CurrencyBalanceResponse>> IncrementPlayerCurrencyBalanceAsync(IncrementPlayerCurrencyBalanceRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(CurrencyBalanceResponse)   },{"400", typeof(MakeVirtualPurchase400OneOf)   },{"403", typeof(BasicErrorResponse)   },{"404", typeof(BasicErrorResponse)   },{"409", typeof(ErrorResponseConflictCurrencyBalance)   },{"422", typeof(BasicErrorResponse)   },{"429", typeof(BasicErrorResponse)   },{"503", typeof(BasicErrorResponse)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("POST",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(_accessToken, finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CurrencyBalanceResponse>(response, statusCodeToTypeMap);
            return new Response<CurrencyBalanceResponse>(response, handledResponse);
        }


        /// <summary>
        /// Async Operation.
        /// Set Currency Balance.
        /// </summary>
        /// <param name="request">Request object for SetPlayerCurrencyBalance.</param>
        /// <param name="operationConfiguration">Configuration for SetPlayerCurrencyBalance.</param>
        /// <returns>Task for a Response object containing status code, headers, and CurrencyBalanceResponse object.</returns>
        /// <exception cref="Unity.Services.Economy.Internal.Http.HttpException">An exception containing the HttpClientResponse with headers, response code, and string of error.</exception>
        public async Task<Response<CurrencyBalanceResponse>> SetPlayerCurrencyBalanceAsync(SetPlayerCurrencyBalanceRequest request,
            Configuration operationConfiguration = null)
        {
            var statusCodeToTypeMap = new Dictionary<string, System.Type>() { {"200", typeof(CurrencyBalanceResponse)   },{"400", typeof(MakeVirtualPurchase400OneOf)   },{"403", typeof(BasicErrorResponse)   },{"404", typeof(BasicErrorResponse)   },{"409", typeof(ErrorResponseConflictCurrencyBalance)   },{"422", typeof(BasicErrorResponse)   },{"429", typeof(BasicErrorResponse)   },{"503", typeof(BasicErrorResponse)   } };

            // Merge the operation/request level configuration with the client level configuration.
            var finalConfiguration = Configuration.MergeConfigurations(operationConfiguration, Configuration);

            var response = await HttpClient.MakeRequestAsync("PUT",
                request.ConstructUrl(finalConfiguration.BasePath),
                request.ConstructBody(),
                request.ConstructHeaders(_accessToken, finalConfiguration),
                finalConfiguration.RequestTimeout ?? _baseTimeout);

            var handledResponse = ResponseHandler.HandleAsyncResponse<CurrencyBalanceResponse>(response, statusCodeToTypeMap);
            return new Response<CurrencyBalanceResponse>(response, handledResponse);
        }

    }
}