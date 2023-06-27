using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsBar : MonoBehaviour
{
    //pre defined text objects
    // //\\//\\//\\
    public TextMeshProUGUI moneyObject;
    public TextMeshProUGUI timeObject;
    public TextMeshProUGUI moneyPHObject;
    public TextMeshProUGUI dataPHObject;
    public TextMeshProUGUI upkeepPHObject;
    // \\//\\//\\//

    //Static controllable variables
    // //\\//\\//\\
    private static float money;
    private static float moneyPH;
    private static float dataPH;
    private static float upkeepPH;
    private static string time;
    // \\//\\//\\//

    private void Start()
    {
        InvokeRepeating(nameof(DelayedObject), 0.5f, 0.2f);
    }

    private void DelayedObject()
    {
      
        moneyObject.text = "Money: " + money.ToString();
        moneyPHObject.text = moneyPH.ToString() + "$+";
        dataPHObject.text = dataPH.ToString() + "+ data";
        upkeepPHObject.text = upkeepPH.ToString() + "- upkeep";
        timeObject.text = time;
    }

    public static void SetMoney(float money) => StatisticsBar.money = money;
    public static void SetMoneyPerHour(float moneyPerHour) => moneyPH = moneyPerHour;
    public static void SetDataPerHour(float DataPerHour) => dataPH = DataPerHour;
    public static void UpkeepPerHour(float UpkeepPerHour) => upkeepPH = UpkeepPerHour;
    public static void Time(string time) => StatisticsBar.time = time;

}
