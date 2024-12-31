using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CubesRope : MonoBehaviour {

	public GameObject Rope;
	private LineRenderer LineRope;

	public GameObject[] ArrayPoint;
	public Vector3 AddForce;
	public AudioClip SoundOfTap;
	public bool OneForce = false;

	void Start(){
		LineRope = Rope.GetComponent<LineRenderer>();
		Rope.SetActive(true);
	}

	void OnMouseDown(){
		if (gameObject.GetComponent<CubesRope> ().enabled == true) {
			Work ();
		}
	}
	void OnTouchDown(){Work();}

	void Work(){
		GameObject.Find("Main Camera").GetComponent<CameraMove>().DeleteDist();
		GameObject.Find("Main Camera").GetComponent<CameraMove>().AllDistance -= 1;
		GameObject.Find("Delete Distance").GetComponent<Text>().text = "-1";
		gameObject.GetComponent<AudioSource>().PlayOneShot(SoundOfTap);
		if(!OneForce){
			if(gameObject.transform.localPosition.z<0){
				gameObject.GetComponent<Rigidbody>().velocity = 
					gameObject.GetComponent<Rigidbody>().velocity - AddForce*1;
			}else{
				gameObject.GetComponent<Rigidbody>().velocity = 
					gameObject.GetComponent<Rigidbody>().velocity + AddForce*1;
			}
		}else{
			gameObject.GetComponent<Rigidbody>().velocity = 
				gameObject.GetComponent<Rigidbody>().velocity + AddForce*1;
		}
	}

	void Update(){
		int Point = 0;
		int Recorder = 0;
		while(Point == 0){
			if(Recorder<ArrayPoint.Length){
				LineRope.SetPosition(Recorder,ArrayPoint[Recorder].transform.position);
				Recorder++;
			}else{Point=1;}
		}
	}


}
