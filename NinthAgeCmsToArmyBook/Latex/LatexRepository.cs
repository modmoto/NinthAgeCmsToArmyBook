using System.Diagnostics;

namespace NinthAgeCmsToArmyBook.Latex;

public class LatexRepository
{
    public void CreateLatex(string path, string texFilePath)
    {
        var process = Process.Start(
            new ProcessStartInfo
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                FileName = "/usr/bin/pdflatex",
                // FileName = "/Library/TeX/texbin/pdflatex",
                ArgumentList = { $"--output-directory={path}", texFilePath }
            });
        process.WaitForExit();
    }
}