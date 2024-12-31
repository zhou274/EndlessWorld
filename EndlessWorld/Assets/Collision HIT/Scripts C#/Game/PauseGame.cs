using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class PauseGame : MonoBehaviour {

	public string NameOfLevel;
	public string NameOfMenu;
	public float VolumeOfMusic;
	public GameObject PausePanel;


	private int Lock = 0;
	private GameObject BlackFirst;
	private GameObject BlackSecond;
	private GameObject BlackThird;
	private GameObject MusicObject;
	private float SaveTime;
	private float SavePitch;
	private GameObject FindCamera;


	void Start(){
		StartCoroutine("Starting");
	}


	IEnumerator Starting(){
		BlackFirst = GameObject.Find("Black Fon 1");
		BlackSecond = GameObject.Find("Black Fon 2");
		BlackThird = GameObject.Find("Black Fon 3");
		MusicObject = GameObject.Find("Music");
		FindCamera = GameObject.Find("Main Camera");
		int Point = 0;
		while(Point == 0){
			if (BlackFirst.GetComponent<Image>().color.a > 0){
				BlackFirst.GetComponent<Image>().color = new Color (BlackFirst.GetComponent<Image>().color.r,BlackFirst.GetComponent<Image>().color.g,BlackFirst.GetComponent<Image>().color.b,BlackFirst.GetComponent<Image>().color.a - 0.05f);
				BlackSecond.GetComponent<Image>().color = new Color (BlackSecond.GetComponent<Image>().color.r,BlackSecond.GetComponent<Image>().color.g,BlackSecond.GetComponent<Image>().color.b,BlackSecond.GetComponent<Image>().color.a - 0.05f);
				if(MusicObject.GetComponent<AudioSource>().volume<VolumeOfMusic){
					MusicObject.GetComponent<AudioSource>().volume += 0.05f;
				}
			}else{
				Point = 1;
				BlackFirst.GetComponent<Image>().enabled=false;
				BlackSecond.GetComponent<Image>().enabled=false;
			}
			yield return new WaitForSeconds(0.05f);
		}
	}


	public void OnPause(){if(Lock == 0){ Lock=1; StartCoroutine("TapOnPause");}}
	public void TapBeyond(){if(Lock == 0){ Lock=1; StartCoroutine("TapOnBeyond");}}
	public void TapReset(){if(Lock == 0){ StartCoroutine("TapOnReset");}}
	public void TapMenu(){if(Lock == 0){ StartCoroutine("TapOnMenu");}}

	IEnumerator TapOnPause(){
		PausePanel.SetActive(true);
		FindCamera.GetComponent<CameraMove>().SaveDistance();
		GameObject.Find("Pause Table").GetComponent<Image>().enabled=true;
		gameObject.GetComponent<Animation>().Play("PauseInGameTrue");
		GameObject.Find("NoTap").GetComponent<BoxCollider>().enabled=true;
		BlackThird.GetComponent<Image>().enabled=true;
		SavePitch = MusicObject.GetComponent<AudioSource>().pitch;
		int Point = 0;
		while (Point == 0){
			if(MusicObject.GetComponent<AudioSource>().pitch > 0.1f){
				MusicObject.GetComponent<AudioSource>().pitch -= 0.1f;
				BlackThird.GetComponent<Image>().color = new Color(BlackThird.GetComponent<Image>().color.r,BlackThird.GetComponent<Image>().color.g,BlackThird.GetComponent<Image>().color.b,BlackThird.GetComponent<Image>().color.a + 0.025f);
			}else{Point = 1;}
			yield return new WaitForSeconds(0.033f);
		}
		MusicObject.GetComponent<AudioSource>().pitch=0;
		VolumeOfMusic = MusicObject.GetComponent<AudioSource>().volume;
		Lock=0;
		if(Time.timeScale < 1){
			yield return new WaitForSeconds(0.3f);
		}
		SaveTime = Time.timeScale;
		AudioListener.pause=true;
		Time.timeScale=0;
	}


	IEnumerator TapOnBeyond(){
		Time.timeScale = SaveTime;
		if(PlayerPrefs.GetInt("SoundActive") == 1){
			AudioListener.pause=false;
		}
		gameObject.GetComponent<Animation>().Play("PauseInGameFalse");
		GameObject.Find("NoTap").GetComponent<BoxCollider>().enabled=false;
		int Point = 0;
		while (Point == 0){
			if(MusicObject.GetComponent<AudioSource>().pitch < 1){
				MusicObject.GetComponent<AudioSource>().pitch += 0.1f;
				BlackThird.GetComponent<Image>().color = new Color(BlackThird.GetComponent<Image>().color.r,BlackThird.GetComponent<Image>().color.g,BlackThird.GetComponent<Image>().color.b,BlackThird.GetComponent<Image>().color.a - 0.025f);
			}else{Point = 1;
				BlackThird.GetComponent<Image>().enabled=false;
			}
			yield return new WaitForSeconds(0.033f);
		}
		MusicObject.GetComponent<AudioSource>().pitch = SavePitch;
		GameObject.Find("Pause Table").GetComponent<Image>().enabled=false;
		while (Point == 1){
			if(MusicObject.GetComponent<AudioSource>().volume<VolumeOfMusic){
				MusicObject.GetComponent<AudioSource>().volume += 0.01f;
			}else{Point = 2;}
			yield return new WaitForSeconds(0.05f);
		}
		Lock=0;
		PausePanel.SetActive(false);
	}



	IEnumerator TapOnReset(){
		Time.timeScale = SaveTime;
		if(PlayerPrefs.GetInt("SoundActive") == 1){
			AudioListener.pause=false;
		}
		FindCamera.GetComponent<CameraMove>().enabled=false;
		gameObject.GetComponent<Animation>().Play("PauseInGameFalseEnd");
		BlackFirst.GetComponent<Image>().enabled = true;
		BlackSecond.GetComponent<Image>().enabled = true;
		int Point = 0;
		while(Point < 2){
			if (BlackSecond.GetComponent<Image>().color.a < 1){
				if(Point == 0){
					if(MusicObject.GetComponent<AudioSource>().pitch < 0.5f){
						MusicObject.GetComponent<AudioSource>().pitch += 0.1f;
					}else{Point = 1;}
				}
				if(Point == 1){
					if(MusicObject.GetComponent<AudioSource>().pitch > 0){
						MusicObject.GetComponent<AudioSource>().pitch -= 0.1f;
					}
				}
				BlackFirst.GetComponent<Image>().color = new Color(0,0,0,BlackFirst.GetComponent<Image>().color.a + 0.1f);
				BlackThird.GetComponent<Image>().color = new Color(0,0,0,BlackThird.GetComponent<Image>().color.a - 0.025f);
				BlackSecond.GetComponent<Image>().color = new Color(0,0,0,BlackSecond.GetComponent<Image>().color.a + 0.05f);
			}else{
				Point = 2;
				MusicObject.GetComponent<AudioSource>().pitch = 0;
				Application.LoadLevel(NameOfLevel);
			}
			yield return new WaitForSeconds(0.04f);
		}
		PausePanel.SetActive(false);
	}

	IEnumerator TapOnMenu(){
		Time.timeScale = SaveTime;
		if(PlayerPrefs.GetInt("SoundActive") == 1){
			AudioListener.pause=false;
		}
		FindCamera.GetComponent<CameraMove>().enabled=false;
		gameObject.GetComponent<Animation>().Play("PauseInGameFalseEnd");;
		BlackFirst.GetComponent<Image>().enabled=true;
		BlackSecond.GetComponent<Image>().enabled=true;
		MusicObject.GetComponent<AudioSource>().pitch = 1.1f;
		int Point = 0;
		while(Point < 2){
			if (BlackSecond.GetComponent<Image>().color.a<1){
				if(Point == 0){
					if(MusicObject.GetComponent<AudioSource>().pitch < 0.5f){
						MusicObject.GetComponent<AudioSource>().pitch += 0.1f;
					}else{Point = 1;}
				}
				if(Point == 1){
					if(MusicObject.GetComponent<AudioSource>().pitch > 0){
						MusicObject.GetComponent<AudioSource>().pitch -= 0.1f;
					}
				}
				MusicObject.GetComponent<AudioSource>().pitch -= 0.04f;
				BlackFirst.GetComponent<Image>().color = new Color(0,0,0,BlackFirst.GetComponent<Image>().color.a + 0.1f);
				BlackThird.GetComponent<Image>().color = new Color(0,0,0,BlackThird.GetComponent<Image>().color.a - 0.025f);
				BlackSecond.GetComponent<Image>().color = new Color(0,0,0,BlackSecond.GetComponent<Image>().color.a + 0.05f);
			}else{
				Point = 2;
				MusicObject.GetComponent<AudioSource>().pitch = 0;
				Application.LoadLevel(NameOfMenu);
			}
			yield return new WaitForSeconds(0.04f);
		}
		PausePanel.SetActive(false);
	}



}
