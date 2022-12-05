using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBunk
{
    public int Id { get; }
    public IEnumerable<IBunk> Neighbours { get; set; }
    public IEnumerable<Cell> Cells { get; set; }

    public void CalculateDistances();
}
