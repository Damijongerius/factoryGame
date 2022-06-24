using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInfo
{
    public int dataStored;

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
    public int[] Prio = {-1,-1,-1,-1};
    //his highest prio number
    public int SelfPrio;

    public int updateSpeed;
}

public class Storage : ObjInfo
{

}

public class UploadStation : ObjInfo
{

}