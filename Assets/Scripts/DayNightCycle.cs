using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    float time;
    public Light light;
    public Material material;
    public Texture2D tex2D;
    public float secondsInDay;

    private void Start()
    {

    }

    void Update()
    {

        time = Mathf.PingPong(Time.time, secondsInDay);
        time = time / secondsInDay;
        print(time);
        Shader.SetGlobalFloat("_Time55", time);
        material.SetFloat("_Time55", time);
        light.color = tex2D.GetPixelBilinear(0, time);

    }
}