using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase
{
    internal class Record
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Turns { get; set; }

        public Record(string name, int score, int turns)
        {
            Name = name;
            Score = score;
            Turns = turns;
        }
    }

    internal class Game
    {
        private List<Record> records = new List<Record>
        {
            new Record("Arrow Alpha", 1010, 40),
            new Record("Brick Beast", 1009, 40),
            new Record("Cyber Clash", 1008, 40),
            new Record("Drake Dream", 1007, 40),
            new Record("Eagle Eraze", 1006, 40),
            new Record("Frost Flare", 1005, 40),
            new Record("Glide Ghost", 1004, 40),
            new Record("Hyper Haven", 1003, 40),
            new Record("Icelo Index", 1002, 40),
            new Record("Joker Juice", 1001, 40),
        };

        public void AddRecord(string name, int score, int turns)
        {
            records.Add(new Record(name, score, turns));
        }

        public List<Record> InsertionSort()
        {
            List<Record> sortedRecords = new List<Record>(records);

            for (int i = 1; i < sortedRecords.Count; i++)
            {
                Record currentRecord = sortedRecords[i];
                int j = i - 1;

                while (j >= 0 && sortedRecords[j].Score < currentRecord.Score)
                {
                    sortedRecords[j + 1] = sortedRecords[j];
                    j--;
                }
                sortedRecords[j + 1] = currentRecord;
            }
            return sortedRecords;
        }
    }
}
