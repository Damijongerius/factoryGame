using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBunk
{
    public int Id { get; }
    public IEnumerable<Cell> Neighbours { get; set; }
    public void CalculateDistances();
}
