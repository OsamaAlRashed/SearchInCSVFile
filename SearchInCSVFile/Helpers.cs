using static SearchInCSVFile.Constants;

namespace SearchInCSVFile
{
    public static class Helpers
    {
        public static void Bind(this string[] args, out string filePath, out int columnNumber, out string searchKey)
        {
            if (args.Length != ArgumentsCount)
            {
                Console.WriteLine($"The number of arguments must be {ArgumentsCount}.");
                Environment.Exit(1);
            }

            filePath = args[0];
            if (!int.TryParse(args[1], out columnNumber))
            {
                Console.WriteLine("Column number must be integer.");
                Environment.Exit(2);
            }
            searchKey = args[2];
        }

        public static string[] Read(this string filePath)
        {
            try
            {
                string extension = Path.GetExtension(filePath);
                if (!AllowedExtensions.Contains(extension))
                {
                    Console.WriteLine($"The file type is not valid.");
                    Environment.Exit(3);
                    return null;
                }

                string[] lines = File.ReadAllText(filePath).Split(LineSplitter)
                    .Where(line => !string.IsNullOrWhiteSpace(line)).Select(line => line.Trim()).ToArray();

                if (lines.Length == 0)
                {
                    Console.WriteLine("The file is Empty.");
                    Environment.Exit(4);
                }
                return lines;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(5);
                return null;
            }
        }

        public static List<string> Search(this string[] lines, int columnNumber, string searchKey)
        {
            if (columnNumber > lines[0].Split(ColumnSplitter).Length - 1)
            {
                Console.WriteLine("The passed column number is greater than the number of columns.");
                Environment.Exit(6);
                return null;
            }

            var result = lines.Where(line => line.Split(ColumnSplitter)[columnNumber] == searchKey).ToList();

            if (!result.Any())
            {
                result.Add($"{searchKey} does not exist.");
            }

            return result;
        }
    }
}
