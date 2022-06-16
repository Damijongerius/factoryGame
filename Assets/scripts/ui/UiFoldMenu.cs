using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class UiFoldMenu : MonoBehaviour
{

 //   Animator openClose;
    //public GameObject foldObject;
    public bool open = false;
    public Button button;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
       // openClose = foldObject.GetComponent<Animator>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
        
    }
        
    public void HandleClick()
    {
        Debug.Log("IUADSILHBGASDHVJIOHOFA");
        open = !open;
        canvas.gameObject.SetActive(open);

    }

    // Update is called once per frame
    void Update()
    {
        
        //if (Input.GetKeyUp(KeyCode.Escape))
        //{
        //    Settings();
        //}
        //else if (Input.GetKeyUp(KeyCode.Q))
        //{
        //    OpenClose();
        //}
    }

    
    


    //public void OpenClose()
    //{
     
    //    if (open[0])
    //    {
    //        open[0] = false;
    //        openClose.SetTrigger("close");
    //    }
    //    else
    //    {
    //        open[0] = true;
    //        openClose.SetTrigger("open");
    //    }
    //}


    //public void LeaveShop(Canvas canvas123)
    //{
    //    if (open[2])
    //        open[2] = false;
    //}

}
