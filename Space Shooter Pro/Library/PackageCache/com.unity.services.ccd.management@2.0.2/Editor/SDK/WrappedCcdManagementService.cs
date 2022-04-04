using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Ccd.Management.Apis.Badges;
using Unity.Services.Ccd.Management.Apis.Buckets;
using Unity.Services.Ccd.Management.Apis.Content;
using Unity.Services.Ccd.Management.Apis.Default;
using Unity.Services.Ccd.Management.Apis.Entries;
using Unity.Services.Ccd.Management.Apis.Orgs;
using Unity.Services.Ccd.Management.Apis.Permissions;
using Unity.Services.Ccd.Management.Apis.Releases;
using Unity.Services.Ccd.Management.Apis.Users;
using Unity.Services.Ccd.Management.Badges;
using Unity.Services.Ccd.Management.Buckets;
using Unity.Services.Ccd.Management.Content;
using Unity.Services.Ccd.Management.Entries;
using Unity.Services.Ccd.Management.Http;
using Unity.Services.Ccd.Management.Models;
using Unity.Services.Ccd.Management.Orgs;
using Unity.Services.Ccd.Management.Permissions;
using Unity.Services.Ccd.Management.Releases;
using Unity.Services.Ccd.Management.Users;
using Unity.Services.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

[assembly: InternalsVisibleTo("Unity.Services.Ccd.Management.Editor.Tests")]
namespace Unity.Services.Ccd.Management
{
    /// <summary>
    /// The CCD Management enables clients to manage CCD resources.
    /// </summary>
    internal class WrappedCcdManagementService : ICcdManagementServiceSdk, ICcdManagementServiceSdkConfiguration
    {
        internal IBadgesApiClient BadgesApiClient;
        internal IBucketsApiClient BucketsApiClient;
        internal IContentApiClient ContentApiClient;
        internal IDefaultApiClient DefaultApiClient;
        internal IEntriesApiClient EntriesApiClient;
        internal IOrgsApiClient OrgsApiClient;
        internal IPermissionsApiClient PermissionsApiClient;
        internal IReleasesApiClient ReleasesApiClient;
        internal IUsersApiClient UsersApiClient;
        internal Configuration Configuration;
        private IHttpClient _HttpClient;
        private TusClient _TusClient;

        //CCD Management Error base value (used to elevate standard errors if unhandled)
        internal const int CCD_MANAGEMENT_ERROR_BASE_VALUE = 19000;
        internal const string AUTH_HEADER = "Authorization";
        internal const string CONTENT_TYPE_HEADER = "Content-Type";
        internal const string CONTENT_LENGTH_HEADER = "Content-Length";
        internal const string SERVICES_ERROR_MSG = "Cloud Services must enabled and connected to a Unity Cloud Project.";

        internal WrappedCcdManagementService(
            IBadgesApiClient badgesApiClient, IBucketsApiClient bucketsApiClient, IContentApiClient contentApiClient,
            IDefaultApiClient defaultApiClient, IEntriesApiClient entriesApiClient, IOrgsApiClient orgsApiClient,
            IPermissionsApiClient permissionsApiClient, IReleasesApiClient releasesApiClient, IUsersApiClient usersApiClient,
            Configuration configuration, IHttpClient httpClient)
        {
            BadgesApiClient = badgesApiClient;
            BucketsApiClient = bucketsApiClient;
            ContentApiClient = contentApiClient;
            DefaultApiClient = defaultApiClient;
            EntriesApiClient = entriesApiClient;
            OrgsApiClient = orgsApiClient;
            PermissionsApiClient = permissionsApiClient;
            ReleasesApiClient = releasesApiClient;
            UsersApiClient = usersApiClient;
            Configuration = configuration;
            _HttpClient = httpClient;
            _TusClient = new TusClient();
        }

        public async Task DeleteBadgeAsync(Guid bucketId, string badgeName)
        {
            var request = new DeleteBadgeRequest(bucketId.ToString(), badgeName, CcdManagement.projectid);
            await TryCatchRequest(BadgesApiClient.DeleteBadgeAsync, request);
        }

        public async Task<CcdBadge> GetBadgeAsync(Guid bucketId, string badgeName)
        {
            var request = new GetBadgeRequest(bucketId.ToString(), badgeName, CcdManagement.projectid);
            var response = await TryCatchRequest(BadgesApiClient.GetBadgeAsync, request);
            return response.Result;
        }

        public async Task<List<CcdBadge>> ListBadgesAsync(Guid bucketId, PageOptions pageOptions = default)
        {
            if (pageOptions == null)
            {
                pageOptions = new PageOptions();
            }
            var request = new ListBadgesRequest(bucketId.ToString(), CcdManagement.projectid, pageOptions.Page, pageOptions.PerPage);
            var response = await TryCatchRequest(BadgesApiClient.ListBadgesAsync, request);
            return response.Result;
        }

