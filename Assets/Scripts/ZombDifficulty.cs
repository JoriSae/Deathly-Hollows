using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombDifficulty : MonoBehaviour {

    public static ZombDifficulty instance;

    public int lowerzomb;
    public int lowerzombInc;
    public int higherzomb;
    public int higherzombInc;
    public float timerdifficulty;
    public float timerdifficultyTimeIncrease;

    public void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        difficulty();

    }

    public void difficulty()
    {
        timerdifficulty -= Time.deltaTime;

        if (timerdifficulty < 0)
        {
            lowerzomb += lowerzombInc;
            higherzomb += higherzombInc;
            timerdifficulty = timerdifficultyTimeIncrease;
        }
    }
}
