using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WorldObjects
{
    public class Miner : Structures
    {
        private float speed;

        public DataTypes data = new DataTypes();

        public List<Station> stations;

        public List<List<Tile>> paths = new();

        public List<Cycle> cycles = new();

        //calculate the distance to rest
        public new void OnCalculate()
        {
            data.standardAmount = 1;
            Debug.Log("cycle");
            foreach(List<Tile> tiles in paths)
            {
                float leak = (tiles.Count - 2) / 100;
                Cycle cycle = new Cycle();
                cycle.delay = 1.5f;
                cycle.amount = (data.standardAmount / paths.Count) - leak;
                cycle.duration = (tiles.Count - 2);

                Debug.Log("station start cycle");
                
                if(tiles[^1] is Station)
                    Station s = (Station)tiles[^1].structure;
                
                s.AddCycle(cycle);
            }
        }

        public new void AddPath(List<Tile> list)
        {
            paths.Add(list);
            OnCalculate();
        }
    }
}
