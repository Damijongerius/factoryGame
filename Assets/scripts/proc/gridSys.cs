using System.Collections.Generic;
using UnityEngine;

public class gridSys : MonoBehaviour
{
    public float waterLevel = .4f;
    public float scale = .1f;
    public int size = 10;

    public GameObject water;
    public GameObject grass;
    public float[,] noiseMap;

    public Material terrainMaterial;

    Cell[,] grid;

    void Start()
    {

        noiseMap = new float[size, size];
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }

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
        for (int j = 0; j < 5; j++)
        {
            Smooth();
        }
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                if (cell.isWater)
                    Gizmos.color = Color.blue;
                else
                    Gizmos.color = Color.green;
                Vector3 pos = new Vector3(x, 0, y);
                Gizmos.DrawCube(pos, Vector3.one);
            }
        }
    }

    void Smooth()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                float CountTiles = GetOtherWall(x, y);
                noiseMap[x, y] = CountTiles;
                Vector3 pos = new Vector3(x, 0, y);
                Cell cell = grid[x, y];
                //if(cell.isWater && noiseMap[x,y] >= 12.5f)
                //{
                    Debug.Log(noiseMap[x, y]);
                //    cell.isWater = false;
                //}
                //if (cell.isWater)
                //{
                //    Instantiate(water, pos, Quaternion.Euler(0, 0, 0), transform);
                //}
                //else
                //{
                //    Instantiate(grass, pos, Quaternion.Euler(0, 0, 0), transform);
                //}
            }
        }
    }

    float GetOtherWall(int gridX, int gridY)
    {
        float wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < size && neighbourY >= 0 && neighbourY < size)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        //Debug.Log(noiseMap[neighbourX, neighbourY]);
                        wallCount += noiseMap[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }
}
