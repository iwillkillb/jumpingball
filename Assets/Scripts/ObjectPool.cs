using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	protected List<GameObject> objList = new List<GameObject> ();

	public GameObject obj;
	public Transform parentTrn;
	public int amount = 1;

	// Make
	void Start () {
		for (int a = 0; a < amount; a++) {
			GameObject makingObj = Instantiate (obj, transform.position, transform.rotation, parentTrn) as GameObject;
			makingObj.SetActive (false);
			objList.Add (makingObj);
		}
	}

	public void CallObj (Transform trn) {
		// FILO
		for (int a = 0; a < objList.Count; a++) {
			if (!objList [a].activeInHierarchy) {
				// Transform reset
				objList [a].transform.position = trn.position;
				objList [a].transform.rotation = trn.rotation;
				objList [a].transform.localScale = trn.localScale;

				// On
				objList [a].SetActive (true);
				break;
			}
		}
	}
}
