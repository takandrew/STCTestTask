using System.Text.RegularExpressions;
using STCTestTask.Models;

namespace STCTestTask.Services
{
    public static class ParseMakeFileText
    {
        public static IEnumerable<Target> GetTargetList(string makeFileText)
        {
            var makeFileTextLineSplitted = makeFileText.Split('\n');

            var targetList = new List<Target>();

            for (int i = 0; i < makeFileTextLineSplitted.Length;)
            {
                if (!CheckForStartSpaces(makeFileTextLineSplitted[i]))
                {
                    var lineSplitted = makeFileTextLineSplitted[i].Split(':');
                    var targetName = lineSplitted[0].Trim();
                    var foundTarget = new Target(targetName);
                    if (lineSplitted.Length > 1)
                    {
                        var dependencies = lineSplitted[1].Split(" ");
                        foreach (var dependency in dependencies)
                        {
                            if (!string.IsNullOrWhiteSpace(dependency))
                                foundTarget.Dependencies.Add(dependency.Trim());
                        }
                    }
                    i++;

                    while (i < makeFileTextLineSplitted.Length && CheckForStartSpaces(makeFileTextLineSplitted[i]))
                    {
                        foundTarget.Actions.Add(makeFileTextLineSplitted[i].Trim());
                        i++;
                    }

                    targetList.Add(foundTarget);
                }
            }

            return targetList;
        }

        private static bool CheckForStartSpaces(string line)
        {
            return Regex.IsMatch(line, @"^ {1}.+");
        }
    }
}
