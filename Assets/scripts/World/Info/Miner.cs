using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorldObjects
{
    public class Miner : Structures
    {
        private float speed;

        public DataTypes data;

        public List<Station> stations;

        public List<List<Tile>> paths;

        private List<Tile> Wires;

        //calculate the distance to rest
        public void OnCalculate()
        {
            foreach(List<Tile> tiles in paths)
            {
                float leak = (tiles.Count - 2) / 100;
                Cycle cycle = new Cycle();
                cycle.delay = 1.5f;
                cycle.amount = (data.standardAmount / paths.Count) - leak;
                cycle.duration = (tiles.Count - 2);
                cycle.InitTimer();

                Station s = (Station)tiles.Last().structure;
                s.cycle.Add(cycle);
            }
        }
    }
}
