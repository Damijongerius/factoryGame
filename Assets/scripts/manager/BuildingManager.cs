using System;
using System.Collections;
using System.Collections.Generic;
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
    private Vector3 pos;

    //raycast layer/hit
    private RaycastHit hit;
    public LayerMask layerMask;

    //grid size
    public float gridSize;
    public bool isWire;

    private int endix;

    private SaveFile sf = SaveFile.GetInstance();


    // Start is called before the first frame update
    void Start()
    {
    }

    public void Update()
    {
        float posX = RoundToNearestGrid(pos.x);
        float posZ = RoundToNearestGrid(pos.z);
        if (pendingObject != null)
        {
            pendingObject.transform.position = new Vector3(posX, 1, posZ);

            if (Input.GetMouseButtonDown(0))
                Pending(posX, posZ);

        }

        if (Input.GetMouseButtonDown(1))
        {
            stopPending();
        }

        //cast to world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void DeleteBuilding()
    {
        if (Input.GetMouseButtonDown(2))
        {
           // Destroy();
        }
    }

    public void Pending(float _posX, float _posZ)
    {
        //snap to grid
            int X = (int)_posX;
            int Z = (int)_posZ;

            World world = World.GetInstance();
            
            if(!world.Grid[X, 0, Z])
            {
                Chunk chunk = world.GetChunkWithPos(X, Z);


                Cell cell = chunk.GetCell(X, Z);
            if (cell.obj == null)
            {

                if (endix == 0)
                {
                    sf.profile.Statistics.Money -= 100;
                }
                if (endix == 1)
                {
                    sf.profile.Statistics.Money -= 10;
                }
                if (endix == 2)
                {
                    sf.profile.Statistics.Money -= 50;
                }
                cell.obj = pendingObject;
                pendingObject = null;

            }
        }
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


    float RoundToNearestGrid(float pos)
    {
        //rounding numbers
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if(xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }
}
