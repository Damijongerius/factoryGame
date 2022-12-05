using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotations : MonoBehaviour
{
    private float xMoved;
    private float yMoved;

    private void Update()
    {
        
        xMoved = (xMoved + (Input.mousePosition.x - (Screen.width / 2))) / 1.2f;
        yMoved = (yMoved + (Input.mousePosition.y - (Screen.height / 2))) / 1.2f;
        transform.rotation = Quaternion.Euler(Mathf.Lerp(transform.rotation.y, yMoved, 0.003f),Mathf.Lerp(transform.rotation.y, xMoved, 0.003f),0);
    }
}
