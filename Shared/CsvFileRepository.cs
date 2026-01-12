// <copyright file="CsvFileRepository.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.IO;

namespace HomeAppliancesStore.Shared
{
    public class CsvFileRepository<T>
    {
        private readonly string filePath;
        private readonly ICsvParser<T> parser;

        public CsvFileRepository(string filePath, ICsvParser<T> parser)
        {
            this.filePath = filePath;
            this.parser = parser;
        }

        public List<T> ReadAll()
        {
            if (!File.Exists(this.filePath))
            {
                return new List<T>();
            }

            var lines = File.ReadAllLines(this.filePath);
            var items = new List<T>();

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                items.Add(this.parser.Parse(lines[i]));
            }

            return items;
        }

        public void WriteAll(List<T> items, string header)
        {
            var lines = new List<string> { header };
            foreach (var item in items)
            {
                lines.Add(this.parser.ToCsv(item));
            }

            File.WriteAllLines(this.filePath, lines);
        }

        public void Append(T item)
        {
            var line = this.parser.ToCsv(item);
            File.AppendAllText(this.filePath, line + "\n");
        }
    }
}
