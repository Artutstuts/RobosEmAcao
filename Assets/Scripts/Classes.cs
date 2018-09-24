using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classes : MonoBehaviour {

	internal float hp;
	internal float dano;
    internal int evasao;
    internal int chanceAtk;
	internal float maxHp;


    // Use this for initialization
    void Start () {
        Estatisticas();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Estatisticas() {
        if (gameObject.tag == "Guerreiro") {
			maxHp = 100;
            hp = 100;
            dano = 10;
            evasao = 10;
            chanceAtk = 50;
        }
        if (gameObject.tag == "Tanque") {
			maxHp = 160;
            hp = 160;
            dano = 12;
            evasao = 5;
            chanceAtk = 30;
        }
        if (gameObject.tag == "Ladino") {
			maxHp = 100;
            hp = 100;
            dano = 8;
            evasao = 15;
            chanceAtk = 70;
        }

    }


}
