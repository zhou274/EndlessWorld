using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchTap : MonoBehaviour {
	
	public LayerMask touchInputMask;
	private List<GameObject> touchList = new List<GameObject>();
	private GameObject[] touchOld;
	private RaycastHit hit;
	private int SaveTouch;
	
	void FixedUpdate (){
		
		
#if UNITY_EDITOR	
#endif
		
		if(Input.touchCount < SaveTouch){SaveTouch = Input.touchCount;}
		
		if(Input.touchCount > 0 && Input.touchCount > SaveTouch){
		SaveTouch = Input.touchCount;
		touchOld = 	new GameObject[touchList.Count];
		touchList.CopyTo(touchOld);
		touchList.Clear();
		
		foreach (Touch touch in Input.touches){
		Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
		if(Physics.Raycast(ray, out hit, touchInputMask)){
		GameObject recipient = hit.transform.gameObject;
		touchList.Add(recipient);
		
		if(touch.phase == TouchPhase.Began){
		recipient.SendMessage("OnTouchDown",hit.point,SendMessageOptions.DontRequireReceiver);	
		}
		}
		}
			
		foreach (GameObject g in touchOld){
		if(g!=null&&!touchList.Contains(g)){
		g.SendMessage("OnTouchDown",hit.point,SendMessageOptions.DontRequireReceiver);
		}
		}
		}
		
		
		
	}	
}