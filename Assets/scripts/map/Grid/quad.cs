using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quad
{
    public Mesh mesh;
    public Vector3[] Quad(int x, int y)
    {
        Vector3 a = new Vector3(x - .5f, 0, y + .5f);
        Vector3 b = new Vector3(x + .5f, 0, y + .5f);
        Vector3 c = new Vector3(x - .5f, 0, y - .5f);
        Vector3 d = new Vector3(x + .5f, 0, y - .5f);
        Vector2 uv00 = new Vector2(0, 0);
        Vector2 uv10 = new Vector2(1, 0);
        Vector2 uv01 = new Vector2(0, 1);
        Vector2 uv11 = new Vector2(1, 1);
        return new Vector3[] { a, b, c, b, d, c };
    }
}
