using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uploadStation : MonoBehaviour
{
    public int score = 0;
    public GameObject Score;
    public TextMeshProUGUI number;
    void Start()
    {
        InvokeRepeating(nameof(TestSurround), 2f, 1.5f);
        Score = GameObject.Find("Score");
        number = Score.GetComponent<TextMeshProUGUI>();
       
    }


    void Update()
    {
        Debug.Log(score);
    }

    public void TestSurround()
    {
        bool[] directions = gridSys.grid[(int)transform.position.x, (int)transform.position.z].CheckNeighbour((int)transform.position.x, (int)transform.position.z, "DataWire");
        GameObject[] Wires = gridSys.grid[(int)transform.position.x, (int)transform.position.z].CheckPowered((int)transform.position.x, (int)transform.position.z, "DataWire");
        foreach (bool direction in directions)
        {
            if (direction)
            {
                score += 10;
                number.text = "Money: " + score;
            }
        }
    }
}
