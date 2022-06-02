using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextTo : MonoBehaviour
{
    public void LookFor(GameObject selected, LayerMask mask)
    {
        if (selected != null)
        {
            Ray ray0 = new(transform.position, Vector3.left);
            Ray ray1 = new(transform.position, Vector3.right);
            Ray ray2 = new(transform.position, Vector3.forward);
            Ray ray3 = new(transform.position, Vector3.back);

            for (int i = 0; i < 4; i++) {
                if (Physics.Raycast(ray0, out RaycastHit hit, 1f, mask))
                {
                    Debug.Log("hi");
                }
            }
        }
        else return;
    }
}
