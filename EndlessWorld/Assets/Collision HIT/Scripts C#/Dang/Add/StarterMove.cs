using UnityEngine;
using System.Collections;

public class StarterMove : MonoBehaviour {

	public GameObject StopObject;

	void OnTouchDown(){Doing();}
	void OnMouseDown(){Doing();}
	
	
	void Doing(){
		gameObject.transform.parent = StopObject.transform;
	}
}