        public async Task<CcdBadge> AssignBadgeAsync(AssignBadgeOptions updateBadgeOptions)
        {
            bool hasReleaseId = updateBadgeOptions.ReleaseId != default;
            bool hasReleaseNum = updateBadgeOptions.ReleaseNum.HasValue;
            CcdBadgeAssign badgeAssign;
            if (hasReleaseId)
            {
                badgeAssign = new CcdBadgeAssign(updateBadgeOptions.BadgeName, updateBadgeOptions.ReleaseId.ToString());
            }
            else if (hasReleaseNum)
            {
                badgeAssign = new CcdBadgeAssign(updateBadgeOptions.BadgeName, null, updateBadgeOptions.ReleaseNum.ToString());
            }
            else
            {
                throw new CcdManagementException(CcdManagementErrorCodes.InvalidArgument, "Cannot have both ReleaseId and ReleaseNum present.");
            }
            var request = new UpdateBadgeRequest(updateBadgeOptions.BucketId.ToString(), CcdManagement.projectid, badgeAssign);
            var response = await TryCatchRequest(BadgesApiClient.UpdateBadgeAsync, request);
            return response.Result;
        }

        public async Task<CcdBucket> CreateBucketAsync(CreateBucketOptions createBucketOptions)
        {
            var request = new CreateBucketByProjectRequest(CcdManagement.projectid,
                new CcdBucketCreate(createBucketOptions.Name, Guid.Parse(CcdManagement.projectid), createBucketOptions.Description, false));
            var response = await TryCatchRequest(BucketsApiClient.CreateBucketByProjectAsync, request);
            return response.Result;
        }

        public async Task DeleteBucketAsync(Guid bucketId)
        {
            var request = new DeleteBucketRequest(bucketId.ToString(), CcdManagement.projectid);
            await TryCatchRequest(BucketsApiClient.DeleteBucketAsync, request);
        }

        public async Task<CcdBucket> GetBucketAsync(Guid bucketId)
        {
            var request = new GetBucketRequest(bucketId.ToString(), CcdManagement.projectid);
            var response = await TryCatchRequest(BucketsApiClient.GetBucketAsync, request);
            return response.Result;
        }

        public async Task<CcdReleaseChangeVersion> GetDiffAsync(Guid bucketId)
        {
            var request = new GetDiffRequest(bucketId.ToString(), CcdManagement.projectid);
            var response = await TryCatchRequest(BucketsApiClient.GetDiffAsync, request);
            return response.Result;
        }

        public async Task<List<CcdReleaseEntry>> GetDiffEntriesAsync(DiffEntriesOptions diffEntriesOptions, PageOptions pageOptions)
        {
            if (pageOptions == null)
            {
                pageOptions = new PageOptions();
            }

            var request = new GetDiffEntriesRequest(
                diffEntriesOptions.BucketId.ToString(),
                CcdManagement.projectid,
                pageOptions.Page,
                pageOptions.PerPage,
                diffEntriesOptions.Path,
                diffEntriesOptions.IncludeStates);
            var response = await TryCatchRequest(BucketsApiClient.GetDiffEntriesAsync, request);
            return response.Result;
        }

        public async Task<CcdRelease> PromoteBucketAsync(PromoteBucketOptions promoteBucketOptions)
        {
            var request = new PromoteBucketRequest(promoteBucketOptions.BucketId.ToString(), CcdManagement.projectid,
                new CcdPromoteBucket(promoteBucketOptions.FromRelease, promoteBucketOptions.BucketId, promoteBucketOptions.Notes));
            var response = await TryCatchRequest(BucketsApiClient.PromoteBucketAsync, request);
            return response.Result;
        }

        public async Task<CcdBucket> UpdateBucketAsync(UpdateBucketOptions updateBucketOptions)
        {
            var request = new UpdateBucketRequest(updateBucketOptions.BucketId.ToString(), CcdManagement.projectid,
                new CcdBucketUpdate(updateBucketOptions.Description, updateBucketOptions.Name));
            var response = await TryCatchRequest(BucketsApiClient.UpdateBucketAsync, request);
            return response.Result;
        }

        public async Task<List<CcdBucket>> ListBucketsAsync(PageOptions pageOptions = default)
        {
            if (pageOptions == null)
            {
                pageOptions = new PageOptions();
            }
            var request = new ListBucketsByProjectRequest(CcdManagement.projectid, pageOptions.Page, pageOptions.PerPage);
            var response = await TryCatchRequest(BucketsApiClient.ListBucketsByProjectAsync, request);
            return response.Result;
        }

        public async Task<string> CreateContentAsync(Guid bucketId, Guid entryId)
        {
            var request = new CreateContentRequest(bucketId.ToString(), entryId.ToString(), CcdManagement.projectid);
            var response = await TryCatchRequest(InternalCreateContent, request);
            string location;
            response.Headers.TryGetValue("location", out location);
            return location;
        }

        private async Task<Response> InternalCreateContent(CreateContentRequest request, Configuration config)
        {
            var headers = request.ConstructHeaders(config);
            if (headers.ContainsKey(CONTENT_TYPE_HEADER))
            {
                headers[CONTENT_TYPE_HEADER] = " ";
            }
            else
            {
                headers.Add(CONTENT_TYPE_HEADER, " ");
            }

            var response = await _HttpClient.MakeRequestAsync(
                UnityWebRequest.kHttpVerbPOST,
                request.ConstructUrl(config.BasePath),
                null,
                headers,
                config.RequestTimeout.Value);

            if (response.IsHttpError || response.IsNetworkError)
            {
                throw new HttpException(response);
            }

            return new Response(response);
        }

