using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DrawTerrain
{

    private readonly Map2 map;
    private readonly int[] size;

    private readonly Quad quad = new Quad();

    private readonly int ChunkSize;

    private readonly Material atlas;

    private WorldManager worldManager;

    //  // \\ // \\ // \\
    public DrawTerrain(Map2 _map, Material atlas)
    {
        this.map = _map;
        this.ChunkSize = 16;
        this.atlas = atlas;

        worldManager = WorldManager.getInstance();
    }

    public DrawTerrain(Map2 _map, int _ChunkSize, Material atlas)
    {
        this.map = _map;
        this.ChunkSize = _ChunkSize;
        this.atlas = atlas;
    }
    //  \\ // \\ // \\ //

    public bool StartDrawing(Cell2[,] _grid)
    {
        CalculateChunks(_grid);
        return true;
    }
    //  // \\ // \\ // \\
    private void CalculateChunks(Cell2[,] _grid)
    {
        int chunksX = Mathf.CeilToInt(size[0] / ChunkSize);
        int chunksZ = Mathf.CeilToInt(size[1] / ChunkSize);
        List<Mesh> meshes = new List<Mesh>();
        for (int xc = 0; xc < chunksX; xc++)
        {
            for(int zc = 0; zc < chunksZ; zc++)
            {
                if ( size[0] - (chunksX * xc) >= ChunkSize)
                {
                    if ( size[0] - (chunksZ * zc) >= ChunkSize)
                    {
                        int[,] chunkInfo = new int[2, 2];
                        chunkInfo[0, 0] = xc * ChunkSize;
                        chunkInfo[0, 1] = xc * ChunkSize + ChunkSize;
                        chunkInfo[1, 0] = zc * ChunkSize;
                        chunkInfo[1, 1] = zc * ChunkSize + ChunkSize;
                        meshes.Add(CalculateMesh(_grid, chunkInfo));
                    }
                }
            }
        }
        GenerateMap(meshes);
    }
    // \\// \\ //  \\ //

    //  // \\ // \\ // \\
    private Mesh CalculateMesh(Cell2[,] _grid, int[,] _chunkInfo)
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        for (int x = _chunkInfo[0, 0]; x < _chunkInfo[0, 1]; x++)
        {
            for (int y = 0; y < 1; y++)
            {
                for( int z = _chunkInfo[1, 0]; z < _chunkInfo[1, 1]; z++)
                {
                    Cell2 cell = _grid[x, y];


                    //verplaats naar quad
                    Vector3 a = new Vector3(x - .5f, 0, y + .5f);
                    Vector3 b = new Vector3(x + .5f, 0, y + .5f);
                    Vector3 c = new Vector3(x - .5f, 0, y - .5f);
                    Vector3 d = new Vector3(x + .5f, 0, y - .5f);
                    //

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
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        return mesh;
    }
    //  \\ // \\ // \\ //

    //  // \\ // \\ // \\
    private void GenerateMap(List<Mesh> meshes)
    {
        int chunksX = Mathf.CeilToInt(size[0] / ChunkSize);
        int chunksZ = Mathf.CeilToInt(size[1] / ChunkSize);
        GameObject chunk = new GameObject();
        chunk.AddComponent<MeshRenderer>();
        MeshRenderer  mr = new MeshRenderer();
        mr.material = atlas;
        chunk.AddComponent<MeshFilter>();

        for(int x = 0; x < chunksX; x++)
        {
            for(int z = 0; z < chunksZ; z++)
            {
                Vector3 pos = new Vector3(x * ChunkSize, 0, z * ChunkSize);
                //worldManager.Instantiate(mr, pos,Quaternion.Euler(0,0,0), worldManager.transform);
            }
        }
    }
}
