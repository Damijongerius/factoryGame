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

            WorldLayers = new WorldLayer[12];
        }

        private float SeedX { get; }
        private float SeedY { get; }

        private float size;

        private WorldLayer[] WorldLayers { get; set; }

        public void SetSize(float _size) => size = _size;
        

        public float GetSize() => size;

        public Vector2 GetSeed()
        {
            return new Vector2(SeedX, SeedY);
        }

        public WorldLayer GetWorldLayer(int layer)
        {
            foreach(WorldLayer worldLayer in WorldLayers) 
            {
                if(worldLayer.GetLayer() == layer) return worldLayer;
            }
            return null;
        }

        public void AddWorldLayer(int layer)
        {
            WorldLayers[^1] = new WorldLayer(layer);
        }
    }
}
