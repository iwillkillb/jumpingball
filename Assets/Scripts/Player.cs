using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Status
	public float speed = 5f;

	// Input
	float axisHor, axisVer;

	// Fly mode
	bool flyMode = false;
	public Color flyColor;

	// Components
	SpriteRenderer sr;
	Rigidbody2D rb;
	float gravityBackup;

	// Joystick
	public bool useJoystick = true;
	public VirtualJoystick joystickCharacter;

	// Is on screen?
	[HideInInspector]
	public bool onBecameVisible = true;



	// Use this for initialization
	void Awake () {
		sr = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody2D> ();
		gravityBackup = rb.gravityScale;

		// FlyOn (5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Input
		if (useJoystick)
			InputByVirtualJoystick ();
		else
			InputByKeyboard ();

		// Limit X Move
		if ((transform.position.x > GameManager.gameManager.limitWidth && axisHor > 0f) ||
			(transform.position.x < -GameManager.gameManager.limitWidth && axisHor < 0f))
			axisHor = 0f;

		// Move
		if (flyMode)
			transform.Translate ((Vector3.right * axisHor + Vector3.up * axisVer) * speed * Time.deltaTime);
		else
			transform.Translate (Vector3.right * axisHor * speed * Time.deltaTime);
		
		/*
		if (flyMode)
			rb.velocity = new Vector2 (axisHor, axisVer) * speed;
		else
			rb.velocity = new Vector2 (speed * axisHor, rb.velocity.y);
		*/
	}



	void OnBecameVisible() {
		if (!onBecameVisible)
			onBecameVisible = true;
	}

	void OnBecameInvisible () {
		if (onBecameVisible)
			onBecameVisible = false;
	}



	// Input

	void InputByKeyboard () {
		// Character move input
		axisHor = Input.GetAxis ("Horizontal");
		if (flyMode)
			axisVer = Input.GetAxis ("Vertical");
	}

	void InputByVirtualJoystick () {
		// Character move input
		axisHor = joystickCharacter.GetInputAxis ().x;
		if (flyMode)
			axisVer = joystickCharacter.GetInputAxis ().y;
	}



	// Fly mode

	public void FlyOn (float flyTime) {
		if (flyMode)
			return;
		else
			flyMode = true;

		// Zero speed and Zero gravity
		rb.velocity = Vector2.zero;
		rb.gravityScale = 0f;

		// Color change
		sr.color = flyColor;

		Invoke ("FlyOff", flyTime);
	}

	void FlyOff () {
		flyMode = false;

		// Gravity reset
		rb.gravityScale = gravityBackup;

		// Color reset
		sr.color = Color.white;

	}
}
