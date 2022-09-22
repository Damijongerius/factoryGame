using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class onClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //add event listener
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClick);

    }

    //run after button has been clicked
    void OnClick() 
    {
        ProfileManager.getObject().Load(gameObject);
    }
}
