using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class CameraMove : MonoBehaviour {

	[HideInInspector]
	public float BaseSpeed;
	public float ApproxSpeed;

	[HideInInspector]
	public float AllDistance;
	public float OnlyDistance;
	[HideInInspector]
	public int DistanceForUI;
	[HideInInspector]
	public float OnlyDistanceSaved;

	[HideInInspector]
	public float RotateAngle;
	[HideInInspector]
	public float RotateSpeed;

	private GameObject DistanceUI;
	private GameObject DeleteDistanceUI;
	private GameObject Way_LineUI;
	private int OnceDelete = 0;

	void Start(){
		BaseSpeed = ApproxSpeed;
		Way_LineUI = GameObject.Find("Way Line");
		DistanceUI = GameObject.Find("Distance");
		DeleteDistanceUI = GameObject.Find("Delete Distance");
		StartCoroutine("UpdateHealth");
	}

	public void NewWayLine(){
		OnlyDistanceSaved=0;
		int DistanceSave = (int)AllDistance;
		PlayerPrefs.SetInt("AllDistance",DistanceSave);
	}

	public IEnumerator DeleteDist(){
		if(OnceDelete == 0){ 
			OnceDelete = 1;
			DeleteDistanceUI.GetComponent<Animation>().Play("DeleteDistance");
			int Point = 0;
			while(Point == 0){
				
				if(DeleteDistanceUI.GetComponent<TextMeshProUGUI>().color.a < 0.77f){
					DeleteDistanceUI.GetComponent<TextMeshProUGUI>().color = new Color(DeleteDistanceUI.GetComponent<TextMeshProUGUI>().color.r,DeleteDistanceUI.GetComponent<TextMeshProUGUI>().color.g,DeleteDistanceUI.GetComponent<TextMeshProUGUI>().color.b,DeleteDistanceUI.GetComponent<TextMeshProUGUI>().color.a + 0.05f );
				}else{Point = 1;}
				yield return new WaitForSeconds(0.033f);
			}
			while(Point == 1){
				if(DeleteDistanceUI.GetComponent<TextMeshProUGUI>().color.a > 0){
					DeleteDistanceUI.GetComponent<TextMeshProUGUI>().color = new Color(DeleteDistanceUI.GetComponent<TextMeshProUGUI>().color.r,DeleteDistanceUI.GetComponent<TextMeshProUGUI>().color.g,DeleteDistanceUI.GetComponent<TextMeshProUGUI>().color.b,DeleteDistanceUI.GetComponent<TextMeshProUGUI>().color.a - 0.05f );
				}else{Point = 2;}
				yield return new WaitForSeconds(0.033f);
			}
			OnceDelete = 0;
		}
	}

	public void SaveDistance(){
		if(PlayerPrefs.GetInt("AllDistance") < AllDistance){
			PlayerPrefs.SetInt("AllDistance",(int)AllDistance);
		}
	}

	public IEnumerator Rotate(){
		int Point = 0;
		while (Point == 0){
			if(BaseSpeed > 0.05f){
				if(RotateAngle>0){
					if(RotateSpeed<0){RotateAngle += RotateSpeed;}
					else{RotateAngle -= RotateSpeed;}
					gameObject.transform.Rotate(0,0,RotateSpeed);
				}else{Point = 1;}
			}else{Point = 1;}
			yield return new WaitForSeconds(0.033f);
		}
	}

	void FixedUpdate () {
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z + BaseSpeed);
		AllDistance += BaseSpeed;
		OnlyDistanceSaved += BaseSpeed;
		
		Way_LineUI.GetComponent<Image>().fillAmount = OnlyDistanceSaved/OnlyDistance;
		
		if(BaseSpeed < ApproxSpeed){ BaseSpeed += BaseSpeed * 0.01f;}
		if(BaseSpeed > ApproxSpeed){ BaseSpeed -= BaseSpeed * 0.01f;}
	}

	IEnumerator UpdateHealth(){
		int Point = 0;
		while(Point == 0){
			DistanceForUI = (int)AllDistance;
			
			if(DistanceForUI<1000){
				DistanceUI.GetComponent<TextMeshProUGUI>().text = DistanceForUI.ToString();
			}
			if(DistanceForUI>=1000 && DistanceForUI<10000){
				DistanceUI.GetComponent<TextMeshProUGUI>().text = DistanceForUI.ToString().Substring(0,1)+" "+DistanceForUI.ToString().Substring(1);
			}
			if(DistanceForUI>=10000 && DistanceForUI<100000){
				DistanceUI.GetComponent<TextMeshProUGUI>().text = DistanceForUI.ToString().Substring(0,2)+" "+DistanceForUI.ToString().Substring(2);
			}
			if(DistanceForUI>100000){
				DistanceUI.GetComponent<TextMeshProUGUI>().text = DistanceForUI.ToString().Substring(0,3)+" "+DistanceForUI.ToString().Substring(3);
			}
			
			yield return new WaitForSeconds(0.1f);
		}
	}

	public IEnumerator SpeedOfCameraDown(){
		int Point = 0;
		while(Point == 0){
			if(ApproxSpeed>0){
				ApproxSpeed -= 0.005f;
				BaseSpeed -= 0.005f;
			}else{Point = 1;}
			yield return new WaitForSeconds(0.033f);
		}
	}

}
