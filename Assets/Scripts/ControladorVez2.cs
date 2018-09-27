using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorVez2 : MonoBehaviour {

	public GameObject jogador;
	public GameObject inimigo;
	int jDesviou = 0;
	int iDesviou = 0;
	int jErrou = 0;
	int iErrou = 0;
	Animator animJ;
	Animator animI;
    public static bool vezDoJogador;
    public static bool vezDoInimigo;
	bool vezJogador = false;
	bool funcRodando = false;
	bool animJRodando;
	bool verificAnims;
	bool animIRodando;
	bool duasAnim;
	string name;
	Slider slider;
    Slider sliderJ;
    Slider[] vidas;
	Animator anim;
	string name2;
    public static int tipoAtk = 4;
    bool gameOver;


	// Use this for initialization
	IEnumerator Start () {
        gameOver = false;
		animJ = jogador.GetComponent<Animator> ();
		animI = inimigo.GetComponent<Animator> ();
		yield return new WaitUntil(()=>DefaultTrackableEventHandler.qrCodeAtivado == true);
        vidas = FindObjectsOfType<Slider>();
        foreach (Slider s in vidas)
        {
            if(s.tag == "inimigo")
            {
                slider = s;
            }
            else
            {
                sliderJ = s;
            }
        }
        // slider = FindObjectOfType<Slider> ();
		slider.GetComponent<Slider> ();
		slider.maxValue = inimigo.GetComponent<Classes> ().maxHp;
		slider.value = inimigo.GetComponent<Classes> ().hp;
        sliderJ.GetComponent<Slider>();
        sliderJ.maxValue = jogador.GetComponent<Classes>().maxHp;
        sliderJ.value = jogador.GetComponent<Classes>().hp;
        Quemcomeca();
		verificAnims = true;

	}


	// Update is called once per frame
	void Update () {
        if (DefaultTrackableEventHandler.qrCodeAtivado == true) {
            if (verificAnims == true) {
                if (vezJogador == true) {
                    if (funcRodando == false) {
                        StartCoroutine(JogadorVez());
                    }
                } else {
                    if (funcRodando == false) {
                        StartCoroutine(InimigoVez());
                    }
                }
            }

            if (verificAnims == true) {
                AnimatorIsPlayingI(animI, name);
                AnimatorIsPlayingJ(animJ, name2);
            }
            if (gameOver == true) {
                if (Input.anyKeyDown == true) {
                    SceneManager.LoadScene("ARLatinha");
                }
            }
        }
	}

	private void Quemcomeca() {
		int n = UnityEngine.Random.Range(0, 2);
		//print(n);
		if (n == 0) {
			vezJogador = true;
		} else {
			vezJogador = false;
		}
	}

	IEnumerator InimigoVez() {
        vezDoInimigo = true;
		funcRodando = true;
		int cAtk = UnityEngine.Random.Range(1, 101);
		//print(cAtk);
		if (cAtk > inimigo.GetComponent<Classes>().chanceAtk) {
			iErrou += 1;
            vezDoInimigo = false;
            yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
            StartCoroutine(JogadorVez());
        } else {
			//print("Inimigo Vai Atacar");
			int desviou = UnityEngine.Random.Range(1, 101);
			if (desviou <= jogador.GetComponent<Classes>().evasao) {
				//print("Jogador Desviou O Ataque");
				jDesviou += 1;
                tipoAtk = 0;
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                animI.SetTrigger("Ataque1");
                name = "Ataque1";
                anim = animI;
                /*	tipoAtk = UnityEngine.Random.Range (0, 2);
                    if (tipoAtk == 0) {
                        animI.SetTrigger ("Ataque1");
                        name = "Ataque1";
                        anim = animI;
                    } else if(tipoAtk == 1){
                        animI.SetTrigger ("Ataque2");
                        name = "Ataque2";
                        anim = animI;
                    }
                    */
                yield return new WaitUntil(() => Esquiva.esquivar == true);
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                animJ.SetTrigger("Esquiva");
				name2 = "Esquiva";
				duasAnim = true;
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                StartCoroutine (AnimRodando(animJ, "Jogador"));
			} else {
				//print("Inimigo Acertou O Ataque");
				//print("Vida Jogador = " + jogador.GetComponent<Classes>().hp);
				jogador.GetComponent<Classes>().hp -= inimigo.GetComponent<Classes>().dano;
				if (jogador.GetComponent<Classes> ().hp > 0) {
					tipoAtk = UnityEngine.Random.Range (0, 2);
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    if (tipoAtk == 0) {
						animI.SetTrigger ("Ataque1");
						name = "Ataque1";
						anim = animI;
					} else if(tipoAtk == 1){
						animI.SetTrigger ("Ataque2");
						name = "Ataque2";
						anim = animI;
					}
				} else {
                    tipoAtk = 3;
					animI.SetTrigger ("AtaqueEspecial");
					name = "AtaqueEspecial";
					anim = animI;
				}
                //if on trigger play Tomou dano do jogador
                 yield return new WaitWhile(() => DetectorDeHit.encostou == false);
                sliderJ.value = jogador.GetComponent<Classes>().hp;
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                animJ.SetTrigger("Dano");
				name2 = "Dano";
				duasAnim = true;
				print("Vida Jogador = " + jogador.GetComponent<Classes>().hp);
				if (jogador.GetComponent<Classes>().hp <= 0) {
					//play Derrota jogador					
					StartCoroutine(AnimRodando(animI,"ninguem"));
					print("Game Over");
					print("Vida Inimigo = " + inimigo.GetComponent<Classes>().hp);
					print("Vida Jogador = " + jogador.GetComponent<Classes>().hp);
					print("Inimigo Venceu");
                    gameOver = true;
                    DetectorDeHit.mao.Clear();
                } else {
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    StartCoroutine (AnimRodando(animI,"Jogador"));
				}
			}
		}
	}

	 IEnumerator JogadorVez() {
        vezDoJogador = true;
		funcRodando = true;
		int cAtk = UnityEngine.Random.Range(1, 101);
		//print(cAtk);
		if (cAtk > jogador.GetComponent<Classes>().chanceAtk) {
			jErrou += 1;
            vezDoJogador = false;
            yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
            StartCoroutine(InimigoVez());
		} else {
			//print("Jogador Vai Atacar");
			int desviou = UnityEngine.Random.Range(1, 101);
			if(desviou <= inimigo.GetComponent<Classes>().evasao) {
				//print("Jogador Errou O Ataque");
				iDesviou += 1;
                tipoAtk = 0;
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                animJ.SetTrigger("Ataque1");
                name = "Ataque1";
                anim = animJ;
                /*	tipoAtk = UnityEngine.Random.Range (0, 2);
                    if (tipoAtk == 0) {
                        animJ.SetTrigger ("Ataque1");
                        name = "Ataque1";
                        anim = animJ;
                    } else if(tipoAtk == 1){
                        animJ.SetTrigger ("Ataque2");
                        name = "Ataque2";
                        anim = animJ;
                    }
                    */
                yield return new WaitUntil(()=> Esquiva.esquivar == true);
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                animI.SetTrigger("Esquiva");
				name = "Esquiva";
				anim = animI;
				duasAnim = true;
                Esquiva.esquivar = false;
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                StartCoroutine (AnimRodando (animI,"Inimigo"));
			} else {
				//print("Jogador Acertou O Ataque");
				//print("Vida Inimigo = " + inimigo.GetComponent<Classes>().hp);
				inimigo.GetComponent<Classes>().hp -= jogador.GetComponent<Classes>().dano;
				if (inimigo.GetComponent<Classes> ().hp > 0) {
					tipoAtk = UnityEngine.Random.Range (0, 2);
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    if (tipoAtk == 0) {
						animJ.SetTrigger ("Ataque1");
						name2 = "Ataque1";
						anim = animJ;
					} else if (tipoAtk == 1) {
						animJ.SetTrigger ("Ataque2");
						name2 = "Ataque2";
						anim = animJ;
                    }
				} else {
                    tipoAtk = 3;
					animJ.SetTrigger ("AtaqueEspecial");
					name2 = "AtaqueEspecial";
					anim = animJ;
                }
                yield return new WaitWhile(() => DetectorDeHit.encostou == false);
               // yield return new WaitForSecondsRealtime(1.5f);
                slider.value = inimigo.GetComponent<Classes> ().hp;
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                animI.SetTrigger("Dano");
                name = "Dano";
				duasAnim = true;
				print("Vida Inimigo = " + inimigo.GetComponent<Classes>().hp);
				if (inimigo.GetComponent<Classes>().hp <= 0) {
					//play Derrota inimigo
					StartCoroutine(AnimRodando(animI,"ninguem"));
					print("Game Over");
					print("Vida Jogador = " + jogador.GetComponent<Classes>().hp);
					print("Vida Inimigo = " + inimigo.GetComponent<Classes>().hp);
					print("Jogador Venceu");
                    gameOver = true;
                    DetectorDeHit.mao.Clear();
				} else {
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    StartCoroutine (AnimRodando (animJ,"Inimigo"));
				}
			}
		}

	}
		
	IEnumerator AnimRodando(Animator anim,string vezDeQuem){
		verificAnims = true;
		yield return new WaitForSeconds (0.3f);
		//print("FDJSIJF");
        if(name == "Dano"|| name == "Esquiva"){
            yield return new WaitWhile(() => animIRodando == true);
        }
        else if(name2 == "Dano"|| name2 == "Esquiva"){
            yield return new WaitWhile(() => animJRodando == true);
        }
       // print(animJRodando);
        //print(animIRodando);
		verificAnims = false;
		//print("DADADA");
		 if(duasAnim == true) {
			duasAnim = false;
			float porcI = (inimigo.GetComponent<Classes> ().hp / inimigo.GetComponent<Classes> ().maxHp) * 100;
			float porcJ = (jogador.GetComponent<Classes> ().hp / jogador.GetComponent<Classes> ().maxHp) * 100;
            if (name == "Dano" || name == "Esquiva")
            {
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                if (porcI > 50)
                {
                    animI.SetTrigger("Idle");
                }
                else if (porcI > 25 && porcI < 51)
                {
                    animI.SetTrigger("IdleMach");
                    
                    GameObject[] faiscas = GameObject.FindGameObjectsWithTag("faiscaMenor");
                    foreach (GameObject f in faiscas) {
                        if (f.transform.IsChildOf(inimigo.transform) == true) {
                            f.GetComponent<ParticleSystem>().Play();
                        }
                    }
                }
                else if (porcI < 26 && porcI > 0)
                {
                    animI.SetTrigger("IdleMtMach");
                    GameObject[] faiscasMenor = GameObject.FindGameObjectsWithTag("faiscaMenor");
                    foreach (GameObject f in faiscasMenor) {
                        if (f.transform.IsChildOf(inimigo.transform) == true) {
                            f.GetComponent<ParticleSystem>().Stop();
                        }
                    }
                    GameObject[] faiscas = GameObject.FindGameObjectsWithTag("faisca");
                    foreach (GameObject f in faiscas) {
                        if (f.transform.IsChildOf(inimigo.transform) == true) {
                            f.GetComponent<ParticleSystem>().Play();
                        }
                    }
                }
                else
                {
                    animI.SetTrigger("Derrota");
                }
                verificAnims = true;
                yield return new WaitWhile(() => animJRodando == true);
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                verificAnims = false;
                if (porcJ > 50)
                {
                    animJ.SetTrigger("Idle");
                }
                else if (porcJ > 25 && porcJ < 51)
                {

                    animJ.SetTrigger("IdleMach");
                    print("DADAD!");
                    GameObject[] faiscasMenor = GameObject.FindGameObjectsWithTag("faiscaMenor");
                    foreach (GameObject f in faiscasMenor) {
                        if (f.transform.IsChildOf(jogador.transform) == true) {
                            f.GetComponent<ParticleSystem>().Play();
                        }
                    }
                }
                else if (porcJ < 26 && porcJ > 0)
                {
                    animJ.SetTrigger("IdleMtMach");

                    GameObject[] faiscasMenor = GameObject.FindGameObjectsWithTag("faiscaMenor");
                    foreach (GameObject f in faiscasMenor) {
                        if (f.transform.IsChildOf(jogador.transform) == true) {
                            f.GetComponent<ParticleSystem>().Stop();
                        }
                    }
                    GameObject[] faiscas = GameObject.FindGameObjectsWithTag("faisca");
                    foreach (GameObject f in faiscas) {
                        if (f.transform.IsChildOf(jogador.transform) == true) {
                            f.GetComponent<ParticleSystem>().Play();
                        }
                    }
                }
                else
                {
                    animJ.SetTrigger("Derrota");
                }
                DetectorDeHit.encostou = false;
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                foreach (SphereCollider m in DetectorDeHit.mao) {
                    if (m.enabled == false) {
                        m.enabled = true;
                    }
                }
                tipoAtk = 4;
                yield return new WaitForSecondsRealtime(0.5f);
                if (vezDeQuem == "Inimigo")
                {
                    print(DetectorDeHit.encostou);
                    vezDoInimigo = false;
                    vezDoJogador = false;
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    StartCoroutine(InimigoVez());
                }
                else if (vezDeQuem == "Jogador")
                {
                    print(DetectorDeHit.encostou);
                    vezDoInimigo = false;
                    vezDoJogador = false;
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    StartCoroutine(JogadorVez());
                }
            }
            else if (name2 == "Dano" || name2 == "Esquiva")
            {
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                if (porcJ > 50)
                {
                    animJ.SetTrigger("Idle");
                }
                else if (porcJ > 25 && porcJ < 51)
                {
                    animJ.SetTrigger("IdleMach");
                    GameObject[] faiscasMenor = GameObject.FindGameObjectsWithTag("faiscaMenor");
                    foreach (GameObject f in faiscasMenor) {
                        if (f.transform.IsChildOf(jogador.transform) == true) {
                            f.GetComponent<ParticleSystem>().Play();
                        }
                    }
                }
                else if (porcJ < 26 && porcJ > 0)
                {
                    animJ.SetTrigger("IdleMtMach");
                    GameObject[] faiscasMenor = GameObject.FindGameObjectsWithTag("faiscaMenor");
                    foreach (GameObject f in faiscasMenor) {
                        if (f.transform.IsChildOf(jogador.transform) == true) {
                            f.GetComponent<ParticleSystem>().Stop();
                        }
                    }
                    GameObject[] faiscas = GameObject.FindGameObjectsWithTag("faisca");
                    foreach (GameObject f in faiscas) {
                        if (f.transform.IsChildOf(jogador.transform) == true) {
                            f.GetComponent<ParticleSystem>().Play();
                        }
                    }
                }
                else
                {
                    animJ.SetTrigger("Derrota");
                }
                verificAnims = true;
                yield return new WaitWhile(() => animIRodando == true);
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                verificAnims = false;
                if (porcI > 50)
                {
                    animI.SetTrigger("Idle");
                }
                else if (porcI > 25 && porcI < 51)
                {
                    animI.SetTrigger("IdleMach");
                    GameObject[] faiscasMenor = GameObject.FindGameObjectsWithTag("faiscaMenor");
                    foreach (GameObject f in faiscasMenor) {
                        if (f.transform.IsChildOf(inimigo.transform) == true) {
                            f.GetComponent<ParticleSystem>().Play();
                        }
                    }

                }
                else if (porcI < 26 && porcI > 0)
                {
                    animI.SetTrigger("IdleMtMach");
                    GameObject[] faiscasMenor = GameObject.FindGameObjectsWithTag("faiscaMenor");
                    foreach (GameObject f in faiscasMenor) {
                        if (f.transform.IsChildOf(inimigo.transform) == true) {
                            f.GetComponent<ParticleSystem>().Stop();
                        }
                    }
                    GameObject[] faiscas = GameObject.FindGameObjectsWithTag("faisca");
                    foreach (GameObject f in faiscas) {
                        if (f.transform.IsChildOf(inimigo.transform) == true) {
                            f.GetComponent<ParticleSystem>().Play();
                        }
                    }
                }
                else
                {
                    animI.SetTrigger("Derrota");
                }
                DetectorDeHit.encostou = false;
                yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                foreach (SphereCollider m in DetectorDeHit.mao) {
                    if(m.enabled == false) {
                        m.enabled = true;
                    }
                }
                tipoAtk = 4;
                yield return new WaitForSecondsRealtime(0.5f);
                if (vezDeQuem == "Inimigo")
                {
                    print(DetectorDeHit.encostou);
                    vezDoInimigo = false;
                    vezDoJogador = false;
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    StartCoroutine(InimigoVez());
                }
                else if (vezDeQuem == "Jogador")
                {
                    vezDoInimigo = false;
                    vezDoJogador = false;
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    StartCoroutine(JogadorVez());
                }
            }
		}
	}

	void AnimatorIsPlayingI(Animator anim, string nome){
		if (anim.GetCurrentAnimatorStateInfo (0).IsName(nome) == true && anim.GetCurrentAnimatorStateInfo(0).normalizedTime <1 /*&& !anim.IsInTransition(0)*/) {
			animIRodando = true;
		} else {
			animIRodando = false;
		}
	}
	void AnimatorIsPlayingJ(Animator anim, string nome){
		if (anim.GetCurrentAnimatorStateInfo (0).IsName(nome) == true && anim.GetCurrentAnimatorStateInfo(0).normalizedTime <1 /*&& !anim.IsInTransition(0)*/) {
			animJRodando = true;
		} else {
			animJRodando = false;
		}
	}



}
