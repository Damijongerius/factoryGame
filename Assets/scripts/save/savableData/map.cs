        using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;

namespace World {
    public class Map
    {
        public Map(float _seedX, float _seedY) {
            SeedX = _seedX;
            SeedY = _seedY;
        }

        public Map()
        {

        }

        public float SeedX = 0;
        public float SeedY = 0;

        public float size;

        public TileObject[] objects;
        public void SetSize(float _size) => size = _size;
        

        public float GetSize() => size;

        public Vector2 GetSeed()
        {
            return new Vector2(SeedX, SeedY);
        }

        public void setSeed(float _seedX, float _seedY)
        {
            if(SeedX == 0)
            {
                SeedX = _seedX;
            }
            if(SeedY == 0)
            {
                SeedY = _seedY;
            }
        }

        public void GetObjects()
        {
            List<TileObject> tileObjects = new();
            World activeWorld = World.GetInstance();
            for(int i = 0; i < activeWorld.tiles.Count; i++)
            {
                tileObjects.Add(new TileObject(activeWorld.tiles[i]));
            }

            objects = tileObjects.ToArray();
        }

        public TileObject[] GetPreparedObjects()
        {
            return objects;
        }

    }

    public class TileObject
    {
        public float x;
        public float y;
        public string order;


        public TileObject(ITile tile)
        {
            Vector2 pos = tile.GetPosition()[0];
            x = pos.x;
            y = pos.y;
            order =  tile.GetType().ToString();
        }
    }
}


