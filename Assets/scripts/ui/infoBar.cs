using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class infoBar : MonoBehaviour
{

    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI DataText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            MoneyText.text = "" + SaveFile.saveFile.profile.Statistics.money;
            DataText.text = "" + SaveFile.saveFile.profile.Statistics.data;
        }
        catch
        {
            Debug.Log("SaveFile doessnt exist yet?");
        }
    }
}
