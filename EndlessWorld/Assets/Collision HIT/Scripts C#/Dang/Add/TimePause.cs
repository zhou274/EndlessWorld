using UnityEngine;
using System.Collections;

public class TimePause : MonoBehaviour {

	public float TimeActive;
	public GameObject ObjectActive;

	void Start() {
		StartCoroutine("Starting");
	}


	IEnumerator Starting () {
		yield  return new WaitForSeconds(TimeActive);
		ObjectActive.SetActive(true);
	}




}
