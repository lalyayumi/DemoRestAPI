using DemoRestAPI.Models;
using DemoRestAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DemoRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubController : ControllerBase
    {

        private IGithubService _gitHubService;

        private readonly ILogger<GitHubController> _logger;

        public GitHubController(ILogger<GitHubController> logger, IGithubService gitHubService)
        {
            _logger = logger;
            _gitHubService = gitHubService;
        }

        [HttpGet]        
        [Route("profile")]
        public async Task<IActionResult> GetProfile()
        {
            GitHubProfile result = new GitHubProfile();
            try
            {
                result= await _gitHubService.GetProfile();
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw new Utils.HttpStatusCodeException(HttpStatusCode.InternalServerError, ex.ToString());
            }
                
           
        }
        [HttpGet]
        [Route("repos")]
        public async Task<IActionResult> GetRepos()
        {
            IEnumerable<GitHubRepos> result = new List<GitHubRepos>();
            try
            {
                result = await _gitHubService.GetRepos();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Utils.HttpStatusCodeException(HttpStatusCode.InternalServerError, ex.ToString());
            }


        }
    }
}
