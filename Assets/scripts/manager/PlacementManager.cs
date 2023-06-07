using System;
using System.Collections.Generic;
using UnityEngine;
using WorldObjects;

public class PlacementManager : MonoBehaviour
{

    private static PlacementManager instance;
    
    public GameObject[] placeables;

    public GameObject m_Prefab;

    private RaycastHit hit;
    public LayerMask layerMask;

    public float speed;

    private World.World world = World.World.GetInstance();

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        UpdatePosition();
        ClickActions();
    }

    private void UpdatePosition()
    {
        if(m_Prefab == null)
        {
            return;
        }

        //cast to world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 newpos = Round(hit.point);
        Vector3 oldpos = m_Prefab.transform.position;
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            if (m_Prefab.transform.localScale == new Vector3(1, 1, 1))
            {
                m_Prefab.transform.position = new Vector3(Mathf.Lerp(oldpos.x, newpos.x, speed), 1f, Mathf.Lerp(oldpos.z, newpos.z, speed));
                return;
            }
            Vector3 LocalS = m_Prefab.transform.localScale;

            newpos.x += (LocalS.x - 1) / 2;
            newpos.z += (LocalS.z - 1) / 2;


            m_Prefab.transform.position = new Vector3(Mathf.Lerp(oldpos.x, newpos.x, speed), 1f, Mathf.Lerp(oldpos.z, newpos.z, speed));

        }
    }

    private void ClickActions()
    {
        if(m_Prefab != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                //let ray hit floor instead of anything
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Vector3 newpos = Round(hit.point);
                Vector3 endPos = newpos;
                Vector3 LocalS = m_Prefab.transform.localScale;
                Debug.Log(LocalS + "--" + endPos);
                endPos.x += LocalS.x - 1;
                endPos.z += LocalS.z - 1;


                    if (AreFree(newpos, endPos))
                {
                    Debug.Log("free");
                    newpos.x += (LocalS.x - 1) / 2;
                    newpos.z += (LocalS.z - 1) / 2;

                    List<Vector3> positions = new();
                    for (int x = (int)newpos.x; x <= (int)endPos.x; x++)
                    {
                        for (int z = (int)newpos.z; z <= (int)endPos.z; z++)
                        {
                            positions.Add(new Vector3(x,1, z));
                        }
                    }
                    Debug.Log(positions);

                    bool result = world.OnSet(positions,Order.CoalPlant, m_Prefab);

                    if(result)
                    {
                        m_Prefab.transform.position = new Vector3(newpos.x,0.5f,newpos.z);
                        m_Prefab = null;
                    }
                }
            }
        }
    }

    public void AddPlacable(GameObject gj, Order item)
    {
        if (m_Prefab != null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        m_Prefab = Instantiate(gj,hit.point,Quaternion.Euler(0,0,0),transform);
    }

    private Boolean AreFree(Vector3 start, Vector3 end)
    {
        for(int x = (int)start.x; x  <= (int)end.x; x++)
        {
            for(int z = (int)start.z; z <= (int) end.z; z++)
            {
                foreach(ITile tile in world.tiles){

                    if(tile.ContainsPosition(new Vector3(x, 0, z)))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    
    private void OnDrawGizmos()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.height / 2, Screen.width / 2));
        RaycastHit hitPoint;

        if (Physics.Raycast(ray, out hitPoint, 100.0f)) {
            Vector3 pos = Round(hitPoint.point);
            for (int x = (int)pos.x - 14; x <= pos.x + 14; x++)
            {
                for (int z = (int)pos.z - 14; z <= pos.z + 7; z++)
                {
                    Gizmos.color = UnityEngine.Color.white;
                    Gizmos.DrawLine(new Vector3(x - 0.5f, 0.55f, z - 0.5f), new Vector3(x - 0.5f, 0.55f, z + 0.5f));
                    Gizmos.DrawLine(new Vector3(x - 0.5f, 0.55f, z - 0.5f), new Vector3(x + 0.5f, 0.55f, z - 0.5f));
                    Gizmos.DrawLine(new Vector3(x + 0.5f, 0.55f, z - 0.5f), new Vector3(x + 0.5f, 0.55f, z + 0.5f));
                    Gizmos.DrawLine(new Vector3(x - 0.5f, 0.55f, z + 0.5f), new Vector3(x + 0.5f, 0.55f, z + 0.5f));
                }
            }
        }
    }

    private Vector3 Round(Vector3 _hitpoint)
    {
        _hitpoint.x = Mathf.Floor(_hitpoint.x + 0.5f);
        _hitpoint.y = Mathf.Floor(_hitpoint.y + 0.5f);
        _hitpoint.z = Mathf.Floor(_hitpoint.z + 0.5f);
        return _hitpoint;
    }


    public static PlacementManager GetInstance()
    {
        return instance;
    }
}
