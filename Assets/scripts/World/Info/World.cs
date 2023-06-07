using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
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

        public bool OnSet(List<Vector3> positions, Order orderObject, GameObject gameObject)
        {

            List<ITile> neighbours = new List<ITile>();
            foreach (ITile tile in tiles)
            {
                List<Vector2> existingPos = tile.GetPosition();
                foreach(Vector2 singlePos in existingPos)
                {
                    foreach (Vector3 position in positions)
                    {
                        if (Mathf.Abs(singlePos.x - position.x) + Mathf.Abs(singlePos.y - position.y) == 1)
                        {
                            neighbours.Add(tile);
                            tile.AddNeighbour(tile);
                        }
                    }
                }
            }

            return true;
        }

        private ITile TileSelection(List<Vector3> positions, Order orderObject, GameObject gameObject, List<ITile> neighbours)
        {
            List<Vector2> v2Positions = new List<Vector2>();
            foreach(Vector3 pos in positions)
            {
                v2Positions.Add(pos);
            }

            return orderObject switch
            {
                Order.PowerLine or Order.WaterPipe => new TileGroup(orderObject, neighbours, v2Positions, (int)positions.First().y, new List<GameObject>() { gameObject }),
                Order.StoryFactory1 => new TileHolder(orderObject, neighbours, v2Positions, (int)positions.First().y, new List<GameObject>() { gameObject }),
                _ => new BasicTile(orderObject, neighbours, v2Positions, (int)positions.First().y, new List<GameObject>() { gameObject }),
            };
        }

        private void ConfigureNeighbours()
        {

        }

        public void OnDelete(int x, int y)
        {
            foreach (ITile tile in tiles)
            {
                if (tile.GetPosition().Contains(new Vector2(x, y))){
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