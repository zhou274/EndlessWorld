using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToContinue : MonoBehaviour {

	public GameObject[] RoomsSet;
	public Vector3[] ZPositionOfCamera;

	void Start(){
		StartCoroutine("StartIE");
	}

	IEnumerator StartIE(){
		if(PlayerPrefs.GetInt("ContinueTrue") == 1){
			Destroy(RoomsSet[0]);
			Destroy(RoomsSet[1]);
			yield return new WaitForSeconds(0.1f);
			StartCoroutine("SetANewParameters");
		}
	}

	void SetANewParameters(){
		int SaveCPNow = PlayerPrefs.GetInt("SaveCPNow");
		GameObject.Find("prop with Camera").transform.position = ZPositionOfCamera[SaveCPNow];
		GameObject NewPart_1 = Instantiate(RoomsSet[SaveCPNow*2]);
		GameObject NewPart_2 = Instantiate(RoomsSet[SaveCPNow*2+1]);
		
		NewPart_1.transform.parent = gameObject.transform;
		NewPart_2.transform.parent = gameObject.transform;
		
		NewPart_1.name = NewPart_1.name.Substring(0,(NewPart_1.name.Length-7));
		NewPart_2.name = NewPart_2.name.Substring(0,(NewPart_2.name.Length-7));
		
		
		RenderSettings.fogColor = GameObject.Find("Main Camera").GetComponent<EffectsLoad>().ColorOfFog[SaveCPNow];
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = RenderSettings.fogColor;
		GameObject.Find("Directional light 2").GetComponent<Light>().color = GameObject.Find("Main Camera").GetComponent<EffectsLoad>().ColorOfDL[SaveCPNow];
		GameObject.Find("Directional light 2").GetComponent<Light>().intensity = GameObject.Find("Main Camera").GetComponent<EffectsLoad>().IntensityOfDL[SaveCPNow];
		GameObject.Find("Main Camera").GetComponent<SystemKilling>().JustHP = PlayerPrefs.GetFloat("RealHealth");
		GameObject.Find("Main Camera").GetComponent<CameraMove>().AllDistance = PlayerPrefs.GetFloat("RealDistance");
		GameObject.Find("Main Camera").GetComponent<CameraMove>().OnlyDistance = PlayerPrefs.GetFloat("RealDistanceOnly");
		GameObject.Find("Main Camera").GetComponent<CameraMove>().OnlyDistanceSaved = PlayerPrefs.GetFloat("RealDistanceOnlySaved");
		GameObject.Find("AmazeOther").GetComponent<Text>().text = (SaveCPNow+1).ToString();
	}


}
