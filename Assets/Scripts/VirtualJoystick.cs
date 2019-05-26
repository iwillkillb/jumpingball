using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
	
	RectTransform rectBg, rectStick;
	float radius;
	Vector3 resultInput, output;

	public float stickLimit = 0.5f;
	//public float inputWeight = 1f;



	void Awake () {
		// Components connecting...
		rectBg = GetComponent<RectTransform> ();
		rectStick = transform.GetChild (0).GetComponent<RectTransform> ();

		radius = rectBg.rect.width * 0.5f * stickLimit;

		// Position swap
		RectTransform myRectTransform = GetComponent<RectTransform> ();
		Vector3 pos = myRectTransform.anchoredPosition;
		if (PlayerPrefs.GetInt ("isJoystickRight") != 1) {
			pos.x *= -1;
			myRectTransform.anchoredPosition = pos;
		}
	}

	void FixedUpdate () {
		// Synchronize device speed
		output = resultInput;
	}

	public virtual void OnDrag (PointerEventData ped) {
		Vector2 value = ped.position - (Vector2)rectBg.position;
		value = Vector2.ClampMagnitude (value, radius);

		rectStick.localPosition = value;

		value = value.normalized;
		float distanceStick = Vector2.Distance (rectBg.position, rectStick.position) / radius;

		resultInput = new Vector3 (value.x, 0, value.y) * distanceStick;
		resultInput = Vector3.ClampMagnitude (resultInput, 1f);
		/*
		Vector2 pos;
		
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (
			imgBg.rectTransform,
			ped.position,
			ped.pressEventCamera,
			out pos)) {
			pos.x = (pos.x / imgBg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / imgBg.rectTransform.sizeDelta.y);

			float inputX = (imgBg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
			float inputY = (imgBg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

			resultInput = new Vector3 (inputX, 0, inputY);
			resultInput = (resultInput.magnitude > 1f) ? resultInput.normalized : resultInput;

			// Move stick
			imgStick.rectTransform.anchoredPosition =
				new Vector2 (
					resultInput.x * (imgBg.rectTransform.sizeDelta.x / 3),
					resultInput.z * (imgBg.rectTransform.sizeDelta.y / 3)
				);
		}*/
	}

	public virtual void OnPointerDown (PointerEventData ped) {
		OnDrag (ped);
	}

	public virtual void OnPointerUp (PointerEventData ped) {
		rectStick.localPosition = Vector3.zero;
		resultInput = Vector3.zero;
		// rectStick.anchoredPosition = Vector3.zero;
	}

	public Vector2 GetInputAxis () {
		return new Vector2 (output.x, output.z);
	}
}