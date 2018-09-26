using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IniciarJogo : MonoBehaviour {


    public void LoadCena(){
        SceneManager.LoadScene("ARLatinha");
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

}
