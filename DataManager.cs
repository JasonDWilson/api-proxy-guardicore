using Jwpro.Api.Proxy.Guardicore.Data.Configuration;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jwpro.Api.Proxy.Guardicore.Data
{
    public class DataManager
    {
        /// <summary>
        /// Stores the access token received after successful login
        /// </summary>
        private static string _accessToken;

        private CentraConfiguration _config;

        public DataManager(CentraConfiguration config)
        { _config = config ?? throw new ArgumentNullException(nameof(config)); }

        /// <summary>
        /// Method used to authenticate to Guardicore Centra API
        /// </summary>
        public void Authenticate()
        {
            AuthenticationResponse response = null;
            try
            {
                response = AuthenticationUrl.PostJsonToUrl(
                    new { username = _config.UserName, password = _config.Password })
                    .FromJson<AuthenticationResponse>();
            } catch(Exception ex)
            {
                throw new Exception("Error authenticating to Guardicore Centra API", ex);
            }
            _accessToken = response.access_token;
        }

        /// <summary>
        /// Method to retrieve agents from the Guardicore Centra API
        /// </summary>
        /// <param name="versions">agent versions to retrieve</param>
        /// <param name="kernelVersions">kernel versions to retrieve</param>
        /// <param name="osTypes">Operating Systems of agents to retrieve</param>
        /// <param name="labels">labels of agents to retrieve</param>
        /// <param name="statuses">statuses of agents to retrieve</param>
        /// <param name="moduleEnforcementStatuses">Module enforcement statuses of agents to retrieve</param>
        /// <param name="moduleDeceptionStatuses">Module deception statuses of agents to retrieve</param>
        /// <param name="moduleRevealStatuses">Module reveal statuses of agents to retrieve</param>
        /// <param name="activityIntervals">activity interval to retrieve agents</param>
        /// <param name="gcFilter">gc filter???</param>
        /// <returns></returns>
        public List<Agent> GetAgents(
            string[] versions = null,
            string[] kernelVersions = null,
            OperatingSystemType[] osTypes = null,
            Label[] labels = null,
            AgentStatus[] statuses = null,
            ModuleEnforcementStatus[] moduleEnforcementStatuses = null,
            ModuleDeceptionStatus[] moduleDeceptionStatuses = null,
            ModuleRevealStatus[] moduleRevealStatuses = null,
            ActivityInterval[] activityIntervals = null,
            string gcFilter = null)
        {
            StringBuilder query = new StringBuilder($"{AgentsUrl}&limit=1000");
            int count = 0;
            if(versions != null && versions.Count() > 0)
            {
                count = 0;
                query.Append("&version=");
                foreach(var version in versions)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(Uri.EscapeDataString(version));
                }
            }
            if(kernelVersions != null && kernelVersions.Count() > 0)
            {
                count = 0;
                query.Append("&kernel=");
                foreach(var kernel in kernelVersions)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(Uri.EscapeDataString(kernel));
                }
            }
            if(osTypes != null && osTypes.Count() > 0)
            {
                count = 0;
                query.Append("&os=");
                foreach(var os in osTypes)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(os.ToString());
                }
            }
            if(labels != null && labels.Count() > 0)
            {
                count = 0;
                query.Append("&labels=");
                foreach(var label in labels)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(label.id);
                }
            }
            if(statuses != null && statuses.Count() > 0)
            {
                count = 0;
                query.Append("&display_status=");
                foreach(var status in statuses)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(status.ToString());
                }
            }
            if(moduleEnforcementStatuses != null && moduleEnforcementStatuses.Count() > 0)
            {
                count = 0;
                query.Append("&module_status_enforcement=");
                foreach(var status in statuses)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(status.ToString());
                }
            }
            if(moduleDeceptionStatuses != null && moduleDeceptionStatuses.Count() > 0)
            {
                count = 0;
                query.Append("&module_status_deception=");
                foreach(var status in statuses)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(status.ToString());
                }
            }
            if(moduleRevealStatuses != null && moduleRevealStatuses.Count() > 0)
            {
                count = 0;
                query.Append("&module_status_reveal=");
                foreach(var status in statuses)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(status.ToString());
                }
            }
            if(activityIntervals != null && activityIntervals.Count() > 0)
            {
                count = 0;
                query.Append("&activity=");
                foreach(var activity in activityIntervals)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(activity.ToString());
                }
            }
            if(!string.IsNullOrEmpty(gcFilter))
                query.Append($"&gc_filter={gcFilter}");

            List<Agent> agents;
            try
            {
                string url = query.ToString();
                var response = url.GetJsonFromUrl().FromJson<PaginatedResponse<Agent>>();
                agents = response.objects;

                // check for other pages
                if(response.total_count > response.results_in_page)
                {
                    // other pages exists
                    int totalPages = (int)Math.Floor((double)(response.total_count / response.results_in_page)) + 1;
                    int currentPage = response.current_page;
                    do
                    {
                        int offset = (currentPage * response.results_in_page) + 1;
                        string newUrl = $"{url}&offset={offset}";
                        var newResponse = newUrl.GetJsonFromUrl().FromJson<PaginatedResponse<Agent>>();
                        foreach(var agent in newResponse.objects)
                            agents.Add(agent);
                        currentPage++;
                    } while (currentPage < totalPages);
                }
            } catch(Exception ex)
            {
                throw new Exception($"Error getting agents using query: {query}", ex);
            }
            return agents;
        }

        /// <summary>
        /// Method to retrieve assets from the Guardicore Centra API
        /// </summary>
        /// <param name="assetIDs">IDs of assets to retrieve</param>
        /// <param name="riskLevels">Risk level of assets to retrieve</param>
        /// <param name="statuses">Statuses of assets to retrieve</param>
        /// <returns></returns>
        public List<Asset> GetAssets(
            AssetID[] assetIDs = null,
            RiskLevel[] riskLevels = null,
            AssetStatus[] statuses = null)
        {
            StringBuilder query = new StringBuilder($"{AssetsUrl}&limit=1000");
            int count = 0;
            if(assetIDs != null && assetIDs.Count() > 0)
            {
                count = 0;
                query.Append("&asset=");
                foreach(var asset in assetIDs)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(Uri.EscapeDataString(asset.ToString()));
                }
            }
            if(riskLevels != null && riskLevels.Count() > 0)
            {
                count = 0;
                query.Append("&risk_level=");
                foreach(var level in riskLevels)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append((int)level);
                }
            }
            List<Asset> assets;
            try
            {
                string url = query.ToString();
                var response = url.GetJsonFromUrl().FromJson<PaginatedResponse<Asset>>();
                assets = response.objects;

                // check for other pages
                if(response.total_count > response.results_in_page)
                {
                    // other pages exists
                    int totalPages = (int)Math.Floor((double)(response.total_count / response.results_in_page)) + 1;
                    int currentPage = response.current_page;
                    do
                    {
                        int offset = (currentPage * response.results_in_page) + 1;
                        string newUrl = $"{url}&offset={offset}";
                        var newResponse = newUrl.GetJsonFromUrl().FromJson<PaginatedResponse<Asset>>();
                        foreach(var asset in newResponse.objects)
                            assets.Add(asset);
                        currentPage++;
                    } while (currentPage < totalPages);
                }
            } catch(Exception ex)
            {
                throw new Exception($"Error getting assets using query: {query}", ex);
            }
            return assets;
        }

        /// <summary>
        /// Method to retrieve Incidents from the Guardicore Centra API
        /// </summary>
        /// <param name="begin">the earliest date to retrieve incidents</param>
        /// <param name="end">the latest date to retrieve incidents</param>
        /// <param name="assets">IDs?? of assets involved in incidents to retrieve</param>
        /// <param name="destinationAssets">IDs? of destination assets involved in incidents to retrieve</param>
        /// <param name="excludedTags">Tags of assets to exclude from retrieval</param>
        /// <param name="groups">incident group of incidents to retrieve</param>
        /// <param name="ids">IDs of incidents to retrieve</param>
        /// <param name="severities">Severity of incidents to retrieve</param>
        /// <param name="sourceAssets">IDs? of source assets involved in incidents to retrieve</param>
        /// <param name="tags">tags of incidents to retrieve</param>
        /// <param name="types">types of incidents to retrieve</param>
        /// <returns></returns>
        public List<Incident> GetIncidents(
            DateTime begin,
            DateTime end,
            string[] assets = null,
            string[] destinationAssets = null,
            string[] excludedTags = null,
            string[] groups = null,
            string[] ids = null,
            IncidentSeverity[] severities = null,
            string[] sourceAssets = null,
            string[] tags = null,
            IncidentTypes[] types = null)
        {
            if(begin == default(DateTime))
                begin = DateTime.Now.AddDays(-1);
            if(end == default(DateTime))
                end = DateTime.Now;
            string beginTimestamp = $"{(long)(begin.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds}";
            string endTimestamp = $"{(long)(end.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds}";


            StringBuilder query = new StringBuilder(
                $"{IncidentsUrl}&limit=1000&from_time={beginTimestamp}&to_time={endTimestamp}");
            int count;
            if(assets != null && assets.Count() > 0)
            {
                count = 0;
                query.Append("&assets=");
                foreach(var asset in assets)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(Uri.EscapeDataString(asset));
                }
            }
            if(destinationAssets != null && destinationAssets.Count() > 0)
            {
                count = 0;
                query.Append("&destinationAssets=");
                foreach(var destinationAsset in destinationAssets)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(Uri.EscapeDataString(destinationAsset));
                }
            }
            if(excludedTags != null && excludedTags.Count() > 0)
            {
                count = 0;
                query.Append("&tags__not=");
                foreach(var excludedTag in excludedTags)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(Uri.EscapeDataString(excludedTag));
                }
            }
            if(groups != null && groups.Count() > 0)
            {
                count = 0;
                query.Append("&incident_group=");
                foreach(var group in groups)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(Uri.EscapeDataString(group));
                }
            }
            if(ids != null && ids.Count() > 0)
            {
                count = 0;
                query.Append("&id=");
                foreach(var id in ids)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(Uri.EscapeDataString(id));
                }
            }
            if(severities != null && severities.Count() > 0)
            {
                count = 0;
                query.Append("&severity=");
                foreach(var severity in severities)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(severity.ToString());
                }
            }
            if(sourceAssets != null && sourceAssets.Count() > 0)
            {
                count = 0;
                query.Append("&source=");
                foreach(var sourceAsset in sourceAssets)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(Uri.EscapeDataString(sourceAsset));
                }
            }
            if(types != null && types.Count() > 0)
            {
                count = 0;
                query.Append("&incident_type=");
                foreach(var type in types)
                {
                    count++;
                    if(count > 1)
                        query.Append(",");
                    query.Append(type.ToString().Replace("_", " "));
                }
            }

            List<Incident> incidents = null;
            string url = query.ToString();
            if(tags.Count() == 0)
                try
                {
                    var response = url.GetJsonFromUrl().FromJson<PaginatedResponse<Incident>>();
                    incidents = response.objects;

                    // check for other pages
                    if(response.total_count > response.results_in_page)
                    {
                        // other pages exists
                        int totalPages = (int)Math.Floor((double)(response.total_count / response.results_in_page)) + 1;
                        int currentPage = response.current_page;
                        do
                        {
                            int offset = (currentPage * response.results_in_page) + 1;
                            string newUrl = $"{url}&offset={offset}";
                            var newResponse = newUrl.GetJsonFromUrl().FromJson<PaginatedResponse<Incident>>();
                            foreach(var incident in newResponse.objects)
                                incidents.Add(incident);
                            currentPage++;
                        } while (currentPage < totalPages);
                    }
                } catch(Exception ex)
                {
                    throw new Exception($"Error getting incidents using query: {query}", ex);
                }
            else
                foreach(string tag in tags)
                {
                    string taggedUrl = $"{url}&tag={tag}";
                    try
                    {
                        var response = taggedUrl.GetJsonFromUrl().FromJson<PaginatedResponse<Incident>>();
                        if(incidents == null)
                            incidents = response.objects;
                        else
                            incidents.AddRange(response.objects);

                        // check for other pages
                        if(response.total_count > response.results_in_page)
                        {
                            // other pages exists
                            int totalPages = (int)Math.Floor((double)(response.total_count / response.results_in_page)) +
                                1;
                            int currentPage = response.current_page;
                            do
                            {
                                int offset = (currentPage * response.results_in_page) + 1;
                                string newUrl = $"{taggedUrl}&offset={offset}";
                                var newResponse = newUrl.GetJsonFromUrl().FromJson<PaginatedResponse<Incident>>();
                                foreach(var incident in newResponse.objects)
                                    incidents.Add(incident);
                                currentPage++;
                            } while (currentPage < totalPages);
                        }
                    } catch(Exception ex)
                    {
                        throw new Exception($"Error getting incidents using query: {query}", ex);
                    }
                }
            return incidents;
        }

        /// <summary>
        /// Method to retrieve Labels from the Guardicore Centra API
        /// </summary>
        /// <param name="findMatches">boolean indicating whether related assets should be returned with labels</param>
        /// <param name="key">search labels by key</param>
        /// <param name="value">search labels by value</param>
        /// <returns></returns>
        public List<Label> GetLabels(bool? findMatches = null, string key = null, string value = null)
        {
            StringBuilder query = new StringBuilder($"{LabelsUrl}&limit=1000");
            if(findMatches != null)
                query.Append($"&find_matches={findMatches}");
            if(!string.IsNullOrWhiteSpace(key))
                query.Append($"&key={key}");
            if(!string.IsNullOrWhiteSpace(value))
                query.Append($"&value={value}");
            List<Label> labels;
            try
            {
                string url = query.ToString();
                var response = url.GetJsonFromUrl().FromJson<PaginatedResponse<Label>>();
                labels = response.objects;

                // check for other pages
                if(response.total_count > response.results_in_page)
                {
                    // other pages exists
                    int totalPages = (int)Math.Floor((double)(response.total_count / response.results_in_page)) + 1;
                    int currentPage = response.current_page;
                    do
                    {
                        int offset = (currentPage * response.results_in_page) + 1;
                        string newUrl = $"{url}&offset={offset}";
                        var newResponse = newUrl.GetJsonFromUrl().FromJson<PaginatedResponse<Label>>();
                        foreach(var label in newResponse.objects)
                            labels.Add(label);
                        currentPage++;
                    } while (currentPage < totalPages);
                }
            } catch(Exception ex)
            {
                throw new Exception($"Error getting labels using query: {query}", ex);
            }
            return labels;
        }

        /// <summary>
        /// Returns the access token received after successful login
        /// </summary>
        public string AccessToken
        {
            get
            {
                if(_accessToken == null)
                {
                    Authenticate();
                }
                return _accessToken;
            }
        }

        /// <summary>
        /// Holds the url path to agents as defined in the config file
        /// </summary>
        public string AgentsUrl
        {
            get { return $"https://{_config.CentralUrl}/api/v{_config.Version}/agents?token={AccessToken}"; }
        }

        /// <summary>
        /// Holds the url path to the assets as defined in the config file
        /// </summary>
        public string AssetsUrl
        {
            get { return $"https://{_config.CentralUrl}/api/v{_config.Version}/assets?token={AccessToken}"; }
        }

        /// <summary>
        /// holds the authentication url as defined in the config file
        /// </summary>
        public string AuthenticationUrl
        {
            get { return $"https://{_config.CentralUrl}/api/v{_config.Version}/authenticate"; }
        }

        /// <summary>
        /// Holds the url path to the incidents as defined in the config file
        /// </summary>
        public string IncidentsUrl
        {
            get { return $"https://{_config.CentralUrl}/api/v{_config.Version}/incidents?token={AccessToken}"; }
        }

        /// <summary>
        /// Holds the url path to labels as defined in the config file
        /// </summary>
        public string LabelsUrl
        {
            get { return $"https://{_config.CentralUrl}/api/v{_config.Version}/labels?token={AccessToken}"; }
        }
    }
}
