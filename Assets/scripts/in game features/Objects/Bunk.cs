using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunk : IBunk
{
    public int Id => throw new System.NotImplementedException();
    public IEnumerable<Cell> Cells { get => _Cells ; set => OnSet(value); }
    private IEnumerable<Cell> _Cells = new List<Cell>();
    IEnumerable<IBunk> IBunk.Neighbours { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


    private void OnSet(IEnumerable<Cell> cells)
    {
        //recalculate routes of object
        _Cells = cells;
    }

    public void CalculateDistances()
    {
        throw new System.NotImplementedException();
    }
}
