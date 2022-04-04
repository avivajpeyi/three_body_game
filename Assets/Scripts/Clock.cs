using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TMP_Text clock_txt;
    private int min = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateClock", 0, 60f);
    }


    // Update is called once per frame
    void UpdateClock()
    {
        clock_txt.text = String.Format("Total Elapsed time: {0:00}min", min);
        min += 1;
    }
}