using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigSettings : MonoBehaviour
{
    public Scrollbar move;
    public Scrollbar mouse;
    public cameraController cc;

    public void Start()
    {
        move.onValueChanged.AddListener(Send);
        mouse.onValueChanged.AddListener(Send);
    }
    
    public void Send(float pos)
    {
        cc.Changed(mouse.value,move.value);
    }
}
