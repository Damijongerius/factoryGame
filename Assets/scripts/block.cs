using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    public int size = 10;
    // Start is called before the first frame update
    void Start()
    {
                MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
                MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        quad[,] quads = new quad[size, size];
        Mesh[] Meshes = new Mesh[size*10];
        for (int x = 0; x < size; x++)
        {
            for(int z = 0; z < size; z++)
            {
               // quads[x, z] = new quad(new Vector3(0, 0, 0));
                Meshes[x + z] = quads[x, z].mesh;
                //Debug.Log(quads[x, z].mesh);
            }
        }
        meshFilter.mesh = MeshUtils.MergeMeshes(Meshes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
