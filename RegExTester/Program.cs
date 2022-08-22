using System;
using System.Text.RegularExpressions;

namespace RegExTester
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex?view=net-5.0#code-try-3
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //RegexClass();
            RegexReplace();
            //RegexMatch();
        }

        /// <summary>
        /// Tests the character matching of the Regex Class. See RegexMatch for more current implementation.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex?view=net-6.0
        /// </summary>
        static void RegexClass()
        {
            // Define a regular expression for repeated words.
            Regex rx = new Regex(@"^\d");

            // Define a test string.
            string text = "VADS12534";

            // Find matches.
            MatchCollection matches = rx.Matches(text);
            
            Console.WriteLine("*******************************************");

            // Report the number of matches found.
            Console.WriteLine("Regex matches: " + rx.IsMatch(text));
            Console.WriteLine("{0} matches found in:\n   {1}",
                              matches.Count,
                              text);

            // Report on each match.
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                Console.WriteLine("'{0}' repeated at positions {1} and {2}",
                                  groups["word"].Value,
                                  groups[0].Index,
                                  groups[1].Index);
            }

            if (matches.Count == 0) Console.WriteLine("No matches found.");

            Console.WriteLine("*******************************************");
        }

        /// <summary>
        /// Tests the replacing of characters in a string.
        /// </summary>
        static void RegexReplace()
        {
            // Define a test string.
            string text = "\\VA/DS12\\534/";

            // REGEX: @"[A-Z]" Matches alphabetical characters
            // Test replacement.
            Console.WriteLine("*******************************************");
            Console.WriteLine("");
            Console.WriteLine(Regex.Replace(text, @"[\/\\]", ""));
            Console.WriteLine("");
            Console.WriteLine("*******************************************");
        }

        /// <summary>
        /// Because the method can be called multiple times from user code, it uses the static 
        /// Regex.Match(String, String, RegexOptions) method. This enables the regular expression 
        /// engine to cache the regular expression and avoids the overhead of instantiating a 
        /// new Regex object each time the method is called. A Match object is then used to 
        /// iterate through all matches in the string.
        /// https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-example-scanning-for-hrefs
        /// </summary>
        static void RegexMatch()
        {
            try
            {
                // Define a regular expression.
                string pattern = @"Photos/\s*=\s*(?:[""'](?<1>[^""']*)[""']|(?<1>[^>\s]+))";

                // Define a test string.
                string text = "<img src=\"https://hpi359.sharepoint.com/sites/UAT_projectfiles/Documents/FilesVA/182273/Photos/000180814-eval-01.jpg\" width=\"100\" />";

                // Find matches.
                Match regexMatch = Regex.Match(text, pattern);

                Console.WriteLine("*******************************************");

                // Report matches.
                if (regexMatch.Length == 0) Console.WriteLine("No matches found.");

                while (regexMatch.Success)
                {
                    Console.WriteLine($"Found {regexMatch.Groups[1]} at {regexMatch.Groups[1].Index}");
                    regexMatch = regexMatch.NextMatch();
                }

                Console.WriteLine("*******************************************");
            }
            catch (RegexMatchTimeoutException)
            {
                Console.WriteLine("The matching operation timed out.");
            }
        }
    }
}
