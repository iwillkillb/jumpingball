using UnityEngine;
using System.Collections;

public class JumpIcon : MonoBehaviour {

	public enum Action
	{
		Up, Down, Stop, Float
	}
	public Action action;

	public float speed = 5f;
	public float pushPower = 5f;
	public int scorePlus = 1;

	bool isActivated = false;

	SpriteRenderer sr;
	AudioSource audioS;



	// Use this for initialization
	void Awake () {
		sr = GetComponent<SpriteRenderer> ();
		audioS = GetComponent<AudioSource> ();

		// Sound volume setting
		if (PlayerPrefs.HasKey ("volume"))
			audioS.volume = PlayerPrefs.GetFloat ("volume");
	}

	// Initialization
	void OnEnable () {
		isActivated = false;
		sr.color = Color.white;
		transform.localScale = Vector3.one;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (Vector3.left * speed * Time.deltaTime);

		// Out of game zone
		if (transform.position.x < -IconManager.iconManager.limitDistance)
			gameObject.SetActive (false);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			if (isActivated)
				return;
			else
				isActivated = true;

			Rigidbody2D colRb = col.GetComponent<Rigidbody2D> ();

			switch (action) {
			case Action.Up:
				colRb.velocity = new Vector2 (colRb.velocity.x, pushPower);
				break;

			case Action.Down:
				colRb.velocity = new Vector2 (colRb.velocity.x, -pushPower);
				break;

			case Action.Stop:
				colRb.velocity = Vector2.zero;
				break;

			case Action.Float:
				col.GetComponent<Player> ().FlyOn (1f);
				break;
			}
			GameManager.gameManager.PlusScore (scorePlus);
			InvokeRepeating ("Fade", 0f, 0.03f);
			audioS.Play ();
		}
	}

	void Fade () {
		Color color = sr.color;
		color.a -= 0.1f;
		sr.color = color;

		if (color.a <= 0f) {
			CancelInvoke ();
			Invoke ("OffAfter", 2f);
		}

		transform.localScale *= 1.1f;
	}

	void OffAfter () {
		gameObject.SetActive (false);
	}
}
