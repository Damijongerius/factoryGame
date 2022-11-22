using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad
{
    private WaterUVs water = new WaterUVs();
    private GrassUVs grass = new GrassUVs();

    public Vector2[] GetUVs(Cell2 _cell)
    {
        if(_cell != null)
        return _cell.isWater ? water.uv : grass.uv;
        else
        {
            return water.uv;
        }

    }

    public Vector3[] GetVerts(Cell2 _cell, int x,  int z)
    {
        if (_cell != null)
            return _cell.isWater ? water.getverts(x,z) : grass.getverts(x,z);
        else
        {
            return water.getverts(x, z);
        }
    }

    public Vector3[] GetEdges(direction dir, int x,int z)
    {
        return grass.getEdgeVerts(dir,x,z);
    }

    public Vector2[] EdgeUVs()
    {
        return grass.euv;
    }

    public class WaterUVs
    {
        Vector2 uv00 = new Vector2(0f, 0f);
        Vector2 uv10 = new Vector2(1f, 0f);
        Vector2 uv01 = new Vector2(1f, 1f);
        Vector2 uv11 = new Vector2(1f, 1f);
        public Vector2[] uv;

        public Vector3[] getverts(int x, int z)
        {
            Vector3 a = new Vector3(x - .5f, 0.15f, z + .5f);
            Vector3 b = new Vector3(x + .5f, 0.15f, z + .5f);
            Vector3 c = new Vector3(x - .5f, 0.15f, z - .5f);
            Vector3 d = new Vector3(x + .5f, 0.15f, z - .5f);

            return new Vector3[] { a, b, c, b, d, c };
        }

        public WaterUVs()
        {
             uv = new Vector2[] { uv00, uv10, uv01, uv10, uv11, uv01 };
        }

    }

    public  class GrassUVs
    {
        //uv's
        Vector2 uv00 = new Vector2(0f, 0f);
        Vector2 uv10 = new Vector2(1f, 0f);
        Vector2 uv01 = new Vector2(1f, 1f);
        Vector2 uv11 = new Vector2(1f, 1f);

        //ledges
        Vector3 i = new Vector3(.6f, 1, .6f);
        Vector3 j = new Vector3(.5f, 1, .5f);
        Vector3 k = new Vector3(.5f, 1, .5f);
        Vector3 l = new Vector3(.5f, 1, .5f);
        Vector3 m = new Vector3(.5f, 1, .5f);
        Vector3 n = new Vector3(.5f, 1, .5f);

        //edges
        Vector3[] north;

        Vector3[] east;

        Vector3[] south;

        Vector3[] west;


        public Vector2[] uv;
        public Vector2[] euv;
        public GrassUVs()
        {
            uv = new Vector2[] { uv00, uv10, uv01, uv10, uv11, uv01 };
            euv = new Vector2[] { uv00, uv10, uv01, uv10, uv11, uv01 };
        }

        public Vector3[] getverts(int x, int z)
        {
            Vector3 av = new Vector3(x -.5f, 0.5f, z +.5f);
            Vector3 bv = new Vector3(x +.5f, 0.5f, z  +.5f);
            Vector3 cv = new Vector3(x-.5f, 0.5f, z -.5f);
            Vector3 dv = new Vector3(x +.5f, 0.5f, z -.5f);

            return new Vector3[] { av, bv, cv, bv, dv, cv };
        }

        public Vector3[] getEdgeVerts(direction dir, int x, int z)
        {
            //corners of a square
            Vector3 a = new Vector3(x + .5f, .5f, z + .5f);
            Vector3 b = new Vector3(x - .5f, .5f, z + .5f);
            Vector3 c = new Vector3(x - .5f, -.5f, z + .5f);
            Vector3 d = new Vector3(x + .5f, -.5f, z + .5f);
            Vector3 e = new Vector3(x - .5f, .5f, z - .5f);
            Vector3 f = new Vector3(x + .5f, .5f, z - .5f);
            Vector3 g = new Vector3(x + .5f, -.5f, z - .5f);
            Vector3 h = new Vector3(x - .5f, -.5f, z - .5f);

            //faces of an inverted square
            switch (dir)
            {
                case (direction)0:
                    {
                        return new Vector3[] { a, b, c, d, a, c };
                    }
                case (direction)1:
                    {
                        return new Vector3[] { g, f, a, g, a, d };
                    }
                case (direction)2:
                    {
                        return new Vector3[] { e, f, g, e, g, h };
                    }
                case (direction)3:
                    {
                        return new Vector3[] { b, e, h, c, b, h };
                    }
                default:
                    {
                        return null;
                    }
            };
        }

    }
    public enum direction
    {
        NORTH = 0,
        EAST = 1,
        SOUTH = 2,
        WEST = 3
    }
}

