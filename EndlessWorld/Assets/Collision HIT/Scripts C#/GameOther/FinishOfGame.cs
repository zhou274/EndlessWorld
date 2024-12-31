using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinishOfGame : MonoBehaviour {
	
	public string MessageToPlayer;
	public int NewGamma;

	private GameObject CP_Look;
	private bool DontRepeat = false;

	void Start(){
		CP_Look = GameObject.Find("CP Look");
	}

	void OnTriggerEnter(Collider other){ 
		if(other.gameObject.tag=="Camera"){
			if(!DontRepeat){ DontRepeat = true;
				StartCoroutine("JustDo");
			}
		}
	}

	IEnumerator JustDo(){
		CP_Look.GetComponent<Text>().color = new Color (CP_Look.GetComponent<Text>().color.r,CP_Look.GetComponent<Text>().color.g,CP_Look.GetComponent<Text>().color.b,1);
		CP_Look.GetComponent<Text>().text = MessageToPlayer;
		CP_Look.GetComponent<Animation>().Play("Finish CP");
		
		yield return new  WaitForSeconds(2);
		int  Point = 0;
		while(Point == 0){
			if(CP_Look.GetComponent<Text>().color.a > 0){
				CP_Look.GetComponent<Text>().color = new Color (CP_Look.GetComponent<Text>().color.r,CP_Look.GetComponent<Text>().color.g,CP_Look.GetComponent<Text>().color.b,CP_Look.GetComponent<Text>().color.a - 0.1f);
			}else{Point = 1;}
			yield return new WaitForSeconds(0.033f);
		}
		CP_Look.GetComponent<Text>().enabled = false;
		
		if(PlayerPrefs.GetInt("SaveCPAll") < NewGamma){
			PlayerPrefs.SetInt("SaveCPAll",NewGamma);
		}
		
		PlayerPrefs.SetInt("SaveCPNow",-1);
		
		
		GameObject.Find("Main Camera").GetComponent<CameraMove>().StartCoroutine("SpeedOfCameraDown");
		PlayerPrefs.SetInt("Room 1",1);
		yield return new WaitForSeconds(2);
		
		GameObject Music = GameObject.Find("Music");
		GameObject BlackFirst = GameObject.Find("Black Fon 1");
		GameObject BlackSecond = GameObject.Find("Black Fon 2");
		BlackFirst.GetComponent<Image>().enabled = true;
		BlackSecond.GetComponent<Image>().enabled = true;
		
		while(Point == 1){
			if (BlackSecond.GetComponent<Image>().color.a<1){
				BlackFirst.GetComponent<Image>().color = new Color(BlackFirst.GetComponent<Image>().color.r,BlackFirst.GetComponent<Image>().color.g,BlackFirst.GetComponent<Image>().color.b,BlackFirst.GetComponent<Image>().color.a + 0.05f);
				BlackSecond.GetComponent<Image>().color = new Color(BlackSecond.GetComponent<Image>().color.r,BlackSecond.GetComponent<Image>().color.g,BlackSecond.GetComponent<Image>().color.b,BlackSecond.GetComponent<Image>().color.a + 0.025f);
				Music.GetComponent<AudioSource>().pitch += 0.025f;
			}else{
				Music.GetComponent<AudioSource>().pitch=0;
				Point = 2;
			}
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitForSeconds(1);
		Application.LoadLevel(GameObject.Find("Canvas Inteface").GetComponent<PauseGame>().NameOfMenu);



	}

}
