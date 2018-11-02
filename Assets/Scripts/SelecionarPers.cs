using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecionarPers : MonoBehaviour {

    public static bool guer = true;
    public static bool tank = false;
    public static bool ladi = false;

    public void PersonagemSelect() {
        if(gameObject.name == "Guerreiro") {
            guer = true;
            tank = false;
            ladi = false;
        }
        if (gameObject.name == "Tanque") {
            tank = true;
            guer = false;
            ladi = false;
        }
        if (gameObject.name == "Ladino") {
            ladi = true;
            guer = false;
            tank = false;
        }
    }












}
