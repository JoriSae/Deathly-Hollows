using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public Text LevelText;
    public Text ExpNeededText;
    public Text CurrentExpText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        LevelText.text = "Level: " + Player.instance.Level.ToString();

        ExpNeededText.text = "Next Level Exp: " + Player.instance.NextLevelExp.ToString();

        CurrentExpText.text = "Current Exp: " + Player.instance.Exp.ToString();
    }
}
