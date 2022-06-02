using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    Cell[,] grid;
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


    // Start is called before the first frame update
    void Start()
    {
        size = GetComponent<gridSys>().size;
        grid = GetComponent<gridSys>().grid;
    }

    private void Update()
    {
        Pending(grid);
    }

    public void Pending(Cell[,] grid)
    {
        //snap to grid
        if (pendingObject != null)
        {
            Debug.Log("hoi hoi");
            float posX = RoundToNearestGrid(pos.x);
            float posY = RoundToNearestGrid(pos.y);
            float posZ = RoundToNearestGrid(pos.z);

            int X = Mathf.FloorToInt(posX);
            int Y = Mathf.FloorToInt(posY);

            pendingObject.transform.position = new Vector3(posX, posY, posZ);
            Cell cell = grid[X,Y];
            Debug.Log("hey");
            if (!cell.isWater && cell.obj == null)
            {
                Debug.Log("voldaan");
                if (Input.GetMouseButtonDown(0))
                {
                    cell.obj = pendingObject;
                    pendingObject = null;
                    Debug.Log("click");
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
       pendingObject = Instantiate(objects[index], pos, transform.rotation);
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
