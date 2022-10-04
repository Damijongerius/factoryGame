using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class Map2
{
    //3d cell2 x y z
    private Cell2[,,] Grid;

    private int[] size;

    private readonly float[] seed;

    public float[] Seed => seed;

    private readonly NoiseMap nm;
    private readonly DrawTerrain terrainGenerator;
    //constructors
    //  // \\ // \\ // \\
    public Map2(float[] _seed, int[] _size)
    {
        GenerateSeed(_seed);

        terrainGenerator = new DrawTerrain(this);

        nm = new NoiseMap(_size, _seed, this);
        nm.GenerateNoise();

        Debug.Log(Mathf.PI);
    }
    Map2(int[] size)
    {
        GenerateSeed();

        //GenerateNoise();
    }
//  \\ // \\ // \\ //

    //creating the grid
//  // \\ // \\ // \\
    public void CreateGrid(float[,] noiseMap)
    {
        Grid = new Cell2[size[0],1,size[1]];

        for(int x = 0; x < size[0]; x++)
        {
            for(int y = 0; y < 1; y++)
            {
                for(int z = 0; z < size[1]; z++)
                {
                    if (noiseMap[x,z] < 0.45f)
                    {
                        Grid[x, y, z].isWater = true;
                    }
                }
            }
        }


    }


    //generating the world seed
//  // \\ // \\ // \\
    private void GenerateSeed(float[] _seed)
    {
        if(_seed == null)
        {
            (Seed[0], Seed[1]) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        }
        else
        {
            (Seed[0], Seed[1]) = (_seed[0], _seed[1]);
        }
    }

    private void GenerateSeed()
    {
        (Seed[0], Seed[1]) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
    }
//  \\ // \\ // \\ //

    public float[] getSeed()
    {
        return seed;
    }
}
