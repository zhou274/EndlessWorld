using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CubesTap : MonoBehaviour {

	public AudioClip SoundPlay;
	private bool Non = false;
	public Vector3 AddForces;
	public Vector3 AddRotation;
	public float WaitSeconds;
	public Vector3 AddForces2;
	public Vector3 AddRotation2;
	public GameObject Contacts;
	private bool Other = false;

	void Start(){
		gameObject.GetComponent<AudioSource>().volume = 0.6f;
	}

	void OnMouseDown(){
		if (gameObject.GetComponent<CubesTap> ().enabled == true) {
			if (!Non) {
				Non = true;
				Work ();
			}
		}
	}
	void OnTouchDown(){if(!Non){Non=true; Work();}}

	void Work(){
		GameObject.Find("Main Camera").GetComponent<CameraMove>().DeleteDist();
		GameObject.Find("Main Camera").GetComponent<CameraMove>().AllDistance -= 5;
		GameObject.Find("Delete Distance").GetComponent<Text>().text = "-5";
		gameObject.GetComponent<AudioSource>().PlayOneShot(SoundPlay);
		gameObject.GetComponent<Rigidbody>().isKinematic = false;
		if(gameObject.GetComponent<Animation>()){gameObject.GetComponent<Animation>().Stop();}
		if(gameObject.transform.parent.GetComponent<Animation>()){gameObject.transform.parent.GetComponent<Animation>().Stop();}
		gameObject.GetComponent<Rigidbody>().AddForce(AddForces*1);
		gameObject.GetComponent<Rigidbody>().angularVelocity += AddRotation*1;
		StartCoroutine("SecondWork");
		if(Contacts){
			Contacts.GetComponent<CubesTap>().StartCoroutine("SecondWork");
			if(Contacts.GetComponent<Animation>()){Contacts.GetComponent<Animation>().Play();}
		}
	}

	IEnumerator SecondWork(){
		if(!Other){ Other = true;
			yield return new WaitForSeconds(WaitSeconds);
			gameObject.GetComponent<Rigidbody>().AddForce(AddForces2*1);
			gameObject.GetComponent<Rigidbody>().angularVelocity += AddRotation2*1;
		}
	}

}
