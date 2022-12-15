using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldObjects;

namespace WorldObjects
{
    public class Station : Structures
    {
        public List<Cycle> cycle = new();

        public new void AddCycle(Cycle c) 
        {
            cycle.Add(c);

            c.InitTimer();
        }

        public new void ResetCycles() => cycle.Clear();

        public new List<Cycle> GetCycles() => cycle;
    }
}