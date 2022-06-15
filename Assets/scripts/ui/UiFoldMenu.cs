using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class UiFoldMenu : MonoBehaviour
{

    Animator openClose;
    public GameObject foldObject;
    public Canvas settings;
    public bool Foldopen = false;
    public bool SettingOpen = false;



    // Start is called before the first frame update
    void Start()
    {
        openClose = foldObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Settings();
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            OpenClose();
        }
    }

    


    public void OpenClose()
    {
     
        if (Foldopen)
        {
            Foldopen = false;
            openClose.SetTrigger("close");
        }
        else
        {
            Foldopen = true;
            openClose.SetTrigger("open");
        }
    }

    public void Settings()
    {
        if (SettingOpen)
        {
            SettingOpen = false;
            settings.gameObject.SetActive(false);
        }
        else
        {
            settings.gameObject.SetActive(true);
            SettingOpen = true;
        }
    }
}
