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
    public int Difficulty;

    public GameObject DifficultyMenu;

    public int maxzomb;

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
            if(lowerzomb < maxzomb)
                lowerzomb += lowerzombInc;

            if (higherzomb < maxzomb)
                higherzomb += higherzombInc;

            if (higherzombInc < maxzomb)
                higherzombInc += Difficulty;


            timerdifficulty = timerdifficultyTimeIncrease;
        }
    }

    public void easy()
    {
        Difficulty = 0;
        maxzomb = 75;

        DifficultyMenu.SetActive(false);
        

    }

    public void Medium()
    {
        Difficulty = 5;
        maxzomb = 100;

        DifficultyMenu.SetActive(false);
        
    }
    

    public void Hard()
    {
        Difficulty = 15;
        maxzomb = 150;

        DifficultyMenu.SetActive(false);
        
    }

    public void Insanity()
    {
        Difficulty = 30;
        maxzomb = 300;

        DifficultyMenu.SetActive(false);
        
    }
}