        public async Task<Stream> GetContentAsync(EntryOptions entryOptions)
        {
            GetContentRequest request;
            request = new GetContentRequest(entryOptions.BucketId.ToString(), entryOptions.EntryId.ToString(), CcdManagement.projectid, entryOptions.VersionId != default ? entryOptions.VersionId.ToString() : null);
            var response = await TryCatchRequest(ContentApiClient.GetContentAsync, request);
            return response.Result;
        }

        public async Task<ContentStatus> GetContentStatusAsync(EntryOptions entryOptions)
        {
            GetContentStatusRequest request;
            request = new GetContentStatusRequest(entryOptions.BucketId.ToString(), entryOptions.EntryId.ToString(), CcdManagement.projectid, entryOptions.VersionId != default ? entryOptions.VersionId.ToString() : null);
            var response = await TryCatchRequest(ContentApiClient.GetContentStatusAsync, request);
            return GetContentStatusFromResponse(response);
        }

        public async Task<ContentStatus> GetContentStatusVersionAsync(EntryVersionsOptions entryVersionsOption)
        {
            var request = new GetContentStatusVersionRequest(
                entryVersionsOption.BucketId.ToString(),
                entryVersionsOption.EntryId.ToString(),
                entryVersionsOption.VersionId.ToString(),
                CcdManagement.projectid);
            var response = await TryCatchRequest(ContentApiClient.GetContentStatusVersionAsync, request);
            return GetContentStatusFromResponse(response);
        }

        public async Task<Stream> GetContentVersionAsync(EntryVersionsOptions entryVersionsOption)
        {
            var request = new GetContentVersionRequest(
                entryVersionsOption.BucketId.ToString(),
                entryVersionsOption.EntryId.ToString(),
                entryVersionsOption.VersionId.ToString(),
                CcdManagement.projectid);
            var response = await TryCatchRequest(ContentApiClient.GetContentVersionAsync, request);
            return response.Result;
        }

        public async Task UploadContentAsync(UploadContentOptions uploadContentOptions)
        {
            await TryCatchRequest(InternalUploadAsync, uploadContentOptions);
        }

        private async Task<Response> InternalUploadAsync(UploadContentOptions request, Configuration config)
        {
            var internalRequest = new InternalUploadContentRequest(request, config);
            string auth;
            config.Headers.TryGetValue(AUTH_HEADER, out auth);
            if (!_TusClient.AdditionalHeaders.ContainsKey(AUTH_HEADER))
            {
                _TusClient.AdditionalHeaders.Add(AUTH_HEADER, auth);
            }
            var uploadOp = _TusClient.UploadAsync(internalRequest.Url, internalRequest.File, internalRequest.ChunkSize);
            uploadOp.Progressed += internalRequest.OnProgressed;
            var uploadResponse = await uploadOp;
            var response = TusClient.MapTusHttpResponsesToHttpResponse(uploadResponse);
            if (response.IsHttpError || response.IsNetworkError)
            {
                //Custom call so we want to catch Authorization error for retry
                if (response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    throw new HttpException<AuthorizationError>(
                        response,
                        new AuthorizationError(
                            response.ErrorMessage,
                            (int)response.StatusCode,
                            Encoding.Default.GetString(response.Data)));
                }
                else
                {
                    throw new HttpException(response);
                }
            }
            return new Response(response);
        }

        public async Task<CcdEntry> CreateEntryAsync(Guid bucketId, EntryModelOptions entry)
        {
            var entryCreate = new CcdEntryCreate(entry.ContentHash, entry.ContentSize, entry.Path, entry.ContentType, entry.Labels, entry.Metadata);
            var request = new CreateEntryRequest(bucketId.ToString(), CcdManagement.projectid, entryCreate);
            var response = await TryCatchRequest(EntriesApiClient.CreateEntryAsync, request);
            return response.Result;
        }

        public async Task<CcdEntry> CreateOrUpdateEntryByPathAsync(EntryByPathOptions entryByPathOptions, EntryModelOptions entry)
        {
            var entryCreateOrUpdate = new CcdEntryCreateByPath(entry.ContentHash, entry.ContentSize, entry.ContentType, entry.Labels, entry.Metadata);
            var request = new CreateOrUpdateEntryByPathRequest(entryByPathOptions.BucketId.ToString(), entryByPathOptions.Path, CcdManagement.projectid, entryCreateOrUpdate, entry.UpdateIfExists);
            var response = await TryCatchRequest(EntriesApiClient.CreateOrUpdateEntryByPathAsync, request);
            return response.Result;
        }

        public async Task DeleteEntryAsync(Guid bucketId, Guid entryId)
        {
            var request = new DeleteEntryRequest(bucketId.ToString(), entryId.ToString(), CcdManagement.projectid);
            await TryCatchRequest(EntriesApiClient.DeleteEntryAsync, request);
        }

