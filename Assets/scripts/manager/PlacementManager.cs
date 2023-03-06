using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlacementManager : MonoBehaviour
{
    
    public GameObject[] placeables;

    public GameObject m_Prefab;

    private RaycastHit hit;
    public LayerMask layerMask;

    public float speed;

    void Update()
    {
        if (m_Prefab != null)
        {
            //cast to world
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 newpos = Round(hit.point);
            Vector3 oldpos = m_Prefab.transform.position;
            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                Debug.Log(newpos + "-" + oldpos + "(" + Mathf.Lerp(oldpos.x, newpos.x, 3000f) +  "," + 0.5f  +  "," + Mathf.Lerp(oldpos.z, newpos.z, 0.1f));
                m_Prefab.transform.position = new Vector3(Mathf.Lerp(oldpos.x, newpos.x, speed), 0.5f, Mathf.Lerp(oldpos.z, newpos.z, speed));
            }
        }
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
        _hitpoint.x = Mathf.Round(_hitpoint.x);
        _hitpoint.y = Mathf.Round(_hitpoint.y);
        _hitpoint.z = Mathf.Round(_hitpoint.z);
        return _hitpoint;
    }
}
