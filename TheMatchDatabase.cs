using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityServerAPI
{
    class Entry
    {
        public DateTime Time { get; set; }
        public ulong steamId { get; set; }
        public int entryType { get; set; }
    }

    class TheMatchDatabase
    {
        private List<Entry> entries;

        public TheMatchDatabase()
        {
            entries = new List<Entry>();
        }

        public void AddEntry(DateTime time, ulong steamId, int entryType)
        {
            Entry newEntry = new Entry
            {
                Time = time,
                steamId = steamId,
                entryType = entryType
            };
            entries.Add(newEntry);
        }
        public List<Entry> GetEntries()
        {
            return entries;
        }

        public Entry FindNearestEntry(Entry theEntry)
        {
            Entry closestEntry = null;
            TimeSpan closestDifference = TimeSpan.MaxValue;

            foreach (Entry entry in entries)
            {
                //no need to calculate if we see source == each other. We only care when they are different.
                if (entry.entryType != theEntry.entryType)
                {
                    TimeSpan difference = entry.Time - theEntry.Time;

                    if (difference < closestDifference)
                    {
                        closestDifference = difference;
                        closestEntry = entry;
                    }
                }
            }
            return closestEntry;
        }
    }

}
