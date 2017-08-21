using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    float time = 0.5f;
    public Light directionalLight;
    public Material material;
    public Texture2D tex2D;
    public float secondsInDay;
    public float minIntensity;
    public bool day = false;

    private void Start()
    {

    }

    void Update()
    {

        time = Mathf.PingPong(Time.time + secondsInDay / 2, secondsInDay);
        time = time / secondsInDay;
        if (time > 0.5f)
        {
            day = true;
        }
        else
        {
            day = false;
        }
            
        //Shader.SetGlobalFloat("_Time55", time);
        //material.SetFloat("_Time55", time);
        directionalLight.color = tex2D.GetPixelBilinear(0, time);
        time = Mathf.Clamp(time, minIntensity, 1);
        directionalLight.intensity = time;

    }
}