
using UnityEngine;
using UnityEngine.UIElements;

public class Map2
{
   // private ObjectSaveLoad objects = new ObjectSaveLoad();
    private World world = World.GetInstance();
    //3d cell2 x y z

    public GameObject pref;


    private readonly NoiseMap nm;
    private readonly DrawTerrain terrainGenerator;

    private SaveFile sf = SaveFile.GetInstance();
    //constructors
    //  // \\ // \\ // \\
    public Map2(GameObject pref, float[] _seed, int[] _size, Material[] _atlas)
    {
        world.size = _size;
        this.pref = pref;
        GenerateSeed(_seed);

        terrainGenerator = new DrawTerrain(this, _atlas, _size);

        nm = new NoiseMap(_size, _seed, this);
        nm.GenerateNoise();
    }
    public Map2(GameObject pref, int[] _size, Material[] _atlas)
    {
        world.size = _size;
        this.pref = pref;
        GenerateSeed();

        terrainGenerator = new DrawTerrain(this, _atlas, _size);

        nm = new NoiseMap(_size, world.seed, this);
        nm.GenerateNoise();
    }
    //  \\ // \\ // \\ //

    //creating the grid
    //  // \\ // \\ // \\
    public void CreateGrid(float[,] noiseMap)
    {
        Debug.Log(world.size[0] + "," + world.size[1]);
        world.Grid = new bool[world.size[0], 1, world.size[1]];

        for (int x = 0; x < world.size[0]; x++)
        {
            for (int y = 0; y < 1; y++)
            {
                for (int z = 0; z < world.size[1]; z++)
                {
                    if (noiseMap[x, z] > 0.3f)
                    {
                        world.Grid[x, y, z] = false;
                    }
                    else
                    {
                        world.Grid[x, y, z] = true;
                    }
                }
            }
        }
        terrainGenerator.StartDrawing(world.Grid);
        //Grid = objects.LoadSavedObjects(Grid);
    }
    //  \\ // \\ // \\ //


    //generating the world seed
    //  // \\ // \\ // \\
    private void GenerateSeed(float[] _seed)
    {
        if (_seed == null)
            GenerateSeed();
        else
            (world.seed[0], world.seed[1]) = (_seed[0], _seed[1]);
    }

    private void GenerateSeed()
    {
        (world.seed[0], world.seed[1]) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        sf.map.xRange = world.seed[0];
        sf.map.yRange = world.seed[1];
    }
    //  \\ // \\ // \\ //
}
