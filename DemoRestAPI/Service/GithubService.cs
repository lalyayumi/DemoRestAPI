using DemoRestAPI.Models;
using DemoRestAPI.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DemoRestAPI.Service
{
    public interface IGithubService
    {
        Task<GitHubProfile> GetProfile();
        Task<IEnumerable<GitHubRepos>> GetRepos();
        Task<GitHubStats> GetStatsForReact();
    }
    public class GithubService : IGithubService
    {
        private HttpClient _httpClient;
        private IConfiguration _config;
        public GithubService(IConfiguration config, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<GitHubProfile> GetProfile()
        {
            GitHubProfile result = new GitHubProfile();
            var uri = _config["GitHub:GithubProfileUri"];

            try
            {
                var response = await _httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GitHubProfile>(data);
            }
            catch (Exception ex)
            {
                throw  new HttpStatusCodeException(HttpStatusCode.InternalServerError, ex.ToString()); 
            }
            return result;

        }

        public async Task<IEnumerable<GitHubRepos>> GetRepos()
        {
            IEnumerable<GitHubRepos> result = new List<GitHubRepos>();
            var uri = _config["GitHub:GithubReposUri"];

            try
            {
                var response = await _httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<IEnumerable<GitHubRepos>>(data);
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, ex.ToString());
            }
            return result;
        }

        public async Task<GitHubStats> GetStatsForReact()
        {
            GitHubStats result = new GitHubStats();
            var uri = _config["GitHub:GithubReposUri"];

            try
            {
                var profile = await GetProfile();
                var repos = await GetRepos();
                result = Mapping.MappToStats(profile, repos);
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, ex.ToString());
            }
            return result;
        }

        
    }
}
