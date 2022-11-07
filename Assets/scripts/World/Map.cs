using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class Map2
{
    private ObjectSaveLoad objects = new ObjectSaveLoad();
    //3d cell2 x y z
    public Cell2[,,] Grid;

    private readonly int[] size;
    public GameObject pref; 

    private readonly float[] Seed = new float[2];

    //public float[] Seed => seed;

    private readonly NoiseMap nm;
    private readonly DrawTerrain terrainGenerator;
    //constructors
    //  // \\ // \\ // \\
    public Map2(GameObject pref,float[] _seed, int[] _size, Material[] _atlas)
    {
        this.size = _size;
        this.pref = pref;
        GenerateSeed(_seed);

        terrainGenerator = new DrawTerrain(this, _atlas, _size);

        nm = new NoiseMap(_size, _seed, this);
        nm.GenerateNoise();
    }
    public Map2(GameObject pref,int[] _size, Material[] _atlas)
    {
        this.size = _size;
        this.pref = pref;
        GenerateSeed();

        terrainGenerator = new DrawTerrain(this, _atlas, _size);

        nm = new NoiseMap(_size, Seed, this);
        nm.GenerateNoise();
    }
//  \\ // \\ // \\ //

    //creating the grid
//  // \\ // \\ // \\
    public void CreateGrid(float[,] noiseMap)
    {
        Debug.Log(size[0] +  "," + size[1]);
        Grid = new Cell2[size[0],1,size[1]];

        for(int x = 0; x < size[0]; x++)
        {
            for(int y = 0; y < 1; y++)
            {
                for(int z = 0; z < size[1]; z++)
                {
                    if (noiseMap[x,z] > 0.3f)
                    {
                        Grid[x, y, z] = new Cell2(false);
                    }
                    else
                    {
                        Grid[x, y, z] = new Cell2(true);
                    }
                }
            }
        }
        objects.LoadSavedObjects();
        terrainGenerator.StartDrawing(Grid);
    }
//  \\ // \\ // \\ //


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
        return Seed;
    }
}
