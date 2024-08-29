using DataEditor.Core.Configuration;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEditor.Core.Services
{
    //https://www.daveabrock.com/2021/03/14/upload-files-to-github-repository/
    public class GitService
    {
        private readonly GitHubClient _gitHubClient;
        public CoreSettingsModel _coreSettings;
        public GitService(CoreSettingsModel settings)
        {
            _coreSettings = settings;
            _gitHubClient = new GitHubClient(new ProductHeaderValue("MyCoolApp"));
        }

        

        public void ApplyConfiguration()
        {
            _gitHubClient.Credentials = new Credentials(_coreSettings.AccessToken);
        }

        public async Task<string> GetUser()
        {
            var user = await _gitHubClient.User.Get("thestamp");

            return user.Login;
        }

        public async Task<RepositoryContent> GetFile(string filename)
        {
            var filePath = Path.Combine(_coreSettings.Folder, filename);
            var fileDetails = await _gitHubClient.Repository.Content.GetAllContentsByRef(
                _coreSettings.Owner, 
                _coreSettings.RepoName,
                filePath, _coreSettings.Branch);

            return fileDetails.First();
        }

        public async Task<RepositoryContentChangeSet> UpdateFile(string filename, RepositoryContent lastCommit, string newContent, string summary)
        {

            var updateResult = await _gitHubClient.Repository.Content.UpdateFile(
                _coreSettings.Owner,
                _coreSettings.RepoName, 
                Path.Combine(_coreSettings.Folder, filename),
                new UpdateFileRequest(summary, newContent, lastCommit.Sha, _coreSettings.Branch));

            return updateResult;
        }


    }
}
