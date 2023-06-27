        using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;

namespace World {
    public class Map
    {
        public Map(int _seedX, int _seedY) {
            SeedX = _seedX;
            SeedY = _seedY;
        }

        private float SeedX { get; }
        private float SeedY { get; }

        private float size;

        private TileObject[] objects;
        public void SetSize(float _size) => size = _size;
        

        public float GetSize() => size;

        public Vector2 GetSeed()
        {
            return new Vector2(SeedX, SeedY);
        }

        public void GetObjects()
        {
            World activeWorld = World.GetInstance();
            for(int i = 0; i < activeWorld.tiles.Count; i++)
            {
                objects[i] = new TileObject(activeWorld.tiles[i]);
            }
        }

    }

    class TileObject
    {
        float x;
        float y;
        string order;


        public TileObject(ITile tile)
        {
            Vector2 pos = tile.GetPosition()[0];
            x = pos.x;
            y = pos.y;
            order =  tile.GetType().ToString();
        }
    }
}


