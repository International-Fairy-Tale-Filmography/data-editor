using Octokit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataEditor.Core.Services
{
    //https://www.daveabrock.com/2021/03/14/upload-files-to-github-repository/
    public class GitService
    {
        private GitHubClient gitHubClient;
        public GitService() 
        {
            gitHubClient = new GitHubClient(new ProductHeaderValue("MyCoolApp"));
           
        }

        public async Task<string> GetUser()
        {
            var user = await gitHubClient.User.Get("thestamp");

            return user.Login;
        }
    }
}
