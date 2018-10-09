using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulaET : MonoBehaviour {

	public void Particulas(int i) {
        if (i == 1) {
            GameObject.Find("Gas_Venenoso").GetComponent<ParticleSystem>().Play();
        } if(i==0) {
            GameObject.Find("Gas_Venenoso").GetComponent<ParticleSystem>().Stop();
        }
        if (i == 3) {
            GameObject.Find("Fumaca").GetComponent<ParticleSystem>().Play();
            GameObject.Find("Robo Ladino_ET").GetComponent<Animator>().SetTrigger("TESTE");

        }
        if (i == 4) {
            GameObject.Find("Fumaca").GetComponent<ParticleSystem>().Stop();
        }
    }
}
