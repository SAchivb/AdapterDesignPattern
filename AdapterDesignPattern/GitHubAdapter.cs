//using Octokit;
//using System;
//using System.Net.Http.Headers;
//using System.Net;
//using System.Threading.Tasks;
//using ProductHeaderValue = Octokit.ProductHeaderValue;

//public class GitHubAdapter
//{
//    private GitHubClient _githubClient;
//    private string _repositoryOwner;
//    private string _repositoryName;

//    public GitHubAdapter(string accessToken, string repositoryOwner, string repositoryName)
//    {
//        _githubClient = new GitHubClient(new ProductHeaderValue("FetchingCode"));
//        _githubClient.Credentials = new Credentials(accessToken);
//        _repositoryOwner = repositoryOwner;
//        _repositoryName = repositoryName;
//    }

//    public async Task<string> GetCodeFromCommitAsync(string commitSha)
//    {
//        try
//        {
//            var commit = await _githubClient.Repository.Commit.Get(_repositoryOwner, _repositoryName, commitSha);
//            //var tree = await _githubClient.Git.Tree.Get(_repositoryOwner, _repositoryName, commit.Tree.Sha);
//            // Now, you can fetch the code from the tree or perform other operations.
//            // The code here depends on your specific use case.
//            // You might want to fetch individual files from the tree.

//            return "Code from the commit here...";
//        }
//        catch (Exception ex)
//        {
//            // Handle exceptions appropriately.
//            return $"Error: {ex.Message}";
//        }
//    }
//}
using Octokit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GitHubAdapter
{
    private GitHubClient _githubClient;
    private string _repositoryOwner;
    private string _repositoryName;

    public GitHubAdapter(string accessToken, string repositoryOwner, string repositoryName)
    {
        _githubClient = new GitHubClient(new ProductHeaderValue("YourAppName"));
        _githubClient.Credentials = new Credentials(accessToken);
        _repositoryOwner = repositoryOwner;
        _repositoryName = repositoryName;
    }

    public async Task<List<string>> GetCodeFromCommitAsync(string commitSha)
    {
        List<string> codeFiles = new List<string>(); // Initialize the list.

        try
        {
            // Fetch the commit details.
            var commit = await _githubClient.Repository.Commit.Get(_repositoryOwner, _repositoryName, commitSha);

            // Fetch the tree associated with the commit.
            var tree = await _githubClient.Git.Tree.Get(_repositoryOwner, _repositoryName, commit.Commit.Tree.Sha);

            // Fetch the content of individual files in the tree.
            codeFiles.AddRange(await FetchFiles(tree));
        }
        catch (ApiException ex)
        {
            // Handle GitHub API errors.
            codeFiles.Add($"GitHub API Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle other exceptions.
            codeFiles.Add($"Error: {ex.Message}");
        }

        return codeFiles;
    }


    private async Task<IEnumerable<string>> FetchFiles(TreeResponse tree)
    {
        var codeFiles = new List<string>();

        foreach (var treeItem in tree.Tree)
        {
            if (treeItem.Type == TreeType.Blob)
            {
                // Fetch the content of the file using the blob SHA.
                var fileContent = await _githubClient.Git.Blob.Get(_repositoryOwner, _repositoryName, treeItem.Sha);
                codeFiles.Add(fileContent.Content);
            }
        }

        return codeFiles;
    }
}



