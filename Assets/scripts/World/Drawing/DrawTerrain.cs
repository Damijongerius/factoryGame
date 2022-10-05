using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DrawTerrain
{

    private readonly Map2 map;
    private readonly int[] size;

    private readonly Quad quad = new Quad();

    private readonly int ChunkSize;

    //  // \\ // \\ // \\
    public DrawTerrain(Map2 _map)
    {
        this.map = _map;
        this.ChunkSize = 16;
    }

    public DrawTerrain(Map2 _map, int _ChunkSize)
    {
        this.map = _map;
        this.ChunkSize = _ChunkSize;
    }
    //  \\ // \\ // \\ //

    //  // \\ // \\ // \\
    private void CalculateChunks()
    {
      //  int chunksX = Mathf.floor(size[0] / ChunkSize);
      //  int chunksY =
      //  for (int xc = 0; xc < )
    }

    //  // \\ // \\ // \\
    public void CalculateVertices(Cell2[,] _grid)
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        for (int y = 0; y < size[0]; y++)
        {
            for (int x = 0; x < size[0]; x++)
            {
                Cell2 cell = _grid[x, y];


                Vector3 a = new Vector3(x - .5f, 0, y + .5f);
                Vector3 b = new Vector3(x + .5f, 0, y + .5f);
                Vector3 c = new Vector3(x - .5f, 0, y - .5f);
                Vector3 d = new Vector3(x + .5f, 0, y - .5f);


                Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                Vector2[] uv = quad.GetUVs(cell);

                for (int k = 0; k < 6; k++)
                {
                    vertices.Add(v[k]);
                    triangles.Add(triangles.Count);
                    uvs.Add(uv[k]);
                }

            }
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
    }
    //  \\ // \\ // \\ //
}
