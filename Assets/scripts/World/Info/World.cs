using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Windows.Forms.DataVisualization.Charting;
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
        if (world == null)
        {
            world = new World();
        }
        return world;
    }

    public int[] size = new int[2];
    public float[] seed = new float[2];
    public bool[,,] Grid;
    public List<Tile> tiles = new List<Tile>();
    public List<Tile> Generators = new List<Tile>();

    public bool OnSet(int X, int Y, GameObject gameObject)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.PosistionCheck(X,Y))
            {
                return false;
            }
        }
        GrassTile grassTile = new GrassTile(X, Y, gameObject);
        tiles.Add(grassTile);

        foreach (Tile tile in tiles)
        {
            if (tile.PosistionCheck(X + 1, Y))
            {
                grassTile.AddNeighbour(tile.AddNeighbour(grassTile));
            }
            if (tile.PosistionCheck(X - 1, Y))
            {
                grassTile.AddNeighbour(tile.AddNeighbour(grassTile));
            }
            if (tile.PosistionCheck(X,Y + 1))
            {
                grassTile.AddNeighbour(tile.AddNeighbour(grassTile));
            }
            if (tile.PosistionCheck(X,Y - 1))
            {
                grassTile.AddNeighbour(tile.AddNeighbour(grassTile));
            }
        }
        return true;
    }

    public void OnDelete(int X, int y)
    {

    }
}