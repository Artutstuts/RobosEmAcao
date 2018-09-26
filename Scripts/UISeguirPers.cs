using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISeguirPers : MonoBehaviour {

    public GameObject obj;
    public Camera camera;
    RectTransform rt;
    public float posy;


	// Use this for initialization
	void Start () {


        rt = gameObject.GetComponent<RectTransform>();

	}
	
	// Update is called once per frame
	void Update () {

        Vector2 pos = RectTransformUtility.WorldToScreenPoint(camera, new Vector3(obj.transform.position.x, obj.transform.position.y+posy, obj.transform.position.z));
        rt.position = pos;

		
	}
}
