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
    public Rigidbody rb;

    public float moveSpeed;
    public float zoomSpeed;

    public float minZoomDist;
    public float maxZoomDist;

    public Terrain floor;
    public LayerMask Mask;

    public Vector3 Point;
    public bool Using;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
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

        //transform.position += dir  * moveSpeed * Time.deltaTime;
        rb.AddForce(10f * dir * moveSpeed * Time.deltaTime);

    }
    public void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float dist = Vector3.Distance(transform.position, mainCam.transform.position);

        if (dist < minZoomDist && scrollInput > 0.0f)
            return;
        else if (dist > maxZoomDist && scrollInput < 0.0f)
            return;

        mainCam.transform.position += mainCam.transform.forward * scrollInput * zoomSpeed;
    }

    public void OverObj()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 20f, Mask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            transform.position += new Vector3(0,0.1f, 0);
            //transform.position += new Vector3(0, Mathf.Lerp(transform.position.y, transform.position.y + 1, 0.5f), 0);
        }
        else if (!Physics.Raycast(ray, out hit, 22f, Mask, QueryTriggerInteraction.Ignore))
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
        RaycastHit hit;
        if (Input.GetMouseButton(2) && Physics.Raycast(ray,out hit))
        {
            if (!Using)
            {
                Point = hit.point;
            }
            Vector3 PointDir = Point - Input.mousePosition;
            float angle = Vector3.Angle(PointDir, transform.forward);
            Debug.Log(angle);
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
