using UnityEngine;
using System.Collections;

public class TurnCamera : MonoBehaviour {

	public int GeneralAngle;
	public float SpeedOfTurn;
	public AudioClip AudioPlay;
	private bool DontRepeat = false;

	void OnTriggerEnter(Collider other){ 
		if(other.gameObject.tag=="Camera"){
			if(!DontRepeat){ DontRepeat = true;
				other.gameObject.GetComponent<CameraMove>().RotateSpeed = SpeedOfTurn;
				other.gameObject.GetComponent<CameraMove>().RotateAngle = GeneralAngle;
				other.gameObject.GetComponent<CameraMove>().StartCoroutine("Rotate");
				gameObject.GetComponent<AudioSource>().PlayOneShot(AudioPlay);
				gameObject.GetComponent<BoxCollider>().enabled = false;
				gameObject.transform.parent = GameObject.Find("StaticParent").transform;
			}
		}
	}

	void Update(){
		
	}

}
