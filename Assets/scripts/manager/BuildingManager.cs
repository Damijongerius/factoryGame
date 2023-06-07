using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public int size;
    //placables
    public GameObject[] objects;

    //display state
    private GameObject pendingObject;

    //mouse snap pos
    public Vector3 pos;

    //raycast layer/hit
    private RaycastHit hit;
    public LayerMask layerMask;

    //grid size
    public float gridSize;
    public bool isWire;

    private int endix;

    private SaveFile sf = SaveFile.GetInstance();
    private World.World world = World.World.GetInstance();

    private static BuildingManager bm;

    private GameObject itemInHand;


    public void Update()
    {
        float posX = RoundToNearestGrid(pos.x);
        float posZ = RoundToNearestGrid(pos.z);
        if (pendingObject != null)
        {
            pendingObject.transform.position = new Vector3(posX, 1, posZ);

            SaveFile.GetInstance().profile.Statistics.Money += 100;
            if (Input.GetMouseButtonDown(0))
                Pending(posX, posZ);

        }

        if (Input.GetMouseButtonDown(1))
        {
            stopPending();

        }
        if (Input.GetMouseButtonDown(2))
        {
            world.OnDelete((int)posX, (int)posZ);
            stopPending();
        }

        //cast to world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    private void Start()
    {
        bm = this;
    }

    public void HoldItem(GameObject item)
    {
        if(itemInHand != null && item == null)
        {
            return;
        }

        itemInHand = item;


    }

    public void PlaceItem()
    {

    }

    public void Pending(float _posX, float _posZ)
    {
        Debug.Log("place");
        //snap to grid
        int X = (int)_posX;
        int Z = (int)_posZ;

    }

    private void stopPending()
    {
        Destroy(pendingObject);
        pendingObject = null;

    }


    public void SelectObject(int index)
    {
        //choosing from array to object

        if (pendingObject == null)
        {
            sf.profile.Statistics.Money += 100;
            if (index == 1 && sf.profile.Statistics.Money >= 10)
            {
                isWire = true;
                inst(index);
            }
            else if ((index == 0 && sf.profile.Statistics.Money >= 100))
            {
                inst(index);
            }
            else if ((index == 2 && sf.profile.Statistics.Money >= 50))
            {
                inst(index);
            }
        }
        else return;
    }

    void inst(int index)
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation);
        endix = index;
    }

    int RoundToNearestGrid(float pos)
    {
        //rounding numbers
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return (int)pos;
    }

    Vector2 GetCords()
    {
        int x = RoundToNearestGrid(itemInHand.transform.position.x);

        return new Vector2();
    }

    Boolean IsPossible()
    {
        return false;
    }

    public static BuildingManager GetInstance()
    {
        return bm;
    }
}
