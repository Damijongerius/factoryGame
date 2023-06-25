using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

        public ITile OnSet(List<Vector3> positions, Order orderObject, GameObject gameObject)
        {
            Debug.Log("continue here");
            return TileSelection(positions,orderObject,gameObject);
        }

        private ITile TileSelection(List<Vector3> positions, Order orderObject, GameObject gameObject)
        {
            List<Vector2> v2Positions = new();
            foreach(Vector3 pos in positions)
            {
                v2Positions.Add(new Vector2(pos.x,pos.z));
            }

            ITile tile = orderObject switch
            {
                Order.PowerLine or Order.WaterPipe => new TileGroup(),
                Order.StoryFactory1 => new TileHolder(),
                _ => new BasicTile(),
            };

            Dictionary<Vector2,GameObject> positionsObjects = new Dictionary<Vector2,GameObject>();
            foreach(Vector2 pos in v2Positions)
            {
                positionsObjects.Add(pos,gameObject);
            }
            tile.Init(positionsObjects, BehaviorSelection(orderObject),orderObject);
            return tile;

        }

        private ITileBehavior BehaviorSelection(Order orderObject)
        {
            return orderObject switch
            {
                Order.CoalPlant => throw new NotImplementedException(),
                Order.OilPlant => throw new NotImplementedException(),
                Order.PotatoPlant => throw new NotImplementedException(),
                Order.SolarPanel => throw new NotImplementedException(),
                Order.WindMill => throw new NotImplementedException(),
                Order.StoryFactory1 => throw new NotImplementedException(),
                Order.DataCable => throw new NotImplementedException(),
                Order.PowerCable => throw new NotImplementedException(),
                Order.PowerProcessor => throw new NotImplementedException(),
                Order.PowerStorageModule => throw new NotImplementedException(),
                Order.LeadAcidBattery => throw new NotImplementedException(),
                Order.LithiumIonBattery => throw new NotImplementedException(),
                Order.SodiumSulphurBattery => throw new NotImplementedException(),
                Order.WaterStorage => throw new NotImplementedException(),
                Order.WaterFilterCenter => throw new NotImplementedException(),
                Order.WaterPipeExtractor => throw new NotImplementedException(),
                Order.WaterTower => throw new NotImplementedException(),
                Order.WaterPipe => throw new NotImplementedException(),
                Order.PowerAmplifier => throw new NotImplementedException(),
                Order.PowerDemper => throw new NotImplementedException(),
                Order.PowerLine => throw new NotImplementedException(),
                Order.PowerMerger => throw new NotImplementedException(),
                Order.PowerSplitter => throw new NotImplementedException(),
                Order.DataCenter => throw new NotImplementedException(),
                Order.DataStockCenter => throw new NotImplementedException(),
                Order.SatelliteDish => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
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