using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Option : MonoBehaviour {

	[Header ("Joystick Position")]
	public Image imgJoystick;
	public Sprite sprJoystickRight, sprJoystickLeft;
	bool isJoystickRight = true;

	[Header ("Volume")]
	public Slider sliderVolume;
	float volume = 1f;

	[Header ("Done Button")]
	public string doneSceneName = "Title";

	AudioSource audioS;

	// Use this for initialization
	void Start () {
		audioS = GetComponent<AudioSource> ();

		// Joystick setting
		if (PlayerPrefs.HasKey ("isJoystickRight") && PlayerPrefs.GetInt ("isJoystickRight") == 1)
			isJoystickRight = true;
		else
			isJoystickRight = false;

		if (isJoystickRight)
			imgJoystick.sprite = sprJoystickRight;
		else
			imgJoystick.sprite = sprJoystickLeft;

		// Volume setting
		if (PlayerPrefs.HasKey ("volume"))
			volume = PlayerPrefs.GetFloat ("volume");

		sliderVolume.value = volume;
	}

	public void OnSwapJoystickPosition () {
		isJoystickRight = !isJoystickRight;

		if (isJoystickRight) {
			imgJoystick.sprite = sprJoystickRight;
		} else {
			imgJoystick.sprite = sprJoystickLeft;
		}
	}

	public void OnVolumeChanged () {
		volume = sliderVolume.value;
	}

	public void OnSound () {
		audioS.volume = volume;
		audioS.Play ();
	}

	public void OnOptionDone () {
		if (isJoystickRight) {
			PlayerPrefs.SetInt ("isJoystickRight", 1);
		} else {
			PlayerPrefs.SetInt ("isJoystickRight", 0);
		}

		PlayerPrefs.SetFloat ("volume", volume);
	}
}
