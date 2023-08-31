//using System;
//using System.Threading.Tasks;

//namespace AdapterDesignPattern
//{
//    public class Program
//    {
//        public static async Task Main(string[] args)
//        {
//            // Replace with your GitHub details:
//            string accessToken = "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"; // Replace with your GitHub access token.
//            string repositoryOwner = "apekshaalaad"; // Replace with your GitHub username or organization name.
//            string repositoryName = "testdemo"; // Replace with the name of your GitHub repository.
//            string commitSha = "a7d44a9af183a097487873ac5343c4f2bccc6359"; // Replace with the specific commit SHA you want to retrieve code from.

//            var githubAdapter = new GitHubAdapter(accessToken, repositoryOwner, repositoryName);

//            string code = await githubAdapter.GetCodeFromCommitAsync(commitSha);
//            Console.WriteLine("Retrieved Code");
//            // Do something with the retrieved code.
//            Console.WriteLine(code);
//            Console.WriteLine("Press Enter to exit...");
//            Console.ReadLine();
//        }
//    }


//}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Replace these with your GitHub details and the desired commit SHA.
        string accessToken = "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"; // Replace with your GitHub access token.
        string repositoryOwner = "apekshaalaad"; // Replace with your GitHub username or organization name.
        string repositoryName = "testdemo"; // Replace with the name of your GitHub repository.
        string commitSha = "a7d44a9af183a097487873ac5343c4f2bccc6359"; // Replace with the specific commit SHA you want to retrieve code from.


        var githubAdapter = new GitHubAdapter(accessToken, repositoryOwner, repositoryName);

        try
        {
            // Fetch code from the specified commit.
            List<string> codeFiles = await githubAdapter.GetCodeFromCommitAsync(commitSha);


            // Display the code.
            foreach (var codeFile in codeFiles)
            {
                Console.WriteLine(codeFile);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
}


