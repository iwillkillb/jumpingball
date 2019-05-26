using UnityEngine;
using System.Collections;

public class FrameAnimation : MonoBehaviour {

	public float period = 1f;
	public Sprite[] sprs;

	int sprIndex = 0;
	SpriteRenderer sr;

	// Use this for initialization
	void Awake () {
		sr = GetComponent<SpriteRenderer> ();
		StartCoroutine (ChangeSprite ());
	}

	IEnumerator ChangeSprite () {
		while (true) {
			sr.sprite = sprs [sprIndex];

			if (sprIndex < sprs.Length - 1)
				sprIndex++;
			else
				sprIndex = 0;

			yield return new WaitForSeconds (period);
		}
	}
}
