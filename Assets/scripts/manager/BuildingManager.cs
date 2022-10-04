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

    private void Update()
    {
        Pending();
    }

    public void Pending()
    {
        //snap to grid
        if (pendingObject != null)
        {
            float posX = RoundToNearestGrid(pos.x);
            float posY = RoundToNearestGrid(pos.y);
            float posZ = RoundToNearestGrid(pos.z);

            int X = Mathf.FloorToInt(posX);
            int Z = Mathf.FloorToInt(posZ);

            pendingObject.transform.position = new Vector3(posX, posY, posZ);
            Cell cell = gridSys.grid[X, Z];
            //Debug.Log(X + "||" + Z + "||" + cell.obj);
            if (!cell.isWater && cell.obj == null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (endix == 0){
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
                    if (isWire)
                    {
                        pendingObject.tag = "dataWire";
                        isWire = false;
                    }
                    pendingObject = null;
                }
                else if (Input.GetMouseButtonDown(1)) {
                    Destroy(pendingObject);
                    pendingObject = null;
                }
            }

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //cast to world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectObject(int index)
    {
        //choosing from array to object

        if (pendingObject == null)
        {
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
