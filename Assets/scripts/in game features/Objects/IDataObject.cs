using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataObject
{
    public int Data { get; set; }
    public float UpkeepCost { get; set; }

    public float Age { get; set; }

    public IEnumerable<IDataObject> Neighbours { get; set; }

    public int DistanceToSell();
}
