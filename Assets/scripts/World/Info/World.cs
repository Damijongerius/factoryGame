using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Windows.Forms.DataVisualization.Charting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using WorldObjects;

namespace World
{
    public sealed class World
    {
        private static World world;

        public int[] size = new int[2];
        public float[] seed = new float[2];
        public bool[,,] Grid;
        public List<ITile> tiles = new List<ITile>();

        public World(World w)
        {
            world = w;
        }

        private World() { }
        public static World GetInstance()
        {
            if (world == null)
            {
                world = new World();
            }
            return world;
        }

        public bool OnSet(List<Vector3> pos, Order orderObject, Vector2 relativePosition)
        {
            foreach (ITile tile in tiles)
            {
                List<Vector2> localPos = tile.GetPosition();
                foreach(Vector2 singlePos in localPos)
                {
                    if(Mathf.Abs(singlePos.x - relativePosition.x) + Mathf.Abs(singlePos.y - relativePosition.y) == 1)
                    {
                        tile.AddNeighbour(tile);
                    }
                }
            }

            return true;
        }

        private void ConfigureNeighbours()
        {

        }

        public void OnDelete(int x, int y)
        {
            foreach (ITile tile in tiles)
            {
                if (tile.GetPosition().Contains(new Vector2(x, y)){
                    tiles.Remove(tile);
                }
            }
        }

        public void OnDelete(ITile tile)
        {
            tiles.Remove(tile);
        }
    }
}