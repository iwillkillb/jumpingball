using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour {

	Text uiText;
	public string[] initStrings;
	public string stringBeforeScore = "Your high score : ";

	public bool isScoreReset = false;

	// Use this for initialization
	void Awake () {
		uiText = GetComponent<Text> ();

		// String init
		uiText.text = initStrings [Random.Range (0, initStrings.Length)];

		// Score reset debug
		if (isScoreReset)
			PlayerPrefs.DeleteKey ("highScore");
		
		if (PlayerPrefs.HasKey ("highScore")) {
			if (PlayerPrefs.GetInt ("highScore") != 0) {
				uiText.text = stringBeforeScore + PlayerPrefs.GetInt ("highScore").ToString ();
			}
		}
	}
}
