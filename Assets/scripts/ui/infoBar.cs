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
        MoneyText.text = "" + GameManager.Money;
        DataText.text = "" + GameManager.Data;
    }
}
