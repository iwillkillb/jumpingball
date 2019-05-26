using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager gameManager;

	Player player;
	public Text scoreText;
	public Text highScoreText;
	public GameObject gameOverObj;

	public float limitWidth = 10f;
	public float limitHeight = -10f;
	[HideInInspector]
	int score;
	int highScore;



	// Use this for initialization
	void Awake () {
		if (gameManager != null) {
			DestroyImmediate (gameObject);
			return;
		}
		gameManager = this;

		player = FindObjectOfType<Player> ();

		if (PlayerPrefs.HasKey ("highScore"))
			highScore = PlayerPrefs.GetInt ("highScore");
		highScoreText.text = highScore.ToString ();

		gameOverObj.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		scoreText.text = score.ToString ();

		// Die
		if (player.transform.position.y < limitHeight) {
			// High score
			if (!PlayerPrefs.HasKey ("highScore") || PlayerPrefs.GetInt ("highScore") < highScore) {
				PlayerPrefs.SetInt ("highScore", highScore);
			}

			// Game over panel on
			gameOverObj.SetActive (true);
		}
	}

	public void PlusScore (int plus) {
		score += plus;

		if (highScore < score) {
			highScore = score;
		}
	}
}