        public async Task<List<CcdEntry>> GetEntriesAsync(EntryOptions entryOptions, PageOptions pageOptions = default)
        {
            if (pageOptions == null)
            {
                pageOptions = new PageOptions();
            }
            var request = new GetEntriesRequest(entryOptions.BucketId.ToString(), CcdManagement.projectid, entryOptions.Path, entryOptions.Label, pageOptions.Page, pageOptions.PerPage);
            var response = await TryCatchRequest(EntriesApiClient.GetEntriesAsync, request);
            return response.Result;
        }

        public async Task<CcdEntry> GetEntryAsync(Guid bucketId, Guid entryId)
        {
            var request = new GetEntryRequest(bucketId.ToString(), entryId.ToString(), CcdManagement.projectid);
            var response = await TryCatchRequest(EntriesApiClient.GetEntryAsync, request);
            return response.Result;
        }

        public async Task<CcdEntry> GetEntryByPathAsync(EntryByPathOptions entryByPathOptions)
        {
            GetEntryByPathRequest request;
            request = new GetEntryByPathRequest(entryByPathOptions.BucketId.ToString(), entryByPathOptions.Path, CcdManagement.projectid, entryByPathOptions.VersionId != default ? entryByPathOptions.VersionId.ToString() : null);
            var response = await TryCatchRequest(EntriesApiClient.GetEntryByPathAsync, request);
            return response.Result;
        }

        public async Task<CcdEntry> GetEntryVersionAsync(EntryVersionsOptions entryVersionsOption)
        {
            var request = new GetEntryVersionRequest(entryVersionsOption.BucketId.ToString(), entryVersionsOption.EntryId.ToString(), entryVersionsOption.VersionId.ToString(), CcdManagement.projectid);
            var response = await TryCatchRequest(EntriesApiClient.GetEntryVersionAsync, request);
            return response.Result;
        }

        public async Task<List<CcdVersion>> GetEntryVersionsAsync(EntryOptions entryOptions, PageOptions pageOptions = default)
        {
            if (pageOptions == null)
            {
                pageOptions = new PageOptions();
            }
            var request = new GetEntryVersionsRequest(entryOptions.BucketId.ToString(), entryOptions.EntryId.ToString(), CcdManagement.projectid, entryOptions.Label, pageOptions.Page, pageOptions.PerPage);
            var response = await TryCatchRequest(EntriesApiClient.GetEntryVersionsAsync, request);
            return response.Result;
        }

        public async Task<CcdEntry> UpdateEntryAsync(EntryOptions entryOptions, EntryModelOptions entry)
        {
            var entryUpdate = new CcdEntryUpdate(entry.ContentHash, entry.ContentSize, entry.ContentType, entry.Labels, entry.Metadata);
            var request = new UpdateEntryRequest(entryOptions.BucketId.ToString(), entryOptions.EntryId.ToString(), CcdManagement.projectid, entryUpdate);
            var response = await TryCatchRequest(EntriesApiClient.UpdateEntryAsync, request);
            return response.Result;
        }

        public async Task<CcdEntry> UpdateEntryByPathAsync(EntryByPathOptions entryByPathOptions, EntryModelOptions entry)
        {
            var entryUpdateByPath = new CcdEntryUpdate(entry.ContentHash, entry.ContentSize, entry.ContentType, entry.Labels, entry.Metadata);
            var request = new UpdateEntryByPathRequest(entryByPathOptions.BucketId.ToString(), entryByPathOptions.Path, CcdManagement.projectid, entryUpdateByPath);
            var response = await TryCatchRequest(EntriesApiClient.UpdateEntryByPathAsync, request);
            return response.Result;
        }

        public async Task<CcdOrg> GetOrgAsync()
        {
            var proj = (await TryCatchRequest(InternalGetOrgData, CloudProjectSettings.projectId)).Result;
            var request = new GetOrgRequest(proj.organizationGenesisId);
            var response = await TryCatchRequest(OrgsApiClient.GetOrgAsync, request);
            return response.Result;
        }

        public async Task<CcdOrgUsage> GetOrgUsageAsync()
        {
            var proj = (await TryCatchRequest(InternalGetOrgData, CloudProjectSettings.projectId)).Result;
            var request = new GetOrgUsageRequest(proj.organizationGenesisId);
            var response = await TryCatchRequest(OrgsApiClient.GetOrgUsageAsync, request);
            return response.Result;
        }

        private async Task<Response<ProjectData>> InternalGetOrgData(string projectId, Configuration config)
        {
            ProjectData projectData;
            //HttpRequestMessage request = new HttpRequestMessage();
            var url = $"{config.BasePath}/api/unity/v1/projects/{projectId}";
            var headers = config.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var clientResponse = await _HttpClient.MakeRequestAsync(UnityWebRequest.kHttpVerbGET, url, null, headers, config.RequestTimeout.Value);
            if (clientResponse.IsHttpError || clientResponse.IsNetworkError)
            {
                //Custom call so we want to catch Authorization error for retry
                if (clientResponse.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    throw new HttpException<AuthorizationError>(
                        clientResponse,
                        new AuthorizationError(
                            clientResponse.ErrorMessage,
                            (int)clientResponse.StatusCode,
                            Encoding.Default.GetString(clientResponse.Data)));
                }
                else
                {
                    throw new HttpException(clientResponse);
                }
            }
            projectData = ProjectData.ParseProjectData(Encoding.Default.GetString(clientResponse.Data));
            return new Response<ProjectData>(clientResponse, projectData);
        }

