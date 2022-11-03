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
    public DrawTerrain(Map2 _map, Material atlas, int[] _size)
    {
        this.map = _map;
        this.ChunkSize = 10;
        this.atlas = atlas;
        this.size = _size;

        worldManager = WorldManager.getInstance();
    }

    public DrawTerrain(Map2 _map, int _ChunkSize, Material atlas, int[] _size)
    {
        this.map = _map;
        this.ChunkSize = _ChunkSize;
        this.atlas = atlas;
        this.size = _size;
    }
    //  \\ // \\ // \\ //

    public bool StartDrawing(Cell2[,,] _grid)
    {
        Debug.Log("start drawing");
        CalculateChunks(_grid);
        return true;
    }
    //  // \\ // \\ // \\
    private void CalculateChunks(Cell2[,,] _grid)
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
                        Debug.Log(chunkInfo[0, 0] + "," + chunkInfo[0, 1] + "," + chunkInfo[1, 0] + "," + chunkInfo[1, 1]);
                        meshes.Add(CalculateMesh(_grid, chunkInfo));
                    }
                }
            }
        }
        Debug.Log(meshes.Count);
        GenerateMap(meshes);
    }
    // \\// \\ //  \\ //

    //  // \\ // \\ // \\
    private Mesh CalculateMesh(Cell2[,,] _grid, int[,] _chunkInfo)
    {
        Debug.Log("mesh");
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
                    Cell2 cell = _grid[x,0, z];


                    //verplaats naar quad
                    Vector3 a = new Vector3(x - .5f, 0, z + .5f);
                    Vector3 b = new Vector3(x + .5f, 0, z + .5f);
                    Vector3 c = new Vector3(x - .5f, 0, z - .5f);
                    Vector3 d = new Vector3(x + .5f, 0, z - .5f);
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
        Debug.Log("return mesh");
        return mesh;
    }
    //  \\ // \\ // \\ //

    //  // \\ // \\ // \\
    private void GenerateMap(List<Mesh> meshes)
    {
        int chunksX = Mathf.CeilToInt(size[0] / ChunkSize);
        int chunksZ = Mathf.CeilToInt(size[1] / ChunkSize);
        GameObject chunk = map.pref;
        chunk.GetComponent<MeshRenderer>().material = atlas;

        for(int x = 0; x < chunksX; x++)
        {
            for(int z = 0; z < chunksZ; z++)
            {
                chunk.GetComponent<MeshFilter>().mesh = meshes[x + z];
                Vector3 pos = new Vector3(0, 0, 0);
                worldManager.init(chunk, pos);
            }
        }
    }
}
