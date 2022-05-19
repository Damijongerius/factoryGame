using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public class ranges
    {
        internal static float maxRotation = 30f;
        internal static float minRotation = 90f;
        internal static float maxDistance = 50f;
        internal static float minDistance = 00f;
        internal static float minFov = 30;
        internal static float maxFov = 90;
    }

    public Camera mainCam;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        Move(); 
    }


    public void Move()
    {
        float xInput = Input.GetAxis("horizontal");
        float zInput = Input.GetAxis("vertical");

        Vector3 dir = transform.forward * zInput + transform.right * xInput;

        transform.position += dir  * moveSpeed * Time.deltaTime;

    }
}