        public async Task<CcdPermission> CreatePermissionAsync(CreatePermissionsOption permissionsOptions)
        {
            var request = new CreatePermissionByBucketRequest(permissionsOptions.BucketId.ToString(), CcdManagement.projectid,
                new CcdPermissionCreate(permissionsOptions.Action, permissionsOptions.Permission));
            var response = await TryCatchRequest(PermissionsApiClient.CreatePermissionByBucketAsync, request);
            return response.Result;
        }

        public async Task DeletePermissionAsync(UpdatePermissionsOption permissionsOptions)
        {
            string permission = permissionsOptions.Permission.ToString();
            var request = new DeletePermissionByBucketRequest(
                permissionsOptions.BucketId.ToString(), CcdManagement.projectid,
                permission: permissionsOptions.Permission.ToString().ToLower(),
                action: permissionsOptions.Action.ToString().ToLower());
            await TryCatchRequest(PermissionsApiClient.DeletePermissionByBucketAsync, request);
        }

        public async Task<List<CcdPermission>> GetPermissionsAsync(Guid bucketId)
        {
            var request = new GetAllByBucketRequest(bucketId.ToString(), CcdManagement.projectid);
            var response = await TryCatchRequest(PermissionsApiClient.GetAllByBucketAsync, request);
            return response.Result;
        }

        public async Task<CcdPermission> UpdatePermissionAsync(UpdatePermissionsOption permissionsOptions)
        {
            var request = new UpdatePermissionByBucketRequest(permissionsOptions.BucketId.ToString(), CcdManagement.projectid,
                new CcdPermissionUpdate(permissionsOptions.Action, permissionsOptions.Permission));
            var response = await TryCatchRequest(PermissionsApiClient.UpdatePermissionByBucketAsync, request);
            return response.Result;
        }

        public async Task<CcdRelease> CreateReleaseAsync(CreateReleaseOptions createReleaseOptions)
        {
            var releaseCreate = new CcdReleaseCreate(createReleaseOptions.Entries, createReleaseOptions.Metadata, createReleaseOptions.Notes, createReleaseOptions.Snapshot);
            var request = new CreateReleaseRequest(createReleaseOptions.BucketId.ToString(), CcdManagement.projectid, releaseCreate);
            var response = await TryCatchRequest(ReleasesApiClient.CreateReleaseAsync, request);
            return response.Result;
        }

        public async Task<CcdRelease> GetReleaseAsync(Guid bucketId, Guid releaseId)
        {
            var request = new GetReleaseRequest(bucketId.ToString(), releaseId.ToString(), CcdManagement.projectid);
            var response = await TryCatchRequest(ReleasesApiClient.GetReleaseAsync, request);
            return response.Result;
        }

        public async Task<CcdRelease> GetReleaseByBadgeAsync(Guid bucketId, string badgeName)
        {
            var request = new GetReleaseByBadgeRequest(bucketId.ToString(), badgeName, CcdManagement.projectid);
            var response = await TryCatchRequest(ReleasesApiClient.GetReleaseByBadgeAsync, request);
            return response.Result;
        }

        public async Task<CcdReleaseChangeVersion> GetReleaseDiffAsync(ReleaseDiffOptions releaseDiffOptions)
        {
            bool hasReleaseId = releaseDiffOptions.FromReleaseId != default && releaseDiffOptions.ToReleaseId != default;
            bool hasReleaseNum = releaseDiffOptions.FromReleaseNum.HasValue && releaseDiffOptions.ToReleaseNum.HasValue;
            GetReleaseDiffRequest request;
            if (hasReleaseId && !hasReleaseNum)
            {
                request = new GetReleaseDiffRequest(
                    releaseDiffOptions.BucketId.ToString(),
                    releaseDiffOptions.FromReleaseId.ToString(),
                    null,
                    CcdManagement.projectid,
                    releaseDiffOptions.ToReleaseId.ToString(),
                    null);
            }
            else if (hasReleaseNum && !hasReleaseId)
            {
                request = new GetReleaseDiffRequest(
                    releaseDiffOptions.BucketId.ToString(),
                    null,
                    releaseDiffOptions.FromReleaseNum.ToString(),
                    CcdManagement.projectid,
                    null,
                    releaseDiffOptions.ToReleaseNum.ToString());
            }
            else
            {
                throw new CcdManagementException(CcdManagementErrorCodes.InvalidArgument, "Cannot have both ReleaseId and ReleaseNum present.");
            }
            var response = await TryCatchRequest(ReleasesApiClient.GetReleaseDiffAsync, request);
            return response.Result;
        }

