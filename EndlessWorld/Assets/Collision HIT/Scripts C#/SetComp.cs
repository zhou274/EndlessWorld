using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class SetComp : MonoBehaviour {
	

	void Start () {
		if (gameObject.GetComponent<Crash> ()) {
			gameObject.AddComponent<Crash>();
		}
	}
}
