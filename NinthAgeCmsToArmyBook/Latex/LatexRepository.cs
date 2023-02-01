using System.Diagnostics;

namespace NinthAgeCmsToArmyBook.Latex;

public class LatexRepository
{
    public void CreateLatex(string path, string latexFileName, string targetFileName)
    {
        var process1 = new Process();
        var startInfo = new ProcessStartInfo();
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = $"C/ pdflatex -job-name=${targetFileName} {path}\\{latexFileName}";
        process1.StartInfo = startInfo;
        process1.Start();
        // process1.WaitForExit();
        
        var process = new Process
        {
            StartInfo =
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                FileName = "CMD.exe",
                WorkingDirectory = path,
                Arguments = $"dir"
                // Arguments = $"pdflatex -job-name=${targetFileName} {latexFileName}"
            }
        };
        process.Start();
        process.WaitForExit();
        
        Console.WriteLine(process.ExitCode);
    }
}