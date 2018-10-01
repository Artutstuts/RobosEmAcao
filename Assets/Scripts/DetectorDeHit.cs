using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorDeHit : MonoBehaviour {

    public static bool encostou;
    public static List<SphereCollider> mao = new List<SphereCollider>();
    internal SphereCollider c;

	// Use this for initialization
	void Start () {
        c = gameObject.GetComponent<SphereCollider>();
        mao.Add(c as SphereCollider);
	}
	
	// Update is called once per frame
	void Update () {
      //  print(encostou);
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Esquiva") {
          //  print("OGJISDHWHS");
        }
        if (GameObject.Find("Controlador").GetComponent<ControladorVez2>().inimigo.transform == /*(isso pega o qrCoDE) gameObject.transform.root.gameObject*/ gameObject.transform.parent.IsChildOf(GameObject.Find("Controlador").GetComponent<ControladorVez2>().inimigo.transform)) {
            if (ControladorVez2.vezDoInimigo == true) {
                if (other.gameObject.tag == "TorsoEt") {
                    if (ControladorVez2.tipoAtk == 0) {
                        if (gameObject.tag == "LMao") {
                            //   print("HUEHUE");
                            gameObject.GetComponent<SphereCollider>().enabled = false;
                            encostou = true;
                          //  print(encostou);
                        }
                    } else if (ControladorVez2.tipoAtk == 1) {
                        if (gameObject.tag == "RMao") {
                           // print("HUEHUE");
                            gameObject.GetComponent<SphereCollider>().enabled = false;
                            encostou = true;
                        }
                    } else if (ControladorVez2.tipoAtk == 3) {
                        if (gameObject.tag == "LMao" || gameObject.name == "RMao") {
                           // print("HUEHUE");
                            gameObject.GetComponent<SphereCollider>().enabled = false;
                            encostou = true;
                        }
                    }
                }
            }
        }
        if (GameObject.Find("Controlador").GetComponent<ControladorVez2>().jogador.transform == /*gameObject.transform.root.gameObject*/ gameObject.transform.parent.IsChildOf(GameObject.Find("Controlador").GetComponent<ControladorVez2>().jogador.transform)) {
            if (ControladorVez2.vezDoJogador == true) {
                if (other.gameObject.tag == "TorsoEt") {
                    if (ControladorVez2.tipoAtk == 0) {
                        if (gameObject.tag == "LMao") {
                            gameObject.GetComponent<SphereCollider>().enabled = false;
                            encostou = true;
                        }
                    } else if (ControladorVez2.tipoAtk == 1) {
                      //  print("ataque2");
                        if (gameObject.tag == "RMao") {
                            gameObject.GetComponent<SphereCollider>().enabled = false;
                            encostou = true;
                        }
                    } else if (ControladorVez2.tipoAtk == 3) {
                     //   print("ataque3");
                        if (gameObject.tag == "LMao" || gameObject.name == "RMao") {
                            gameObject.GetComponent<SphereCollider>().enabled = false;
                            encostou = true;
                        }
                    }
                }
            }
        }
    }
}


