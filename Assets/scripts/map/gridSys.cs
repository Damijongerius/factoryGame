using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class gridSys : MonoBehaviour
{
    public ObjectSaveLoad objects = new ObjectSaveLoad();

    public Material atlas;
    public float waterLevel = .4f;
    public float scale = .1f;
    public int size = 100;

    private static gridSys sys;

    public GameObject[] placables;

    public static Cell[,] grid;

    private SaveFile sf = SaveFile.GetInstance();

    void Start()
    {
        sys = this;
        if (!ProfileManager.playing)
        {
            Generate();
        }   
    }
    public void Generate()
    {
        float[,] noiseMap = new float[size, size];
        float xOffset = 0;
        float yOffset = 0;
        if(sf.map.xRange == 0)
        {
            (xOffset, yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));

            sf.map.xRange = xOffset;
            sf.map.yRange = yOffset;
        }
        else
        {
            (xOffset, yOffset) = (sf.map.xRange, sf.map.yRange);
        }

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }
        
        //(float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));

        float[,] falloffMap = new float[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float xv = x / (float)size * 2 - 1;
                float yv = y / (float)size * 2 - 1;
                float v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs(yv));
                falloffMap[x, y] = Mathf.Pow(v, 3f) / (Mathf.Pow(v, 3f) + Mathf.Pow(2.2f - 2.2f * v, 3f));
            }
        }

        grid = new Cell[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = noiseMap[x, y];
                noiseValue -= falloffMap[x, y];
                bool isWater = noiseValue < waterLevel;
                Cell cell = new Cell(isWater);
                grid[x, y] = cell;
            }
        }

        objects.LoadSavedObjects();
        DrawTerrainMesh(grid);
        DrawTexture(grid);
    }

    void DrawTerrainMesh(Cell[,] grid)
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];


                Vector3 a = new Vector3(x - .5f, 0, y + .5f);
                Vector3 b = new Vector3(x + .5f, 0, y + .5f);
                Vector3 c = new Vector3(x - .5f, 0, y - .5f);
                Vector3 d = new Vector3(x + .5f, 0, y - .5f);


                Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                Vector2[] uv = getUvs(cell);

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

        
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        try
        {
            meshFilter.mesh = mesh;
        }
        catch
        {
            Debug.Log("couldn't preform that action");
        }

        MeshRenderer meshRenderer;
        
        if(gameObject.GetComponent<MeshRenderer>() != null)
        {
            meshRenderer = gameObject.GetComponent<MeshRenderer>();
        }
        else
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }
    }

    void DrawTexture(Cell[,] grid)
    {

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = atlas;
    }

    private Vector2[] getUvs(Cell cell)
    {
        
        if (!cell.isWater)
        {
            Vector2 uv00 = new Vector2(0.04f, 0.04f);
            Vector2 uv10 = new Vector2(0.46f, 0.04f);
            Vector2 uv01 = new Vector2(0.46f, 0.46f);
            Vector2 uv11 = new Vector2(0.04f, 0.46f);
            Vector2[] uv = new Vector2[] { uv00, uv10, uv01, uv10, uv11, uv01 };
            return uv;
        }
        else
        {
            Vector2 uv00 = new Vector2(0.54f, 0.54f);
            Vector2 uv10 = new Vector2(0.54f, 0.96f);
            Vector2 uv01 = new Vector2(0.96f, 0.54f);
            Vector2 uv11 = new Vector2(0.96f, 0.96f);
            Vector2[] uv = new Vector2[] { uv00, uv10, uv01, uv10, uv11, uv01 };
            return uv;
        }
    }

    public static gridSys GetInstance()
    {
        return sys;
    }
    
}