
using System.Collections.Generic;
using System.Management;
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
        this.ChunkSize = 25;
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

    public bool StartDrawing(bool[,,] _grid)
    {
        CalculateChunks(_grid);
        return true;
    }
    //  // \\ // \\ // \\
    private void CalculateChunks(bool[,,] _grid)
    {
        World world = World.GetInstance();
        Chunk.chunksize = ChunkSize;
        int chunksX = Mathf.CeilToInt(size[0] / ChunkSize);
        int chunksZ = Mathf.CeilToInt(size[1] / ChunkSize);
        Mesh[,] meshes = new Mesh[chunksX,chunksZ];
        for (int xc = 0; xc < chunksX; xc++)
        {
            for(int zc = 0; zc < chunksZ; zc++)
            {
                Chunk c = new Chunk(xc, zc);
                world.Chunks.Add(c);
                int[,] chunkInfo = new int[2, 2];
                chunkInfo[0, 0] = xc * ChunkSize;
                chunkInfo[0, 1] = xc * ChunkSize + ChunkSize;
                chunkInfo[1, 0] = zc * ChunkSize;
                chunkInfo[1, 1] = zc * ChunkSize + ChunkSize;
                meshes[xc,zc] = CalculateMesh(_grid, chunkInfo);

            }
        }
        GenerateMap(meshes);
    }
    // \\// \\ //  \\ //

    //  // \\ // \\ // \\
    private Mesh CalculateMesh(bool[,,] _grid, int[,] _chunkInfo)
    {
        Mesh mesh = new Mesh();

        mesh.subMeshCount = 3;

        List<int> Gtriangles = new List<int>();
        List<int> Wtriangles = new List<int>();
        List<int> Etriangles = new List<int>();

        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        for (int x = 0; x + _chunkInfo[0,0] < _chunkInfo[0, 1]; x++)
        {
            for (int y = 0; y < 1; y++)
            {
                for( int z = 0; z + _chunkInfo[1, 0] < _chunkInfo[1, 1]; z++)
                {
                    bool cell = _grid[(x + _chunkInfo[0, 0]), 0, (z + _chunkInfo[1, 0])];

                    Vector3[] v = quad.GetVerts(cell,x,z);
                    Vector2[] uv = quad.GetUVs(cell);
                    if (cell)
                    {
                        for (int k = 0; k < 6; k++)
                        {
                            vertices.Add(v[k]);
                            Wtriangles.Add(vertices.Count - 1);
                            uvs.Add(uv[k]);
                        }
                    }
                    else
                    {
                        for (int k = 0; k < 6; k++)
                        {
                            vertices.Add(v[k]);
                            Gtriangles.Add(vertices.Count - 1);
                            uvs.Add(uv[k]);
                        }
                    }


                    bool[] water;
                    if (!_grid[(x + _chunkInfo[0, 0]), 0, (z + _chunkInfo[1, 0])])
                    {
                        water = GetWaters(_grid, (x + _chunkInfo[0, 0]), (z + _chunkInfo[1, 0]));

                        if (water[0] == true)
                        {
                            Vector3[] ev = quad.GetEdges(Quad.direction.NORTH, x, z);
                            Vector2[] euv = quad.EdgeUVs();
                            for (int k = 0; k < 6; k++)
                            {
                                vertices.Add(ev[k]);
                                Etriangles.Add(vertices.Count - 1);
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
                                Etriangles.Add(vertices.Count - 1);
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
                                Etriangles.Add(vertices.Count - 1);
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
                                Etriangles.Add(vertices.Count - 1);
                                uvs.Add(euv[k]);
                            }
                        }
                    }
                }
            }
        }

        mesh.SetVertices(vertices);
        mesh.SetUVs(0,uvs);

        mesh.SetTriangles(Gtriangles.ToArray(),0, true, 0);
        mesh.SetTriangles(Etriangles.ToArray(), 1, true, 0);
        mesh.SetTriangles(Wtriangles.ToArray(), 2, true, 0);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();

        return mesh;
    }
    //  \\ // \\ // \\ //

    //  // \\ // \\ // \\
    private void GenerateMap(Mesh[,] meshes)
    {
        int chunksX = Mathf.CeilToInt(size[0] / ChunkSize);
        int chunksZ = Mathf.CeilToInt(size[1] / ChunkSize);
        GameObject chunk = map.pref;

        for(int x = 0; x < chunksX; x++)
        {
            for(int z = 0; z < chunksZ; z++)
            {
                
                chunk.GetComponent<MeshFilter>().mesh = meshes[x, z];
                Vector3 pos = new Vector3((x * ChunkSize), 0, (z * ChunkSize));

                WorldManager.Instantiate(chunk, pos, new Quaternion(0,0,0,0));
            }
        }
    }

    public bool[] GetWaters(bool[,,] _grid, int x, int z)
    {
        bool[] water = { false, false, false, false };
        try
        {
            if (_grid[x, 0, z + 1])
            {
                water[0] = true;
            }
        }
        catch { }
        try
        {
            if (_grid[x + 1, 0, z])
            {
                water[1] = true;
            }
        }
        catch { }
        try
        {
            if (_grid[x, 0, z - 1])
            {
                water[2] = true;
            }
        }
        catch { }
        try
        {
            if (_grid[x - 1, 0, z])
            {
                water[3] = true;
            }
        }
        catch { }
        return water;
    }
}
