using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class infoBar : MonoBehaviour
{

    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI DataText;
    private SaveFile sf;

    void Start()
    {
        sf = SaveFile.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
        MoneyText.text = "" + sf.profile.Statistics.Money;
           DataText.text = "" + sf.profile.Statistics.Data;
    }
}
