using System.Globalization;

namespace ShopForHome.Api.Helpers
{
    public static class CsvParser
    {
        public static List<Dictionary<string, string>> Parse(Stream csvStream)
        {
            using var reader = new StreamReader(csvStream);
            var lines = new List<Dictionary<string, string>>();
            string? headerLine = reader.ReadLine();
            if (headerLine == null) return lines;

            var headers = headerLine.Split(',').Select(h => h.Trim()).ToArray();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var values = line.Split(',');
                var dict = new Dictionary<string, string>();
                for (int i = 0; i < headers.Length && i < values.Length; i++)
                {
                    dict[headers[i]] = values[i].Trim();
                }
                lines.Add(dict);
            }

            return lines;
        }
    }
}
