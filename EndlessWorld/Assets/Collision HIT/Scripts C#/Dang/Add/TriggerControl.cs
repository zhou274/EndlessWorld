using UnityEngine;
using System.Collections;

public class TriggerControl : MonoBehaviour {

	public GameObject MainObject;
	public bool PlayAnimation = false;
	public Vector3 AddForces;
	public Vector3 AddRotation;

	private bool DontRepeat = false;

	void OnTriggerEnter(Collider other){ 
		if (gameObject.GetComponent<TriggerControl> ().enabled == true) {
			if (other.gameObject.tag == "Camera") {
				if (!DontRepeat) {
					DontRepeat = true;
					if (PlayAnimation) {
						MainObject.GetComponent<Animation> ().Play ();
					} else {
						MainObject.GetComponent<Rigidbody> ().AddForce (AddForces * 1);
						MainObject.GetComponent<Rigidbody> ().angularVelocity = AddRotation * 1;
					}
				}
			}
		}
	}

	void Update(){


	}


}
