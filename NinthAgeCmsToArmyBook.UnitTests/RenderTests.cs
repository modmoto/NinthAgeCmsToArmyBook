using System.Diagnostics;
using System.Reflection;
using NinthAgeCmsToArmyBook.Model;
using NUnit.Framework;

namespace NinthAgeCmsToArmyBook.UnitTests;

public class RenderTests
{
    [Test]
    public void BasicRenderingWorks()
    {
        var globalProfiles = new List<GlobalProfile>
        {
            new(5, 10, 9)
        };
        var defensiveProfiles = new List<DefensiveProfile>
        {
            new(1, 4, 3, 3)
        };
        var offensiveProfiles = new List<OffensiveProfile>
        {
            new(1, 5, 4, 1, 6)
        };
        var unit = new Unit("OBsGuard", globalProfiles, defensiveProfiles, offensiveProfiles, false);

        var latexRepository = new LatexRepository();
    }

    [Test]
    public void CreatePdfWorks()
    {
        var latexRepository = new LatexRepository();
        var directory = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\TexFiles";
        latexRepository.CreateLatex(directory, "hello-world.tex", "output_Vermin_Swarms.pdf");
    }
}

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