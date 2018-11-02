using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISeguirPers : MonoBehaviour {

    public GameObject obj;
    public Camera camera;
    RectTransform rt;
     float posy;


	// Use this for initialization
	void Start () {


        rt = gameObject.GetComponent<RectTransform>();
        if (gameObject.tag == "jogador") {
            obj = GameObject.Find("Controlador").GetComponent<ControladorVez2>().jogador.transform.GetChild(1).gameObject;
            if(GameObject.Find("Controlador").GetComponent<ControladorVez2>().jogador.tag == "Ladino") {
                posy = 49;
            } else {
                posy = 77;
            }
        } else {
            obj = GameObject.Find("Controlador").GetComponent<ControladorVez2>().inimigo.transform.GetChild(1).gameObject;
            posy = 77;
        }
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 pos = RectTransformUtility.WorldToScreenPoint(camera, new Vector3(obj.transform.position.x, obj.transform.position.y+posy, obj.transform.position.z));
        rt.position = pos;

		
	}
}
