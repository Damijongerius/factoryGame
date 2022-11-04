using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class DrawTerrain
{

    private readonly Map2 map;
    private readonly int[] size;

    private readonly Quad quad = new Quad();

    private readonly int ChunkSize;

    private readonly Material[] atlas;

    private WorldManager worldManager;

    //  // \\ // \\ // \\
    public DrawTerrain(Map2 _map, Material[] atlas, int[] _size)
    {
        this.map = _map;
        this.ChunkSize = 10;
        this.atlas = atlas;
        this.size = _size;

        worldManager = WorldManager.getInstance();
    }

    public DrawTerrain(Map2 _map, int _ChunkSize, Material[] atlas, int[] _size)
    {
        this.map = _map;
        this.ChunkSize = _ChunkSize;
        this.atlas = atlas;
        this.size = _size;
    }
    //  \\ // \\ // \\ //

    public bool StartDrawing(Cell2[,,] _grid)
    {
        CalculateChunks(_grid);
        return true;
    }
    //  // \\ // \\ // \\
    private void CalculateChunks(Cell2[,,] _grid)
    {
        int chunksX = Mathf.CeilToInt(size[0] / ChunkSize);
        int chunksZ = Mathf.CeilToInt(size[1] / ChunkSize);
        Mesh[,] meshes = new Mesh[chunksX,chunksZ];
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
                        meshes[xc,zc] = CalculateMesh(_grid, chunkInfo);
                    }
                }
            }
        }
        GenerateMap(meshes);
    }
    // \\// \\ //  \\ //

    //  // \\ // \\ // \\
    private Mesh CalculateMesh(Cell2[,,] _grid, int[,] _chunkInfo)
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        for (int x = 0; x + _chunkInfo[0,0] < _chunkInfo[0, 1]; x++)
        {
            for (int y = 0; y < 1; y++)
            {
                for( int z = 0; z + _chunkInfo[1, 0] < _chunkInfo[1, 1]; z++)
                {
                    Cell2 cell = _grid[(x + _chunkInfo[0, 0]), 0, (z + _chunkInfo[1, 0])];

                    Vector3[] v = quad.GetVerts(cell,x,z);
                    Vector2[] uv = quad.GetUVs(cell);

                    for (int k = 0; k < 6; k++)
                    {
                        vertices.Add(v[k]);
                        triangles.Add(triangles.Count);
                        uvs.Add(uv[k]);
                    }

                    bool[] water;
                    if (!_grid[(x + _chunkInfo[0, 0]), 0, (z + _chunkInfo[1, 0])].isWater)
                    {
                        water = cell.GetWaters(_grid, (x + _chunkInfo[0, 0]), (z + _chunkInfo[1, 0]));

                        if (water[0] == true)
                        {
                            Vector3[] ev = quad.GetEdges(Quad.direction.NORTH, x, z);
                            Vector2[] euv = quad.EdgeUVs();
                            for (int k = 0; k < 6; k++)
                            {
                                vertices.Add(ev[k]);
                                triangles.Add(triangles.Count);
                                uvs.Add(euv[k]);
                            }
                        }
                        if (water[1] == true)
                        {
                            Vector3[] ev = quad.GetEdges(Quad.direction.EAST, x, z);
                            Vector2[] euv = quad.EdgeUVs();
                            for (int k = 0; k < 6; k++)
                            {
                                vertices.Add(ev[k]);
                                triangles.Add(triangles.Count);
                                uvs.Add(euv[k]);
                            }
                        }
                        if (water[2] == true)
                        {
                            Vector3[] ev = quad.GetEdges(Quad.direction.SOUTH, x, z);
                            Vector2[] euv = quad.EdgeUVs();
                            for (int k = 0; k < 6; k++)
                            {
                                vertices.Add(ev[k]);
                                triangles.Add(triangles.Count);
                                uvs.Add(euv[k]);
                            }
                        }
                        if (water[3] == true)
                        {
                            Vector3[] ev = quad.GetEdges(Quad.direction.WEST, x, z);
                            Vector2[] euv = quad.EdgeUVs();
                            for (int k = 0; k < 6; k++)
                            {
                                vertices.Add(ev[k]);
                                triangles.Add(triangles.Count);
                                uvs.Add(euv[k]);
                            }
                        }
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
    private void GenerateMap(Mesh[,] meshes)
    {
        int chunksX = Mathf.CeilToInt(size[0] / ChunkSize);
        int chunksZ = Mathf.CeilToInt(size[1] / ChunkSize);
        GameObject chunk = map.pref;
        chunk.GetComponent<MeshRenderer>().material = atlas[0];

        for(int x = 0; x < chunksX; x++)
        {
            for(int z = 0; z < chunksZ; z++)
            {
                chunk.GetComponent<MeshFilter>().mesh = meshes[x , z];
                Vector3 pos = new Vector3((x * ChunkSize), 0, (z * ChunkSize));
                worldManager.init(chunk, pos);
            }
        }
    }
}
