using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class InsertGitVersionEvent : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    private readonly string AssemblyInfoPath = "./Assets/AssemblyInfo.cs";
    private readonly string AssemblyInfoTemplatePath = "./Assets/AssemblyInfoTemplate.txt";

    private readonly string GitBinPath = @"C:\Program Files\Git\bin\";

    public void OnPreprocessBuild(BuildReport report)
    {
        string hash = GetCurrentCommitHash();
        int rev = GetTotalCommitCount();

        string assembly_text = File.ReadAllText(AssemblyInfoTemplatePath);
        assembly_text = assembly_text.Replace("%rev%", rev.ToString());
        assembly_text = assembly_text.Replace("%hash%", hash);

        File.WriteAllText(AssemblyInfoPath, assembly_text);
    }

    private string GetCurrentCommitHash()
    {
        Process process = CreateGitProcess();
        process.StartInfo.Arguments = @"rev-parse --short HEAD";

        process.Start();
        string result = process.StandardOutput.ReadLine();
        process.WaitForExit();
        process.Close();

        return result;
    }

    private int GetTotalCommitCount()
    {
        Process process = CreateGitProcess();
        process.StartInfo.Arguments = "log --oneline";

        process.Start();
        string commits = process.StandardOutput.ReadToEnd();
        int count = commits.Split('\n').Count() - 1;
        process.WaitForExit();
        process.Close();

        return count;
    }

    private Process CreateGitProcess()
    {
        Process process = new Process();
        process.StartInfo.FileName = GitBinPath + "git.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardInput = false;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.WorkingDirectory = @"C:/Users/yukki/Desktop/Projects/C#/Jams/TohoJam4";

        return process;
    }
}