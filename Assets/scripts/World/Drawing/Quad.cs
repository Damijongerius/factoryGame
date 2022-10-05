using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad
{
    private WaterUVs water = new WaterUVs();
    private GrassUVs grass = new GrassUVs();

    public Vector2[] GetUVs(Cell2 _cell)
    {
        
        if (_cell.isWater)
            return water.uv;
        else
            return grass.uv;

    }

    public class WaterUVs
    {
        public Vector2 uv00 = new Vector2(0.51f, 0.51f);
        public Vector2 uv10 = new Vector2(0.51f, 1);
        public Vector2 uv01 = new Vector2(1f, 0.51f);
        public Vector2 uv11 = new Vector2(1, 1);
        public Vector2[] uv;

        public WaterUVs()
        {
             uv = new Vector2[] { uv00, uv10, uv01, uv10, uv11, uv01 };
        }
    }

    public  class GrassUVs
    {
        public Vector2 uv00 = new Vector2(0, 0);
        public Vector2 uv10 = new Vector2(0.49f, 0);
        public Vector2 uv01 = new Vector2(0.49f, 0.49f);
        public Vector2 uv11 = new Vector2(0, 0.49f);
        public Vector2[] uv;
        public GrassUVs()
        {
            uv = new Vector2[] { uv00, uv10, uv01, uv10, uv11, uv01 };
        }
    }
}
