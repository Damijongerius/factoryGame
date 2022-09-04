using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public class Ranges
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
    public float zoomSpeed;

    public float minZoomDist;
    public float maxZoomDist;

    public Terrain floor;
    public LayerMask Mask;

    public Vector3 point;
    public bool Using;
    public float mouseSensitivity = 500f;

    public float xRotation;
    public float yRotation;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        

        Move();
        Zoom();
        OverObj();
        Rotation();

    }


    public void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.forward * zInput + transform.right * xInput;
        dir = new Vector3(dir.x,0,dir.z);

        transform.position += moveSpeed * Time.deltaTime * dir;

    }
    public void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float dist = Vector3.Distance(transform.position, mainCam.transform.position);

        if (dist < minZoomDist && scrollInput > 0.0f)
            return;
        else if (dist > maxZoomDist && scrollInput < 0.0f)
            return;

        mainCam.transform.position += scrollInput * zoomSpeed * mainCam.transform.forward;
    }

    public void OverObj()
    {
        Ray ray = new(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 20f, Mask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            transform.position += new Vector3(0, 0.1f, 0);
            //transform.position += new Vector3(0, Mathf.Lerp(transform.position.y, transform.position.y + 1, 0.5f), 0);
        }
        else if (!Physics.Raycast(ray, out _, 22f, Mask, QueryTriggerInteraction.Ignore) && Physics.Raycast(ray, out hit, 150f, Mask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            transform.position -= new Vector3(0, 0.1f, 0);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 22f, Color.green);
        }

    }

    public void Rotation()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(2) && Physics.Raycast(ray, out RaycastHit hit))
        {
            if (!Using)
             {
               point = hit.point;
               Using = true;
             }
            float MouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
            float MouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

            xRotation -= MouseY;
            xRotation = Mathf.Clamp(xRotation, -40f, 0f);
            yRotation -= MouseX;

            this.transform.localRotation = Quaternion.Euler(xRotation, -yRotation, 0f);
        }
        else
        {
            Using = false;
        }
    }

    public void FocustOnPosition (Vector3 pos)
    {
        transform.position = pos;
    }
}
