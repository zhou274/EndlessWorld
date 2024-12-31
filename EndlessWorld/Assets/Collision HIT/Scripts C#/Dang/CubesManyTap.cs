using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CubesManyTap : MonoBehaviour {

	public AudioClip SoundPlay;
	public Vector3 AddForces;
	public Vector3 AddRotation;
	public int Attempt;

	void Start(){
		gameObject.GetComponent<AudioSource>().volume = 0.8f;
	}

	void OnMouseDown(){
		if (gameObject.GetComponent<CubesManyTap> ().enabled == true) {
			if (Attempt > 0) {
				Work ();
				Attempt--;
			} else {
				gameObject.GetComponent<CubesManyTap> ().enabled = false;
			}
		}
	}

	void Work(){
		GameObject.Find("Main Camera").GetComponent<CameraMove>().DeleteDist();
		GameObject.Find("Main Camera").GetComponent<CameraMove>().AllDistance -= 1;
		GameObject.Find("Delete Distance").GetComponent<Text>().text = "-1";
		gameObject.GetComponent<AudioSource>().PlayOneShot(SoundPlay);
		gameObject.GetComponent<Rigidbody>().AddForce(AddForces*1);
		gameObject.GetComponent<Rigidbody>().angularVelocity += AddRotation*1;
	}



}
