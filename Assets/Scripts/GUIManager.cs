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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        LevelText.text = "Level: " + Player.instance.Level.ToString();

        ExpNeededText.text = "Next Level Exp: " + Player.instance.NextLevelExp.ToString();

        CurrentExpText.text = "Current Exp: " + Player.instance.Exp.ToString();

        Healthbar.value = (Player.instance.Health / Player.instance.MaxHealth) * 100;

        healthoverlaytext.text = Player.instance.Health.ToString("F0") + "/" + Player.instance.MaxHealth.ToString("F0");
    }
}
