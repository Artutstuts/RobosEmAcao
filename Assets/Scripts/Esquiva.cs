using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esquiva : MonoBehaviour {

    public static bool esquivar = false;
    public static bool defender = false;

    public void AtivarEsquiva() {
        esquivar = true;
        if((ControladorVez2.danoDobradoI == true || ControladorVez2.danoDobradoJ == true || ControladorVez2.danoMetadeI == true || ControladorVez2.danoMetadeJ == true) && (ControladorVez2.name == "AtaqueEspecial"|| ControladorVez2.name2 == "AtaqueEspecial")) {
                defender = true;
            print(defender);
        }
    }
}
