using System.Diagnostics;

namespace NinthAgeCmsToArmyBook.Latex;

public class LatexRepository
{
    public void CreateLatex(string path, string texFileName, string pdfFileName)
    {
        var process = Process.Start(
            new ProcessStartInfo
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                FileName = "/usr/bin/pdflatex",
                ArgumentList = { $"--output-directory={path}", $"-jobname={pdfFileName}", $"{path}/{texFileName}" }
            });
        process.WaitForExit();
    }
}