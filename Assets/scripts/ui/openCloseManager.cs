using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class openCloseManager : MonoBehaviour
{


    public bool open = false;
    public Button button;
    public Canvas canvas;
    public Canvas CloseCanvas;

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    [Obsolete]
    public void HandleClick()
    {
        bool state = canvas.gameObject.active;
        state = !state;
        canvas.gameObject.SetActive(state);
        if(CloseCanvas != null)
        {
            CloseCanvas.gameObject.SetActive(!state);
        }
    }

    // Update is called once per frame


    
    


   

}
