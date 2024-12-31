using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class MenuCentre : MonoBehaviour {

	private int SoundActive = 0;
	public Sprite SoundYes;
	public Sprite SoundNot;
	public float MaxDistance;
	public string LevelName;
	public string LinkToRate;

	public Sprite Coninue;
	private int HaveAccess = 0;

	void Start(){
		if(PlayerPrefs.GetInt("Chance") > 0 && PlayerPrefs.GetInt("SaveCPNow") > 0){
			GameObject.Find("Checkpoint Go").GetComponent<Image>().sprite = Coninue;
			HaveAccess = 1;
		}
		StartCoroutine("Starting");
	}


	IEnumerator Starting(){

		if(PlayerPrefs.HasKey("SoundActive")){
			SoundActive=PlayerPrefs.GetInt("SoundActive");
		}else{SoundActive=1;}
		DoingSound();
		
		
		if(PlayerPrefs.HasKey("AllDistance")){}else{PlayerPrefs.SetInt("AllDistance",0);}
		GameObject.Find("Distance").GetComponent<TextMeshProUGUI>().text=PlayerPrefs.GetInt("AllDistance").ToString();
		if(PlayerPrefs.GetInt("AllDistance")>=1000&&PlayerPrefs.GetInt("AllDistance")<10000){
			GameObject.Find("Distance").GetComponent<TextMeshProUGUI>().text=PlayerPrefs.GetInt("AllDistance").ToString().Substring(0,1)+" "+PlayerPrefs.GetInt("AllDistance").ToString().Substring(1);
		}
		if(PlayerPrefs.GetInt("AllDistance")>=10000){
			GameObject.Find("Distance").GetComponent<TextMeshProUGUI>().text=PlayerPrefs.GetInt("AllDistance").ToString().Substring(0,2)+" "+PlayerPrefs.GetInt("AllDistance").ToString().Substring(2);
		}
		
		if(PlayerPrefs.GetInt("AllDistance")>MaxDistance){
			GameObject.Find("Way Infinity").GetComponent<TextMeshProUGUI>().enabled=true;
			
		}
		float fAm = PlayerPrefs.GetFloat("SaveCPAll") * 0.111f;
		GameObject.Find("Way Line").GetComponent<Image>().fillAmount=fAm;
		yield return new WaitForSeconds(0.2f);
		int Point = 0;
		GameObject Black = GameObject.Find("Black");
		while(Point == 0){
			if(Black.GetComponent<Image>().color.a>0){
				Black.GetComponent<Image>().color = new Color (0,0,0, Black.GetComponent<Image>().color.a - 0.1f);
			}else{Point=1; Black.GetComponent<Image>().enabled=false;}
			yield return new WaitForSeconds(0.05f);
		}
		if(PlayerPrefs.HasKey("RateEnd")){}else{PlayerPrefs.SetInt("RateEnd",0);}
		if(PlayerPrefs.GetInt("AllDistance") > 3000 && PlayerPrefs.GetInt("RateEnd") != 1){
			GameObject.Find("Rate").GetComponent<Image>().enabled = true;
		}

	}

	public void TapOnGo(){StartCoroutine("OnGo");}

	IEnumerator OnGo(){
		PlayerPrefs.SetInt("Chance",3);
		PlayerPrefs.SetInt("ContinueTrue",0);
		PlayerPrefs.SetInt("SaveCPNow",0);
		GameObject Black = GameObject.Find("Black");
		GameObject Loading = GameObject.Find("loading");
		GameObject MusicR = GameObject.Find("Music");
		GameObject Triangle = GameObject.Find("triangle");
		MusicR.GetComponent<Animation>().Stop();
		Black.GetComponent<Image>().enabled=true;
		int Point =0;
		while(Point == 0){
			if(Black.GetComponent<Image>().color.a<1){
				Black.GetComponent<Image>().color = new Color(0,0,0,Black.GetComponent<Image>().color.a + 0.1f);
				MusicR.GetComponent<AudioSource>().volume -= 0.1f;
			}else{Point=1;}
			yield return new WaitForSeconds(0.05f);
		}
		Triangle.GetComponent<Image>().enabled=true;
		Loading.GetComponent<TextMeshProUGUI>().enabled=true;
		while(Point == 1){
			if(Loading.GetComponent<TextMeshProUGUI>().color.a<1){
				Loading.GetComponent<TextMeshProUGUI>().color = new Color (Loading.GetComponent<TextMeshProUGUI>().color.r,Loading.GetComponent<TextMeshProUGUI>().color.g,Loading.GetComponent<TextMeshProUGUI>().color.b,Loading.GetComponent<TextMeshProUGUI>().color.a + 0.05f);
				
				Triangle.GetComponent<Image>().color = new Color (Triangle.GetComponent<Image>().color.r,Triangle.GetComponent<Image>().color.g,Triangle.GetComponent<Image>().color.b,Triangle.GetComponent<Image>().color.a + 0.05f);;
			}else{Point=2; Application.LoadLevel(LevelName);}
			yield return new WaitForSeconds(0.05f);
		}
	}

	public void TapOnSound(){
		if(SoundActive==0){SoundActive=1;DoingSound();
		}else{SoundActive=0;DoingSound();}
	}
	
	public void TapOnExit(){
		Application.Quit();
	}

	void DoingSound(){
		if(SoundActive==0){AudioListener.pause=true;
			GameObject.Find("Sound").GetComponent<Image>().sprite = SoundNot;}
		if(SoundActive==1){AudioListener.pause=false;
			GameObject.Find("Sound").GetComponent<Image>().sprite = SoundYes;}
		PlayerPrefs.SetInt("SoundActive",SoundActive);
	}

	public void RateNow(){
		PlayerPrefs.SetInt("RateEnd",1);
		Application.OpenURL(LinkToRate);
	}

	public void TapOnGoContunie(){StartCoroutine("Continue");}

	IEnumerator Continue(){
		if (HaveAccess == 1) {
			PlayerPrefs.SetInt("Chance",(PlayerPrefs.GetInt("Chance")-1));
			PlayerPrefs.SetInt("ContinueTrue",1);
			GameObject Black = GameObject.Find("Black");
			GameObject Loading = GameObject.Find("loading");
			GameObject MusicR = GameObject.Find("Music");
			GameObject Triangle = GameObject.Find("triangle");
			MusicR.GetComponent<Animation>().Stop();
			Black.GetComponent<Image>().enabled=true;
			int Point =0;
			while(Point == 0){
				if(Black.GetComponent<Image>().color.a<1){
					Black.GetComponent<Image>().color = new Color(0,0,0,Black.GetComponent<Image>().color.a + 0.1f);
					MusicR.GetComponent<AudioSource>().volume -= 0.1f;
				}else{Point=1;}
				yield return new WaitForSeconds(0.05f);
			}
			Triangle.GetComponent<Image>().enabled=true;
			Loading.GetComponent<TextMeshProUGUI>().enabled=true;
			while(Point == 1){
				if(Loading.GetComponent<TextMeshProUGUI>().color.a<1){
					Loading.GetComponent<TextMeshProUGUI>().color = new Color (Loading.GetComponent<TextMeshProUGUI>().color.r,Loading.GetComponent<TextMeshProUGUI>().color.g,Loading.GetComponent<TextMeshProUGUI>().color.b,Loading.GetComponent<TextMeshProUGUI>().color.a + 0.05f);
					
					Triangle.GetComponent<Image>().color = new Color (Triangle.GetComponent<Image>().color.r,Triangle.GetComponent<Image>().color.g,Triangle.GetComponent<Image>().color.b,Triangle.GetComponent<Image>().color.a + 0.05f);;
				}else{Point=2; Application.LoadLevel(LevelName);}
				yield return new WaitForSeconds(0.05f);
			}
		}

	}

}
