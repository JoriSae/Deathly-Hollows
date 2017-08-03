using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    float time;

    public Material material;

    private void Start()
    {

    }

    void Update()
    {

        time = Mathf.PingPong(Time.time, 5);
        time = time / 5;
        print(time);
        Shader.SetGlobalFloat("_Time2", time);
        //material.SetFloat("_Time2", time);
    }
}
