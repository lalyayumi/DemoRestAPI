using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoRestAPI.Models
{
    public class GitHubStats
    {
        public string name { get; set; }
        public string url_git { get; set; }
        public string location { get; set; }
        public int number_repositories { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<GitHubRepoStats> repos { get; set; } = new List<GitHubRepoStats>();
    }
    public class GitHubRepoStats
    {
        public string name { get; set; }
        public string url_repository { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
