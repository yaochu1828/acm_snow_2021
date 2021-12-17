using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StareDurations: MonoBehaviour
{
    public static List<float> stareSeconds = new List<float>();

    private void Start()
    {
        stareSeconds.Add(7f);
        stareSeconds.Add(9f);
        stareSeconds.Add(10f);
        stareSeconds.Add(7f);
        stareSeconds.Add(8f);
        stareSeconds.Add(7f);
        stareSeconds.Add(7f);
    }
}
