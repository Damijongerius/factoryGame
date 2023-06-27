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

        public delegate void WorldDelegate();
        public WorldDelegate change;

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

            ITile result = TileSelection(positions,orderObject,gameObject);
            world.tiles.Add(result);
            change();
            return result;
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
                Order.CoalPlant => new CoalPlant(),
                Order.OilPlant => new OilPlant(),
                Order.PotatoPlant => new PotatoPlant(),
                Order.SolarPanel => new SolarFarm(),
                Order.WindMill => new  WindMill(),
                Order.StoryFactory1 => new Factory(),
                Order.LeadAcidBattery => new LeadAcidBattery(),
                Order.LithiumIonBattery => new LithiumIonBattery(),
                Order.SodiumSulphurBattery => new SodiumSulphurBattery(),
                Order.DataCenter => new DataCenter(),
                Order.DataStockCenter => new DataStockCenter(),
                Order.SatelliteDish => new SatelliteDish(),
                _ => throw new NotImplementedException(),
            };
        }

        private float GetPrice(Order orderObject)
        {
            return orderObject switch
            {
                Order.CoalPlant => new CoalPlant().Price(),
                Order.OilPlant => new OilPlant().Price(),
                Order.PotatoPlant => new PotatoPlant().Price(),
                Order.SolarPanel => new SolarFarm().Price(),
                Order.WindMill => new WindMill().Price(),
                Order.StoryFactory1 => new Factory().Price(),
                Order.LeadAcidBattery => new LeadAcidBattery().Price(),
                Order.LithiumIonBattery => new LithiumIonBattery().Price(),
                Order.SodiumSulphurBattery => new SodiumSulphurBattery().Price(),
                Order.DataCenter => new DataCenter().Price(),
                Order.DataStockCenter => new DataStockCenter().Price(),
                Order.SatelliteDish => new SatelliteDish().Price(),
                _ => throw new NotImplementedException(),
            };
        }

        public void OnDelete(int x, int y)
        {
            foreach (ITile tile in tiles)
            {
                if (tile.GetPosition().Contains(new Vector2(x, y))){
                    tiles.Remove(tile);
                    change();
                }
            }
        }

        public void OnDelete(ITile tile)
        {
            tiles.Remove(tile);
        }
    }
}