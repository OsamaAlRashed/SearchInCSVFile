using SearchInCSVFile;

args.Bind(out string filePath, out int columnNumber, out string searchKey);

filePath.Read()
        .Search(columnNumber, searchKey)
        .ForEach(Console.WriteLine);