using UnityEngine;
using System.Collections;

public class PivotSearch : MonoBehaviour {

	private GameObject NextCP;

	void Start () {
		Transform OldParent = gameObject.transform.parent;
		NextCP = GameObject.Find(gameObject.transform.parent.GetComponent<ResLoader>().FromPrefab.name);
		gameObject.transform.parent = GameObject.Find("StaticParent").transform;
		gameObject.transform.position = NextCP.transform.position;
		gameObject.transform.rotation = NextCP.transform.rotation;
		gameObject.transform.parent = OldParent;
	}

}
