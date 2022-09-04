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
            if(MoneyText != null)
            MoneyText.text = "" + SaveFile.saveFile.profile.Statistics.money;
            if(DataText != null)
            DataText.text = "" + SaveFile.saveFile.profile.Statistics.data;
        }
        catch
        {
            Debug.Log(SaveFile.saveFile.profile);
        }
    }
}
