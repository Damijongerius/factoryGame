using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInfo
{
    [Range(0, 50)]
    public int dataStored;

    [Range(0f, 100f)]
    public float powerStored;

    public int Level;

    public int age;

    public int upkeepCost;

}

public class Miner : ObjInfo
{
    public int dataMined;

    public bool exitPoints;
    public bool powered;

}

public class Transformer : ObjInfo
{

}

public class Wires : ObjInfo
{
    //up right down left
    public int[] distance;
}

public class Storage : ObjInfo
{

}

public class Sell : ObjInfo
{

}