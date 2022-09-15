using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInfo
{
    public float dataStored;

    public float powerStored;

    public int level;

    public float age;

    public float upkeepCost;

    public float dataMined;

    public float dataSold;

    public float dataTransferd;

    public void Settings(ObjInfo obj)
    {
        this.dataStored = obj.dataStored;
        this.level = obj.level;
        this.age = obj.age;
        this.upkeepCost = obj.upkeepCost;
        this.dataMined = obj.dataMined;
        this.dataSold = obj.dataSold;
        this.dataTransferd = obj.dataTransferd;
    }
}

public class Miner : ObjInfo
{

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
    public int SelfPrio = -1;

    public int updateSpeed;
}

public class Storage : ObjInfo
{

}

public class UploadStation : ObjInfo
{

}