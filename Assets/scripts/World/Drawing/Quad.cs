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
        Vector2 uv00 = new Vector2(0.54f, 0.54f);
        Vector2 uv10 = new Vector2(0.54f, 0.96f);
        Vector2 uv01 = new Vector2(0.96f, 0.54f);
        Vector2 uv11 = new Vector2(0.96f, 0.96f);
        public Vector2[] uv;

        public WaterUVs()
        {
             uv = new Vector2[] { uv00, uv10, uv01, uv10, uv11, uv01 };
        }

    }

    public  class GrassUVs
    {
        Vector2 uv00 = new Vector2(0.04f, 0.04f);
        Vector2 uv10 = new Vector2(0.46f, 0.04f);
        Vector2 uv01 = new Vector2(0.46f, 0.46f);
        Vector2 uv11 = new Vector2(0.04f, 0.46f);
        public Vector2[] uv;
        public GrassUVs()
        {
            uv = new Vector2[] { uv00, uv10, uv01, uv10, uv11, uv01 };
        }

        //cube
        Vector3 a = new Vector3(-.5f, 1,  .5f);
        Vector3 b = new Vector3( .5f, 1,  .5f);
        Vector3 c = new Vector3(-.5f, 1, -.5f);
        Vector3 d = new Vector3( .5f, 1, -.5f);
        Vector3 e = new Vector3(-.5f, 0,  .5f);
        Vector3 f = new Vector3( .5f, 0,  .5f);
        Vector3 g = new Vector3(-.5f, 0, -.5f);
        Vector3 h = new Vector3( .5f, 0, -.5f);

        //edges
        Vector3 i = new Vector3(.6f, 1, .6f);
        Vector3 j = new Vector3(.5f, 1, .5f);
        Vector3 k = new Vector3(.5f, 1, .5f);
        Vector3 l = new Vector3(.5f, 1, .5f);
        Vector3 m = new Vector3(.5f, 1, .5f);
        Vector3 n = new Vector3(.5f, 1, .5f);
    }
}

