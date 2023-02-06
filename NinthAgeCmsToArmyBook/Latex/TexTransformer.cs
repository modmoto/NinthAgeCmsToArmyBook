using NinthAgeCmsToArmyBook.ArmyBooks;

namespace NinthAgeCmsToArmyBook.Latex;

public class TexTransformer
{
    public async Task CreateTexFile(string armyName, ArmyBook selectedBook, string targetPath)
    {
        var texContent = $@"\documentclass[12pt]{{article}}
\begin{{document}}
\title{{{armyName}}}
\author{{Army Book (Core Rules)}}
\date{{2\textsuperscript{{nd}} Edition, version 2022 {selectedBook.Version} - \today}}

\maketitle
\section*{{Special Rules}}

\section*{{Units}}
";
        foreach (var selectedBookUnit in selectedBook.Units)
        {
            var unitContent = $@"\subsection*{{{selectedBookUnit.Name}}}
\textbf{{{selectedBookUnit.BaseCost}}} pts + \textbf{{{selectedBookUnit.AdditionalCost}}} pts/extra model,  \textbf{{{selectedBookUnit.UnitSize.Min}-{selectedBookUnit.UnitSize.Max}}} models, {selectedBookUnit.ModelsPerArmy.Min}-{selectedBookUnit.ModelsPerArmy.Min} models/army
\begin{{center}}
\begin{{tabular}}{{p{{0.17\textwidth}}p{{0.17\textwidth}}p{{0.17\textwidth}}p{{0.17\textwidth}}p{{0.17\textwidth}}}}
 Adv & Mar & Dis & & \\ 
 {selectedBookUnit.GlobalProfile.First().Advance}"" & {selectedBookUnit.GlobalProfile.First().March}"" & {selectedBookUnit.GlobalProfile.First().Discipline} & & \\ 
 \hline
 HP & Def & Res & Arm & \\ 
 {selectedBookUnit.DefensiveProfile.First().HealthPoints} & {selectedBookUnit.DefensiveProfile.First().Defense} & {selectedBookUnit.DefensiveProfile.First().Resilience} & {selectedBookUnit.DefensiveProfile.First().Armor} & \\ 
 \hline
 Att & Of & Str & AP & Agi \\ 
 {selectedBookUnit.OffensiveProfile.First().Attacks} & {selectedBookUnit.OffensiveProfile.First().Offense} & {selectedBookUnit.OffensiveProfile.First().Strength} & {selectedBookUnit.OffensiveProfile.First().ArmorPiercing} & {selectedBookUnit.OffensiveProfile.First().Agility} \\ 
\end{{tabular}}
\end{{center}}";
            texContent += unitContent;
        }
        texContent += "\\end{document}";

        if (!File.Exists(targetPath))
        {
            var handle = File.Create(targetPath);
            await handle.FlushAsync();
            await handle.DisposeAsync();
        }
        await File.WriteAllTextAsync(targetPath, texContent);
    }
}