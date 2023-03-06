using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMap
{
    private readonly int[] size;

    private readonly Vector2 seed;

    private readonly Map2 map;

    public NoiseMap(int[] _size, Vector2 _seed, Map2 _map)
    {
        this.size = _size;
        this.seed = _seed;
        this.map = _map;
    }

    //generating noise of seed
    //  // \\ // \\ // \\
    public void GenerateNoise()
    {
        float[,] noiseMap = new float[size[0], size[1]];
        float scale = 0.1f;
        for (int x = 0; size[0] > x; x++)
        {
            for (int y = 0; size[1] > y; y++)
            {
                noiseMap[x, y] = Mathf.PerlinNoise(x * scale + seed[0], y * scale + seed[1]);
            }
        }
        noiseMap = ImplementMask(noiseMap);

        map.CreateGrid(noiseMap);
    }

    //scalable version
    public void GenerateNoise(float _scale)
    {
        float[,] noiseMap = new float[size[0], size[1]];
        for (int x = 0; size[0] > x; x++)
        {
            for (int y = 0; size[1] > y; y++)
            {
                noiseMap[x, y] = Mathf.PerlinNoise(x * _scale + seed[0], y * _scale + seed[1]);
            }
        }
        noiseMap = ImplementMask(noiseMap);

        map.CreateGrid(noiseMap);
    }
//  \\ // \\ // \\ //

    //implementing mask on the noise map
//  // \\ // \\ // \\
    private float[,] ImplementMask(float[,] noiseMap)
    {
        for (int x = 0; x < size[0]; x++)
        {
            for (int y = 0; y < size[1]; y++)
            {
                float xv = x / (float)size[0] * 2 - 1;
                float yv = y / (float)size[1] * 2 - 1;
                float v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs(yv));
                noiseMap[x, y] -= Mathf.Pow(v, 3f) / (Mathf.Pow(v, 3f) + Mathf.Pow(2.2f - 2.2f * v, 3f));
            }
        }
        return noiseMap;
    }
//  \\ // \\ // \\ //
}