        public async Task<List<CcdReleaseEntry>> GetReleaseDiffEntriesAsync(ReleaseDiffOptions releaseDiffOptions, PageOptions pageOptions = null)
        {
            if (pageOptions == null)
            {
                pageOptions = new PageOptions();
            }
            bool hasReleaseId = releaseDiffOptions.FromReleaseId != default && releaseDiffOptions.ToReleaseId != default;
            bool hasReleaseNum = releaseDiffOptions.FromReleaseNum.HasValue && releaseDiffOptions.ToReleaseNum.HasValue;
            GetReleaseDiffEntriesRequest request;

            if (hasReleaseId && !hasReleaseNum)
            {
                request = new GetReleaseDiffEntriesRequest(
                    releaseDiffOptions.BucketId.ToString(),
                    releaseDiffOptions.FromReleaseId.ToString(),
                    null,
                    CcdManagement.projectid,
                    releaseDiffOptions.ToReleaseId.ToString(),
                    null,
                    pageOptions.Page,
                    pageOptions.PerPage,
                    releaseDiffOptions.Path,
                    releaseDiffOptions.Include_States);
            }
            else if (hasReleaseNum && !hasReleaseId)
            {
                request = new GetReleaseDiffEntriesRequest(
                    releaseDiffOptions.BucketId.ToString(),
                    null,
                    releaseDiffOptions.FromReleaseNum.ToString(),
                    CcdManagement.projectid,
                    null,
                    releaseDiffOptions.ToReleaseNum.ToString(),
                    pageOptions.Page,
                    pageOptions.PerPage,
                    releaseDiffOptions.Path,
                    releaseDiffOptions.Include_States);
            }
            else
            {
                throw new CcdManagementException(CcdManagementErrorCodes.InvalidArgument, "Cannot have both ReleaseId and ReleaseNum present.");
            }

            var response = await TryCatchRequest(ReleasesApiClient.GetReleaseDiffEntriesAsync, request);
            return response.Result;
        }

        public async Task<List<CcdReleaseEntry>> GetReleaseEntriesAsync(ReleaseEntryOptions releaseEntryOptions, PageOptions pageOptions = null)
        {
            if (pageOptions == null)
            {
                pageOptions = new PageOptions();
            }
            var request = new GetReleaseEntriesRequest(releaseEntryOptions.BucketId.ToString(), releaseEntryOptions.ReleaseId.ToString(),
                CcdManagement.projectid, releaseEntryOptions.Label, pageOptions.Page, pageOptions.PerPage);
            var response = await TryCatchRequest(ReleasesApiClient.GetReleaseEntriesAsync, request);
            return response.Result;
        }

        public async Task<List<CcdReleaseEntry>> GetReleaseEntriesByBadgeAsync(ReleaseByBadgeOptions releaseByBadgeOptions, PageOptions pageOptions = null)
        {
            if (pageOptions == null)
            {
                pageOptions = new PageOptions();
            }
            var request = new GetReleaseEntriesByBadgeRequest(
                releaseByBadgeOptions.BucketId.ToString(),
                releaseByBadgeOptions.BadgeName,
                CcdManagement.projectid,
                releaseByBadgeOptions.Label,
                pageOptions.Page,
                pageOptions.PerPage);
            var response = await TryCatchRequest(ReleasesApiClient.GetReleaseEntriesByBadgeAsync, request);
            return response.Result;
        }

        public async Task<List<CcdRelease>> GetReleasesAsync(Guid bucketId, PageOptions pageOptions = null)
        {
            if (pageOptions == null)
            {
                pageOptions = new PageOptions();
            }
            var request = new GetReleasesRequest(bucketId.ToString(), CcdManagement.projectid, pageOptions.Page, pageOptions.PerPage);
            var response = await TryCatchRequest(ReleasesApiClient.GetReleasesAsync, request);
            return response.Result;
        }

        public async Task<CcdMetricQuantity> GetStatsAsync(ReleaseStatsOptions releaseStatsOptions)
        {
            var request = new GetStatsRequest(
                releaseStatsOptions.BucketId.ToString(),
                releaseStatsOptions.ReleaseId.ToString(),
                releaseStatsOptions.Metric.ToString().ToLower(),
                releaseStatsOptions.Interval.ToString().ToLower(),
                CcdManagement.projectid);
            var response = await TryCatchRequest(ReleasesApiClient.GetStatsAsync, request);
            return response.Result;
        }

        public async Task<CcdRelease> UpdateReleaseAsync(Guid bucketId, Guid releaseId, string notes)
        {
            var request = new UpdateReleaseRequest(
                bucketId.ToString(), releaseId.ToString(), CcdManagement.projectid,
                new CcdReleaseUpdate(notes));
            var response = await TryCatchRequest(ReleasesApiClient.UpdateReleaseAsync, request);
            return response.Result;
        }

        public async Task<CcdUserAPIKey> GetUserApiKeyAsync()
        {
            var request = new GetUserApiKeyRequest(CloudProjectSettings.userId);
            var response = await TryCatchRequest(UsersApiClient.GetUserApiKeyAsync, request);
            return response.Result;
        }

