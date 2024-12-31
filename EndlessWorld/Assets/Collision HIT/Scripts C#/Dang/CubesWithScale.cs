using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CubesWithScale : MonoBehaviour {

	private Vector3 StartScale;
	public Vector3 EndScale;
	public float SpeedInt;
	public float TimeOfEndScale;
	private bool Non = false;	
	public float ResultSpeed;
	private float ResultOfScale;

	public AudioClip SoundStartChange;
	public AudioClip SoundEndChange;


	void Start(){
		StartScale = gameObject.transform.localScale;
		if(SpeedInt != 0){ ResultSpeed = 0.01f * SpeedInt; } else{ ResultSpeed = 0.01f; }
		var DeltaScale = StartScale - EndScale;
		ResultOfScale = Mathf.Max(DeltaScale.x, DeltaScale.y, DeltaScale.z);
	}

	void OnMouseDown(){
		if (gameObject.GetComponent<CubesWithScale> ().enabled == true) {
			if (!Non) {
				Non = true;
				StartCoroutine ("Work");
			}
		}
	}
	void OnTouchDown(){if(!Non){Non=true; StartCoroutine("Work");}}

	IEnumerator Work(){
		GameObject.Find("Main Camera").GetComponent<CameraMove>().DeleteDist();
		GameObject.Find("Main Camera").GetComponent<CameraMove>().AllDistance -= 5;
		GameObject.Find("Delete Distance").GetComponent<Text>().text = "-5";
		gameObject.GetComponent<AudioSource>().PlayOneShot(SoundStartChange);
		int Point = 0;
		float ScaleController = 0;
		while(Point == 0){
			if(ScaleController < ResultOfScale){
				if(gameObject.transform.localScale.x > EndScale.x){
					gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - ResultSpeed,gameObject.transform.localScale.y,gameObject.transform.localScale.z);
				}
				if(gameObject.transform.localScale.y > EndScale.y){
					gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,gameObject.transform.localScale.y - ResultSpeed,gameObject.transform.localScale.z);
				}
				if(gameObject.transform.localScale.z > EndScale.z){
					gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,gameObject.transform.localScale.y,gameObject.transform.localScale.z - ResultSpeed);
				}
				ScaleController += ResultSpeed;
			}else{Point = 1;}
			yield return new WaitForSeconds(0.033f);
		}
		
		yield return new WaitForSeconds(TimeOfEndScale);
		gameObject.GetComponent<AudioSource>().PlayOneShot(SoundEndChange);
		
		while(Point == 1){
			if(ScaleController > 0){
				if(gameObject.transform.localScale.x < StartScale.x){
					gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + ResultSpeed,gameObject.transform.localScale.y,gameObject.transform.localScale.z);
				}
				if(gameObject.transform.localScale.y < StartScale.y){
					gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,gameObject.transform.localScale.y + ResultSpeed,gameObject.transform.localScale.z);
				}
				if(gameObject.transform.localScale.z < StartScale.z){
					gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,gameObject.transform.localScale.y,gameObject.transform.localScale.z + ResultSpeed);
				}
				ScaleController -= ResultSpeed;
			}else{Point = 2;}
			yield return new WaitForSeconds(0.033f);
		}
		
		Non=false;
	}

}
