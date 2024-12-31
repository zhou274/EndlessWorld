using UnityEngine;
using System.Collections;

public class EffectsLoad : MonoBehaviour {

	public Color[] ColorOfDL;
	public float[] IntensityOfDL;
	public Color[] ColorOfFog;

	private GameObject DL2;
	[HideInInspector]
	public int FirstSettings;

	void Start(){
		DL2 = GameObject.Find("Directional light 2");
	}

	public IEnumerator NewSettings(){
		int Point = 0;
		int FirstSet = 0;
		float ReservedFloat = 1;
		Vector3 SetColors = new Vector3(0,0,0);

		while(Point == 0){
			if(ReservedFloat > 0){
				if(FirstSet == 0){
					SetColors.x = (RenderSettings.fogColor.r - ColorOfFog[FirstSettings].r) / 50;
					SetColors.y = (RenderSettings.fogColor.g - ColorOfFog[FirstSettings].g) / 50;
					SetColors.z = (RenderSettings.fogColor.b - ColorOfFog[FirstSettings].b) / 50;
					FirstSet = 1;
				}
				RenderSettings.fogColor = new Color(RenderSettings.fogColor.r - SetColors.x,RenderSettings.fogColor.g - SetColors.y,RenderSettings.fogColor.b - SetColors.z, 1);
				gameObject.GetComponent<Camera>().backgroundColor = RenderSettings.fogColor;
				ReservedFloat -= 0.02f;
			}else{Point = 1;}
			yield return new WaitForSeconds(0.03f);
		}
		while(Point == 1){
			if(ReservedFloat < 1){
				if(FirstSet == 1){
					SetColors.x = (DL2.GetComponent<Light>().color.r - ColorOfDL[FirstSettings].r) / 50;
					SetColors.y = (DL2.GetComponent<Light>().color.g - ColorOfDL[FirstSettings].g) / 50;
					SetColors.z = (DL2.GetComponent<Light>().color.b - ColorOfDL[FirstSettings].b) / 50;
					FirstSet = 2;
				}
				DL2.GetComponent<Light>().color = new Color(DL2.GetComponent<Light>().color.r - SetColors.x,DL2.GetComponent<Light>().color.g - SetColors.y,DL2.GetComponent<Light>().color.b - SetColors.z,1);
				ReservedFloat += 0.02f;
			}else{Point = 2;}
			yield return new WaitForSeconds(0.03f);
		}
		while(Point == 2){
			if(ReservedFloat > 0){
				if(FirstSet == 2){
					SetColors.x = (DL2.GetComponent<Light>().intensity - IntensityOfDL[FirstSettings]) / 50;
					FirstSet = 3;
				}
				DL2.GetComponent<Light>().intensity -= SetColors.x;
				ReservedFloat -= 0.02f;
			}else{Point = 3;}
			yield return new WaitForSeconds(0.03f);
		}
	}


}