        public async Task<CcdUser> GetUserInfoAsync()
        {
            var request = new GetUserInfoRequest(CloudProjectSettings.userId);
            var response = await TryCatchRequest(UsersApiClient.GetUserInfoAsync, request);
            return response.Result;
        }

        public async Task<CcdUserAPIKey> RegenerateUserApiKeyAsync()
        {
            var apiKeyResult = (await TryCatchRequest(UsersApiClient.GetUserApiKeyAsync, new GetUserApiKeyRequest(CloudProjectSettings.userId))).Result;
            var request = new RegenerateUserApiKeyRequest(CloudProjectSettings.userId, new CcdUserAPIKey(apiKeyResult.Apikey));
            var response = await TryCatchRequest(UsersApiClient.RegenerateUserApiKeyAsync, request);
            return response.Result;
        }

        /// <summary>
        /// Sets the base path of the config
        /// </summary>
        /// <param name="basePath">base path</param>
        public void SetBasePath(string basePath)
        {
            Configuration.BasePath = basePath;
        }

        // Helper function to reduce code duplication of try-catch
        private async Task<Response> TryCatchRequest<TRequest>(Func<TRequest, Configuration, Task<Response>> func, TRequest request)
        {
            if (string.IsNullOrEmpty(CcdManagement.projectid))
            {
                throw new CcdManagementException(CommonErrorCodes.InvalidRequest, SERVICES_ERROR_MSG);
            }
            Response response = null;
            try
            {
                if (!AuthHeaderConfigured(Configuration))
                {
                    await SetConfigurationAuthHeader(Configuration);
                }

                try
                {
                    response = await func(request, Configuration);
                }
                catch (HttpException<AuthorizationError>)
                {
                    ClearAuthHeader(Configuration);
                    await SetConfigurationAuthHeader(Configuration);
                    response = await func(request, Configuration);
                }
            }
            catch (HttpException he)
            {
                ResolveErrorWrapping((int)he.Response.StatusCode, he);
            }
            catch (Exception e)
            {
                //Pass error code that will throw default label, provide exception object for stack trace.
                ResolveErrorWrapping(CommonErrorCodes.Unknown, e);
            }
            return response;
        }

        // Helper function to reduce code duplication of try-catch (generic version)
        private async Task<Response<TReturn>> TryCatchRequest<TRequest, TReturn>(Func<TRequest, Configuration, Task<Response<TReturn>>> func, TRequest request)
        {
            if (string.IsNullOrEmpty(CcdManagement.projectid))
            {
                throw new CcdManagementException(CommonErrorCodes.InvalidRequest, SERVICES_ERROR_MSG);
            }

            Response<TReturn> response = null;
            try
            {
                if (!AuthHeaderConfigured(Configuration))
                {
                    await SetConfigurationAuthHeader(Configuration);
                }

                try
                {
                    response = await func(request, Configuration);
                }
                catch (HttpException<AuthorizationError>)
                {
                    ClearAuthHeader(Configuration);
                    await SetConfigurationAuthHeader(Configuration);
                    response = await func(request, Configuration);
                }
            }
            catch (HttpException he)
            {
                ResolveErrorWrapping((int)he.Response.StatusCode, he);
            }
            catch (Exception e)
            {
                //Pass error code that will throw default label, provide exception object for stack trace.
                ResolveErrorWrapping(CommonErrorCodes.Unknown, e);
            }
            return response;
        }

        // Helper function to resolve the new wrapped error/exception based on input parameter
        internal static void ResolveErrorWrapping(int reason, Exception exception = null)
        {
            int code = MapErrorCode(reason);
            //Check http exception types
            HttpException<AuthenticationError> authenticationException = exception as HttpException<AuthenticationError>;
            if (authenticationException != null)
            {
                throw new CcdManagementException(code, $"{authenticationException.ActualError.Title}. {authenticationException.ActualError.Detail}", exception);
            }
            HttpException<AuthorizationError> authorizationException = exception as HttpException<AuthorizationError>;
            if (authorizationException != null)
            {
                throw new CcdManagementException(code, $"{authorizationException.ActualError.Title}. {authorizationException.ActualError.Detail}", exception);
            }
            HttpException<ConflictError> conflictException = exception as HttpException<ConflictError>;
            if (conflictException != null)
            {
                throw new CcdManagementException(code, $"{conflictException.ActualError.Title}. {conflictException.ActualError.Detail}", exception);
            }
            HttpException<InternalServerError> internalServerException = exception as HttpException<InternalServerError>;
            if (internalServerException != null)
            {
                throw new CcdManagementException(code, internalServerException.ActualError.Title, exception);
            }
            HttpException<NotFoundError> notFoundException = exception as HttpException<NotFoundError>;
            if (notFoundException != null)
            {
                throw new CcdManagementException(code, $"{notFoundException.ActualError.Title}. {notFoundException.ActualError.Detail}", exception);
            }
            HttpException<ValidationError> validationException = exception as HttpException<ValidationError>;
            if (validationException != null)
            {
                throw new CcdManagementValidationException(code, $"{validationException.ActualError.Title}. {String.Join(", ", validationException.ActualError.Details)}", exception)
                {
                    Details = validationException.ActualError.Details
                };
            }
            HttpException<ServiceUnavailableError> serviceException = exception as HttpException<ServiceUnavailableError>;
            if (serviceException != null)
            {
                throw new CcdManagementException(code, "Service Unavailable", exception);
            }
            HttpException<TooManyRequestsError> tooManyRequestException = exception as HttpException<TooManyRequestsError>;
            if (tooManyRequestException != null)
            {
                throw new CcdManagementException(code, $"{tooManyRequestException.ActualError.Title}. {tooManyRequestException.ActualError.Detail}", exception);
            }
            //Other general exception message handling
            throw new CcdManagementException(code, exception.Message, exception);
        }

