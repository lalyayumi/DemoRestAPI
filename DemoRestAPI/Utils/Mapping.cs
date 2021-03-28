using DemoRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoRestAPI.Utils
{
    public static class Mapping
    {
        public static GitHubStats MappToStats(GitHubProfile profile, IEnumerable<GitHubRepos> repos)
        {
            GitHubStats result = new GitHubStats();
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
            return result;
        }
    }
}
