using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class World
{
    private static World world;

    public World(World w)
    {
        world = w;
    }

    private World() { }
    public static World GetInstance()
    {
        if(world == null)
        {
            world = new World();
        }
        return world;
    }

    public HashSet<Chunk> Chunks = new HashSet<Chunk>();
    public int[] size = new int[2];
    public float[] seed = new float[2];
    public bool[,,] Grid;

    public Chunk GetChunkWithPos(int _x, int _y)
    {
        int x = Mathf.FloorToInt(_x / Chunk.chunksize);
        int y = Mathf.FloorToInt(_y / Chunk.chunksize);

        foreach (Chunk chunk in Chunks)
            if (chunk.InRange(x, y)) return chunk;

        return null;
    }
}