        /// <summary>
        /// Maps internal service error code or http error code to SDK error code
        /// </summary>
        /// <param name="reason">Error code to match</param>
        /// <returns></returns>
        internal static int MapErrorCode(int reason)
        {
            switch (reason)
            {
                case 001:
                    return CcdManagementErrorCodes.InvalidArgument;
                case 002:
                case 416:
                    return CcdManagementErrorCodes.OutOfRange;
                case 010:
                    return CcdManagementErrorCodes.InactiveOrganization;
                case 011:
                    return CcdManagementErrorCodes.InvalidHashMismatch;
                case 009:
                case 400:
                    return CommonErrorCodes.InvalidRequest;
                case 003:
                case 401:
                    return CcdManagementErrorCodes.Unauthorized;
                case 004:
                case 403:
                    return CommonErrorCodes.Forbidden;
                case 005:
                case 404:
                    return CommonErrorCodes.NotFound;
                case 408:
                    return CommonErrorCodes.Timeout;
                case 006:
                case 409:
                    return CcdManagementErrorCodes.AlreadyExists;
                case 012:
                case 429:
                    return CommonErrorCodes.TooManyRequests;
                case 008:
                case 500:
                    return CcdManagementErrorCodes.InternalError;
                case 503:
                    return CommonErrorCodes.ServiceUnavailable;
                case 007:
                default:
                    return CommonErrorCodes.Unknown;
            }
        }

        internal static bool AuthHeaderConfigured(Configuration config)
        {
            string auth;
            config.Headers.TryGetValue(AUTH_HEADER, out auth);
            if (string.IsNullOrEmpty(auth))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal static void ClearAuthHeader(Configuration config)
        {
            config.Headers.Remove(AUTH_HEADER);
        }

        private async Task SetConfigurationAuthHeader(Configuration config)
        {
            if (string.IsNullOrEmpty(CloudProjectSettings.accessToken))
            {
                throw new CcdManagementException(CommonErrorCodes.InvalidRequest, SERVICES_ERROR_MSG);
            }

            var jsonString = JsonConvert.SerializeObject(new Token() { TokenValue = CloudProjectSettings.accessToken });
            var url = $"{config.BasePath}/api/auth/v1/genesis-token-exchange/unity/";
            var headers = config.Headers.ToDictionary(kvp => kvp.Key, kvp => string.Join(", ", kvp.Value));
            if (headers.ContainsKey(CONTENT_TYPE_HEADER))
            {
                headers[CONTENT_TYPE_HEADER] = "application/json";
            }
            else
            {
                headers[CONTENT_TYPE_HEADER] = "application/json";
            }
            var clientResponse = await _HttpClient.MakeRequestAsync(UnityWebRequest.kHttpVerbPOST, url, Encoding.Default.GetBytes(jsonString), headers, config.RequestTimeout.Value);
            if (clientResponse.IsHttpError || clientResponse.IsNetworkError)
            {
                throw new HttpException(clientResponse);
            }
            var token = JsonConvert.DeserializeObject<Token>(Encoding.Default.GetString(clientResponse.Data)).TokenValue;
            var tokenValue = $"Bearer {token}";

            if (config.Headers.ContainsKey(AUTH_HEADER))
            {
                config.Headers[AUTH_HEADER] = tokenValue;
            }
            else
            {
                config.Headers.Add(AUTH_HEADER, tokenValue);
            }
        }

        internal static ContentStatus GetContentStatusFromResponse(Response response)
        {
            string uploadHash, uploadLengthString, uploadOffsetString;
            response.Headers.TryGetValue("upload-hash", out uploadHash);
            response.Headers.TryGetValue("upload-length", out uploadLengthString);
            response.Headers.TryGetValue("upload-offset", out uploadOffsetString);

            int uploadLength, uploadOffset;
            bool parsed = true;
            var parsedLength = int.TryParse(uploadLengthString, out uploadLength);
            var parsedOffset = int.TryParse(uploadOffsetString, out uploadOffset);
            parsed = parsedLength && parsedOffset && parsed;
            if (!parsed)
            {
                throw new Exception("Could not parse upload-length or upload-offset from request header.");
            }

            return new ContentStatus(uploadHash, uploadLength, uploadOffset);
        }

        private class Token
        {
            [JsonProperty("token")]
            public string TokenValue;
        }
    }
}
