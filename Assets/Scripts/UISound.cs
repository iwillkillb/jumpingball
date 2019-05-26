using UnityEngine;
using System.Collections;

public class UISound : MonoBehaviour {

	AudioSource audioS;
	float volume = 1f;

	// Use this for initialization
	void Start () {
		audioS = GetComponent<AudioSource> ();

		// Volume setting
		if (PlayerPrefs.HasKey ("volume"))
			volume = PlayerPrefs.GetFloat ("volume");
	}

	public void OnSound () {
		audioS.volume = volume;
		audioS.Play ();
	}
}
