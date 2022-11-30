using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class application : MonoBehaviour
{
    public void exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
