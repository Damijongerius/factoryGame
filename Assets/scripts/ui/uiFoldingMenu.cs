using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiFoldingMenu : MonoBehaviour
{
    public Animator openClose;
    public GameObject foldObject;
    public bool open;

    // Start is called before the first frame update
    void Start()
    {
        openClose = foldObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Q))
        {
            OpenClose();
        }
    }

    public void OpenClose()
    {

        if (open)
        {
            open = false;
            openClose.SetTrigger("close");
        }
        else
        {
            open = true;
            openClose.SetTrigger("open");
        }
    }
}
