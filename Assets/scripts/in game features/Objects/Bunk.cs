using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunk : IBunk
{
    public IEnumerable<Cell> Neighbours { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public int Id => throw new System.NotImplementedException();

    public void CalculateDistances()
    {
        throw new System.NotImplementedException();
    }
}
