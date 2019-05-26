using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
	
	public string goalSceneName;

	public AudioSource audioS;
	float volume = 1f;



	public void OnSound () {
		audioS.volume = volume;
		audioS.Play ();
	}

	public void ChangeScene () {
		// Volume setting
		if (PlayerPrefs.HasKey ("volume"))
			volume = PlayerPrefs.GetFloat ("volume");
		audioS.volume = volume;
		audioS.Play ();

		Invoke ("ChangeSceneInvoke", 0.3f);
	}

	void ChangeSceneInvoke () {
		SceneManager.LoadScene(goalSceneName);
	}

	public void GameQuit () {
		Application.Quit ();
	}
}
