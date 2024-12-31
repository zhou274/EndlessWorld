using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ResLoader : MonoBehaviour {

	public GameObject FromPrefab;
	public string NameOfNextPart;
	private Transform PivotOfNewPart;
	public float NewSpeedOfMove;

	private float MoreDistance;

	public bool IfThisLastPart = false;
	public float DistanceOfNewCP;
	public string TextInLB;
	public int NewGamma;

	private GameObject OldPart;
	private GameObject MainCam;
	private bool DontRepeat = false;

	void Start(){
		PivotOfNewPart = transform.Find("Pivot");
		OldPart = GameObject.Find(gameObject.transform.parent.name);
		MainCam = GameObject.Find("Main Camera");
		MoreDistance = -gameObject.transform.localPosition.z;
	}

	void OnTriggerEnter(Collider other){ 
		if(gameObject.GetComponent<ResLoader>().enabled == true){
		if(other.gameObject.tag=="Camera"){
			if(!DontRepeat){ DontRepeat = true;
				StartCoroutine("MainProcess");
			}
		}
		}
	}

	IEnumerator MainProcess(){
		MainCam.GetComponent<CameraMove>().ApproxSpeed = NewSpeedOfMove;
		MainCam.GetComponent<CameraMove>().SaveDistance();
		GameObject NewPart = Instantiate(FromPrefab);
		NewPart.transform.parent = GameObject.Find("LineRooms").transform;
		NewPart.name = NewPart.name.Substring(0,(NewPart.name.Length-7));
		NewPart.transform.position = PivotOfNewPart.position;
		NewPart.transform.rotation = PivotOfNewPart.rotation;
		if(IfThisLastPart){StartCoroutine("Reconstruction");}
		yield return new WaitForSeconds(5);
		MainCam.transform.position = new Vector3 (MainCam.transform.position.x,MainCam.transform.position.y,MainCam.transform.position.z - MoreDistance);
		gameObject.transform.parent.transform.position = new Vector3 (gameObject.transform.parent.transform.position.x,gameObject.transform.parent.transform.position.y,gameObject.transform.parent.transform.position.z - MoreDistance);
		GameObject.Find(NameOfNextPart).transform.position = new Vector3 (GameObject.Find(NameOfNextPart).transform.position.x,GameObject.Find(NameOfNextPart).transform.position.y,GameObject.Find(NameOfNextPart).transform.position.z - MoreDistance); 
		NewPart.transform.position = new Vector3 (NewPart.transform.position.x,NewPart.transform.position.y,NewPart.transform.position.z - MoreDistance);
		OldPart.transform.position = new Vector3 (0,0,-1000);
		Destroy(OldPart);
	}

	IEnumerator Reconstruction(){
		MainCam.GetComponent<CameraMove>().OnlyDistance = DistanceOfNewCP;
		MainCam.GetComponent<CameraMove>().NewWayLine();
		GameObject.Find("AmazeOther").GetComponent<TextMeshProUGUI>().text = TextInLB;
		MainCam.GetComponent<EffectsLoad>().FirstSettings = NewGamma;
		MainCam.GetComponent<EffectsLoad>().StartCoroutine("NewSettings");
		
		if(PlayerPrefs.GetFloat("SaveCPAll") < NewGamma){
			PlayerPrefs.SetFloat("SaveCPAll",NewGamma);
		}
		
		PlayerPrefs.SetInt("SaveCPNow",NewGamma);
		PlayerPrefs.SetInt("RealDistance",MainCam.GetComponent<CameraMove>().DistanceForUI);
		PlayerPrefs.SetFloat("RealHealth",MainCam.GetComponent<SystemKilling>().JustHP);
		PlayerPrefs.SetFloat("RealDistanceOnly",MainCam.GetComponent<CameraMove>().OnlyDistance);
		PlayerPrefs.SetFloat("RealDistanceOnlySaved",MainCam.GetComponent<CameraMove>().OnlyDistanceSaved);
		
		GameObject CPLook = GameObject.Find("CP Look");
		CPLook.GetComponent<TextMeshProUGUI>().color = new Color (CPLook.GetComponent<TextMeshProUGUI>().color.r,CPLook.GetComponent<TextMeshProUGUI>().color.g,CPLook.GetComponent<TextMeshProUGUI>().color.b,1);
		CPLook.GetComponent<Animation>().Play("Saved CP");
		yield return new WaitForSeconds(2);
		int Point = 0;
		while(Point == 0){
			if(CPLook.GetComponent<TextMeshProUGUI>().color.a > 0){
				CPLook.GetComponent<TextMeshProUGUI>().color = new Color (CPLook.GetComponent<TextMeshProUGUI>().color.r,CPLook.GetComponent<TextMeshProUGUI>().color.g,CPLook.GetComponent<TextMeshProUGUI>().color.b,CPLook.GetComponent<TextMeshProUGUI>().color.a - 0.1f);
			}else{Point = 1;}
			yield return new WaitForSeconds(0.033f);
		} 
		CPLook.GetComponent<TextMeshProUGUI>().enabled = false;
	}


}
