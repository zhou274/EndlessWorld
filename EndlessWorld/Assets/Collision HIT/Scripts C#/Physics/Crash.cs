using UnityEngine;
using System.Collections;

public class Crash : MonoBehaviour {
	
	private GameObject Effect_to;
	public GameObject Effect_after;
	public AudioClip SoundOfTap;
	private float VarianceSpeed;

	private bool LoopNon = true;
	private float RealySpeed;
	private GameObject MainCamera;
	private int Point = 0;

	void Start(){
		MainCamera=GameObject.Find("Main Camera");
		RealySpeed = gameObject.transform.parent.transform.localScale.x / 40;
		VarianceSpeed = gameObject.transform.parent.transform.localScale.x * Random.Range(-0.007f , 0.007f);
		gameObject.GetComponent<AudioSource>().pitch += VarianceSpeed*30.01f;
		Effect_to = gameObject.transform.parent.Find("Fireball").gameObject;
	}

	void OnTouchDown(){ StartCoroutine("Doing"); }
	void OnMouseDown(){ 
		if (gameObject.GetComponent<Crash> ().enabled == true) {
			StartCoroutine ("Doing"); 
		}
	}

	IEnumerator Doing(){
		if(LoopNon){
			LoopNon=false;
			gameObject.GetComponent<BoxCollider>().enabled = false;
			Effect_to.GetComponent<LensFlare>().color =  new Color32(157,0,0,0);
			Effect_to.GetComponent<LensFlare>().brightness = 0.7f;
			MainCamera.GetComponent<SystemKilling>().AddHP();
			gameObject.GetComponent<AudioSource>().PlayOneShot(SoundOfTap);
			Effect_after.SetActive(true);
			Point = 0;
			while (Point == 0){
				if(gameObject.transform.parent.transform.localScale.z>0){
					gameObject.transform.parent.transform.localScale = new Vector3 (gameObject.transform.parent.transform.localScale.x,gameObject.transform.parent.transform.localScale.y,gameObject.transform.parent.transform.localScale.z - RealySpeed * 1.08f + VarianceSpeed);
					if(gameObject.transform.parent.transform.localScale.x>0){
						MainCamera.GetComponent<SystemKilling>().JustHP += 0.0025f;
					}
				}else{
					MainCamera.GetComponent<SystemKilling>().StartCoroutine("AddHPEnd");
					gameObject.transform.parent.transform.localScale = new Vector3(0,0,0);
					Point = 1;
					Effect_to.SetActive(false);
					yield return new WaitForSeconds(4);
					Destroy(gameObject.transform.parent.gameObject);
				}
				yield return new WaitForSeconds(0.05f);
			}
		}
	}

}
