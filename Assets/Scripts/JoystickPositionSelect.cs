using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoystickPositionSelect : MonoBehaviour {

	Image uiImage;

	bool isJoystickRight = true;

	public Sprite sprJoystickRight, sprJoystickLeft;

	// Use this for initialization
	void Start () {
		uiImage = GetComponent<Image> ();

		if (PlayerPrefs.HasKey ("isJoystickRight") && PlayerPrefs.GetInt ("isJoystickRight") == 1)
			isJoystickRight = true;
		else
			isJoystickRight = false;

		if (isJoystickRight)
			uiImage.sprite = sprJoystickRight;
		else
			uiImage.sprite = sprJoystickLeft;
	}

	public void OnSwapJoystickPosition () {
		isJoystickRight = !isJoystickRight;

		if (isJoystickRight) {
			uiImage.sprite = sprJoystickRight;
			PlayerPrefs.SetInt ("isJoystickRight", 1);
		} else {
			uiImage.sprite = sprJoystickLeft;
			PlayerPrefs.SetInt ("isJoystickRight", 0);
		}
	}


}
