using System.Diagnostics;

namespace NinthAgeCmsToArmyBook.Latex;

public class LatexRepository
{
    public void CreateLatex(string path, string latexFileName, string targetFileName)
    {
        var process = Process.Start(
            new ProcessStartInfo
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                FileName = "/usr/bin/pdflatex",
                ArgumentList = { $"--output-directory={path}", $"-jobname={targetFileName}", $"{path}/{latexFileName}" }
            });
        process.WaitForExit();
    }
}