using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public Text LevelText;
    public Text ExpNeededText;
    public Text CurrentExpText;
    public Slider Healthbar;
    public Text healthoverlaytext;
    public Text levelUpText;

    bool paused = false;
    public GameObject pauseMenu;
    public GameObject deathMenu;

    // Use this for initialization
    void Start () {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        paused = false;
        deathMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Player.instance.isDead != true)
                Pause();  
        }

        if (Player.instance.isDead == true)
            deathMenu.SetActive(true);

        if (Player.instance.leveledUp)
            levelUpText.gameObject.SetActive(true);
        else
            levelUpText.gameObject.SetActive(false);

        LevelText.text = "Level: " + Player.instance.Level.ToString();

        ExpNeededText.text = "Next Level Exp: " + Player.instance.NextLevelExp.ToString();

        CurrentExpText.text = "Current Exp: " + Player.instance.Exp.ToString();

        Healthbar.value = (Player.instance.Health / Player.instance.MaxHealth) * 100;

        healthoverlaytext.text = Player.instance.Health.ToString("F0") + "/" + Player.instance.MaxHealth.ToString("F0");
    }

    public void Pause()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            paused = true;
            pauseMenu.SetActive(true);
        }

        else if (paused)
        {
            Time.timeScale = 1;
            paused = false;
            pauseMenu.SetActive(false);
        }
    }
}
