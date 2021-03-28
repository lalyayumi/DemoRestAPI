using DemoRestAPI.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
                return new GitHubProfile();
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
                return new List<GitHubRepos>();
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
                if(profile!=null)
                {
                    result.name = profile.login;
                    result.url_git = profile.url;
                    result.number_repositories = profile.public_repos;
                    result.location = profile.location;
                    result.created_at = profile.created_at;
                    result.updated_at = profile.updated_at;
                }
                if (repos.Count()>0)
                {

                    foreach(var repo in repos)
                    {
                        GitHubRepoStats repoStat = new GitHubRepoStats();
                        repoStat.name = repo.name;
                        repoStat.url_repository = repo.html_url;
                        repoStat.created_at = repo.created_at;
                        repoStat.updated_at = repo.updated_at;
                        result.repos.Add(repoStat);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
