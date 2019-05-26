using UnityEngine;
using System.Collections;

public class HereArrow : MonoBehaviour {
	
	public float posXMin = -10f, posXMax = 10f;

	public Color farColor, nearColor;
	public float farPos = 10f;

	SpriteRenderer sr;

	Player player;

	// Use this for initialization
	void Awake () {
		sr = GetComponent<SpriteRenderer> ();

		player = FindObjectOfType<Player> ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (player.transform.position.y > transform.position.y && !player.onBecameVisible) {
			if (!sr.enabled)
				sr.enabled = true;

			// Setting position
			Vector3 curPos = Vector3.zero;
			curPos.x = player.transform.position.x;
			if (curPos.x < posXMin)
				curPos.x = posXMin;
			else if (curPos.x > posXMax)
				curPos.x = posXMax;
			curPos.y = transform.position.y;
			transform.position = curPos;

			// Setting color
			Color curColor = Color.Lerp (nearColor, farColor, (player.transform.position.y - transform.position.y) / farPos);
			sr.color = curColor;

		} else {
			if (sr.enabled)
				sr.enabled = false;
		}
	}
}
