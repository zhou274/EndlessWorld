using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlaneSliceAnim : MonoBehaviour {

	private bool Once = true;
	public AnimationClip NameOfAnimation;
	public bool DoubleTime = false;
	public AudioClip SoundOfTap;
	public GameObject StartObject;
	public GameObject[] ArrayOfPieces;

	void OnMouseDown(){
		if (gameObject.GetComponent<PlaneSliceAnim> ().enabled == true) {
			gameObject.transform.parent.GetComponent<PlaneSliceAnim> ().StartCoroutine ("Work");
		}
	}
	void OnTouchDown(){gameObject.transform.parent.GetComponent<PlaneSliceAnim>().StartCoroutine("Work");}

	IEnumerator Work(){
		if(Once){
			GameObject.Find("Main Camera").GetComponent<CameraMove>().DeleteDist();
			GameObject.Find("Main Camera").GetComponent<CameraMove>().AllDistance -= 4;
			GameObject.Find("Delete Distance").GetComponent<Text>().text = "-4";
			Once=false;
			StartObject.GetComponent<MeshRenderer>().enabled=false;
			int Point = 0;
			while (Point<ArrayOfPieces.Length){
				ArrayOfPieces[Point].GetComponent<MeshRenderer>().enabled=true;
				Point++;
			}
			gameObject.GetComponent<AudioSource>().PlayOneShot(SoundOfTap);
			gameObject.GetComponent<Animation>().Play(NameOfAnimation.name);
			yield return new WaitForSeconds(NameOfAnimation.length);
			if(DoubleTime){yield return new WaitForSeconds(NameOfAnimation.length);}
			gameObject.GetComponent<Animation>().Stop();
			StartObject.GetComponent<MeshRenderer>().enabled=true;
			while (Point>0){
				ArrayOfPieces[Point-1].GetComponent<MeshRenderer>().enabled=false;
				Point--;
			}
			Once=true;
		}
		
	}

	void Update(){
		
	}

}
