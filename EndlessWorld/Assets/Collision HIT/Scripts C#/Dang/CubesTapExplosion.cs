using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CubesTapExplosion : MonoBehaviour {

	public AudioClip SoundPlay;
	private bool Non = false;
	public Vector3 AddForces_1;
	public float Wait;
	public Vector3 AddForces_2;
	public Vector3 AddRotation_2;
	public GameObject ExplosionPartice;


	void OnMouseDown(){
		if (gameObject.GetComponent<CubesTapExplosion> ().enabled == true) {
			if (!Non) {
				Non = true;
				StartCoroutine ("Work");
			}
		}
	}
	void OnTouchDown(){if(!Non){Non=true; StartCoroutine("Work");}}

	IEnumerator Work(){
		GameObject.Find("Main Camera").GetComponent<CameraMove>().DeleteDist();
		GameObject.Find("Main Camera").GetComponent<CameraMove>().AllDistance -= 3;
		GameObject.Find("Delete Distance").GetComponent<Text>().text = "-3";
		gameObject.GetComponent<AudioSource>().PlayOneShot(SoundPlay);
		if(gameObject.GetComponent<Animation>()){gameObject.GetComponent<Animation>().Stop();}
		if(gameObject.transform.parent.GetComponent<Animation>()){gameObject.transform.parent.GetComponent<Animation>().Stop();}
		gameObject.GetComponent<Rigidbody>().AddForce(AddForces_1*1);
		yield return new WaitForSeconds(Wait);
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		ExplosionPartice.SetActive(true);
		gameObject.GetComponent<Rigidbody>().AddForce(AddForces_2*1);
		gameObject.GetComponent<Rigidbody>().angularVelocity = AddRotation_2*1;
	}

	void Update(){

	}



}
