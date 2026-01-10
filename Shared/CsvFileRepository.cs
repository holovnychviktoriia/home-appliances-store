using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeAppliancesStore.Shared
{
    public class CsvFileRepository<T>
    {
        private readonly string _filePath;
        private readonly ICsvParser<T> _parser;

        public CsvFileRepository(string filePath, ICsvParser<T> parser)
        {
            _filePath = filePath;
            _parser = parser;
        }

        public List<T> ReadAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }

            var lines = File.ReadAllLines(_filePath);
            var items = new List<T>();

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                items.Add(_parser.Parse(lines[i]));
            }

            return items;
        }

        public void WriteAll(List<T> items, string header)
        {
            var lines = new List<string> { header };
            foreach (var item in items)
            {
                lines.Add(_parser.ToCsv(item));
            }
            File.WriteAllLines(_filePath, lines);
        }

        public void Append(T item)
        {
            var line = _parser.ToCsv(item);
            File.AppendAllText(_filePath, line + "\n");
        }
    }
}