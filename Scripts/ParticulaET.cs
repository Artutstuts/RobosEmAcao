using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulaET : MonoBehaviour {

	public void Particulas(int i) {
        if (i == 1) {
            GameObject.Find("Gas_Venenoso").GetComponent<ParticleSystem>().Play();
        } else {
            GameObject.Find("Gas_Venenoso").GetComponent<ParticleSystem>().Stop();
        }
    }
}
