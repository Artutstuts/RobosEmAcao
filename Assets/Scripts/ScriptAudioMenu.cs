using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAudioMenu : MonoBehaviour {

    AudioSource aS;
    public AudioClip loop;
    public AudioClip inicio;

    void Start() {
        aS = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        float progress = Mathf.Clamp01(aS.time / aS.clip.length);
        if(progress == 1) {
            print("SDSA");
            if(aS.clip == inicio) {
                aS.clip = loop;
                aS.loop = true;
                aS.Play();
            }
        }

    }
}
