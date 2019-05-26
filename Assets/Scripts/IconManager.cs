using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Icon {
	public GameObject objIcon;
	public int spawnAmount;
}

public class IconManager : MonoBehaviour {

	public static IconManager iconManager;

	protected List<GameObject> objList = new List<GameObject> ();

	public Icon[] icons;

	public float range = 10f;
	public float limitDistance = 20f;
	public float delay = 0.5f;



	// Use this for initialization
	void Start () {
		if (iconManager != null) {
			DestroyImmediate (gameObject);
			return;
		}
		iconManager = this;

		for (int a = 0; a < icons.Length; a++) {
			GameObject makingObj = icons [a].objIcon;
			for (int b = 0; b < icons[a].spawnAmount; b++) {
				GameObject inListObj = Instantiate (makingObj, transform.position, transform.rotation, transform) as GameObject;
				inListObj.SetActive (false);
				objList.Add (inListObj);
			}
		}

		StartCoroutine (SpawnIcon ());
	}

	IEnumerator SpawnIcon () {
		while (true) {
			CallIcon ();

			yield return new WaitForSeconds (delay);
		}
	}

	public void CallIcon () {
		// Index select
		int index = Random.Range (0, objList.Count);

		if (!objList [index].activeInHierarchy) {
			// Transform reset
			objList [index].transform.position = transform.position + Vector3.up * range * Random.Range (-1f, 1f);
			objList [index].transform.rotation = Quaternion.identity;
			objList [index].transform.localScale = Vector3.one;

			// On
			objList [index].SetActive (true);
		} else {
			// Index Reselect
			index = Random.Range (0, objList.Count);
		}
	}
}
