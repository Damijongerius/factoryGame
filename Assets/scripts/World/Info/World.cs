using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Windows.Forms.DataVisualization.Charting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

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

        public bool OnSet(int X, int Y, GameObject gameObject, int index)
        {
            foreach (ITile tile in tiles)
            {
                if (tile.PosistionCheck(X, Y)) return false;
            }
            GrassTile grassTile = new(X, Y, gameObject, index);
            tiles.Add(grassTile);

            foreach (ITile tile in tiles)
            {
                if (tile.PosistionCheck(X + 1, Y))
                    grassTile.AddNeighbour(tile.AddNeighbour(grassTile));

                if (tile.PosistionCheck(X - 1, Y))
                    grassTile.AddNeighbour(tile.AddNeighbour(grassTile));

                if (tile.PosistionCheck(X, Y + 1))
                    grassTile.AddNeighbour(tile.AddNeighbour(grassTile));

                if (tile.PosistionCheck(X, Y - 1))
                    grassTile.AddNeighbour(tile.AddNeighbour(grassTile));
            }

            List<ITile> L = new();
            L.Add(grassTile);
            if (index == 0)
            {
                return true;
            }
            return true;
        }

        private void ConfigureNeighbours()
        {

        }

        public void OnDelete(int X, int Y)
        {
            foreach (ITile tile in tiles)
            {
                if (tile.PosistionCheck(X, Y))
                    tiles.Remove(tile);
            }
        }

        public void OnDelete(ITile tile)
        {
            tiles.Remove(tile);
        }
    }
}