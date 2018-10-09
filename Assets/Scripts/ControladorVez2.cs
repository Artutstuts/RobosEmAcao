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
	public static string name;
	Slider slider;
    Slider sliderJ;
    Slider[] vidas;
	Animator anim;
	public static string name2;
    public static int tipoAtk = 4;
    bool gameOver;
    public GameObject qTE;
    float x;
    float y;
    bool jaRodou = false;
    bool acertouBotao = false;
    public SpriteState sprstatATK;
    public SpriteState sprstatDEF;
    public Sprite sprtDEF;
    bool cheio = false;
    bool qTEOcorrendo = false;
    public Sprite sprtATK;
    public Slider SQTE;
    public Sprite sprtNEU;
    public static bool danoDobradoJ = false;
    public static bool danoDobradoI = false;
    public static bool danoMetadeJ = false;
    public static bool danoMetadeI = false;
    public GameObject hab1Botao;
    public GameObject hab2Botao;
    public GameObject hab3Botao;
    float pontoDeHab = 0;
    bool hab1;
    bool hab2;
    bool hab3;


    // Use this for initialization
    IEnumerator Start () {

        hab1Botao.SetActive(false);
        hab2Botao.SetActive(false);
        hab3Botao.SetActive(false);


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
            HabilidadeFillTeste();
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
            if(pontoDeHab >= 2) {
                hab1Botao.SetActive(true);
                if (pontoDeHab >= 4) {
                    hab2Botao.SetActive(true);
                    if(pontoDeHab == 6) {
                        hab3Botao.SetActive(true);
                    }
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
                if (pontoDeHab < 6) {
                    pontoDeHab++;
                }
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
                    int chnace = Random.Range(0, 101);
                    if (chnace < 40) {
                        if (jaRodou == false) {
                            StartCoroutine(QuickTimeEventDEMODAY());
                        }
                    }

                    if (danoDobradoI == false && danoMetadeI == false) {
                        if (tipoAtk == 0) {
                            animI.SetTrigger("Ataque1");
                            name = "Ataque1";
                            anim = animI;
                        } else if (tipoAtk == 1) {
                            animI.SetTrigger("Ataque2");
                            name = "Ataque2";
                            anim = animI;
                        }
                    } else if (danoDobradoI == true || danoMetadeI == true) {
                        tipoAtk = 3;
                        animI.SetTrigger("AtaqueEspecial");
                        name = "AtaqueEspecial";
                        anim = animI;
                    }
                } else {
                    tipoAtk = 3;
					animI.SetTrigger ("AtaqueEspecial");
					name = "AtaqueEspecial";
					anim = animI;
                    animI.SetBool("Vitoria", true);
                }
                // print("BBBB");
                if ((danoDobradoI == false && danoMetadeI == false) || (danoDobradoI == true && danoMetadeI == false)) {
                    if (danoDobradoI == true) {
                        jogador.GetComponent<Classes>().hp -= inimigo.GetComponent<Classes>().dano;
                        danoDobradoI = false;
                    }
                    yield return new WaitWhile(() => DetectorDeHit.encostou == false);
                    // print("AAAA");
                    
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    animJ.SetTrigger("Dano");
                    name2 = "Dano";
                    duasAnim = true;
                    sliderJ.value = jogador.GetComponent<Classes>().hp;
                } else if(danoMetadeI == true){
                    yield return new WaitUntil(() => Esquiva.defender == true);
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    int r = Random.Range(0, 2);
                    if(r == 0) {
                        animJ.SetTrigger("Defesa1");
                        name2 = "Defesa1";
                        duasAnim = true;
                        jogador.GetComponent<Classes>().hp -= (inimigo.GetComponent<Classes>().dano / 2);
                        sliderJ.value = jogador.GetComponent<Classes>().hp;
                        danoMetadeI = false;
                        Esquiva.defender = false;
                    } else {
                        animJ.SetTrigger("Defesa2");
                        name2 = "Defesa2";
                        duasAnim = true;
                        jogador.GetComponent<Classes>().hp -= (inimigo.GetComponent<Classes>().dano / 2);
                        sliderJ.value = jogador.GetComponent<Classes>().hp;
                        danoMetadeI = false;
                        Esquiva.defender = false;
                    }
                }
				//print("Vida Jogador = " + jogador.GetComponent<Classes>().hp);
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
                if (inimigo.GetComponent<Classes>().hp > 0) {
                    tipoAtk = UnityEngine.Random.Range(0, 2);
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    int chnace = Random.Range(0, 101);
                    if (chnace < 40) {
                        if (jaRodou == false) {
                            StartCoroutine(QuickTimeEventDEMODAY());
                        }
                    }
                    if ((danoDobradoJ == false && danoMetadeJ == false)) {
                        if (hab1 == true) {
                            animJ.SetTrigger("Habilidade1");
                            name2 = "Habilidade1";
                            anim = animJ;
                        } else if (hab2 == true) {
                            animJ.SetTrigger("Habilidade2");
                            name2 = "Habilidade2";
                            anim = animJ;
                        } else if (hab3 == true) {
                            animJ.SetTrigger("Habilidade3");
                            name2 = "Habilidade3";
                            anim = animJ;
                        } else {
                            if (tipoAtk == 0) {
                                animJ.SetTrigger("Ataque1");
                                name2 = "Ataque1";
                                anim = animJ;
                                if (pontoDeHab < 6) {
                                    pontoDeHab++;
                                }
                            } else if (tipoAtk == 1) {
                                animJ.SetTrigger("Ataque2");
                                name2 = "Ataque2";
                                anim = animJ;
                                if (pontoDeHab < 6) {
                                    pontoDeHab++;
                                }
                            }
                        }
                    } else if ((danoDobradoJ == true || danoMetadeJ == true) && (hab1 == false || hab2 == false || hab3 == false)) {
                        animJ.SetTrigger("AtaqueEspecial");
                        name2 = "AtaqueEspecial";
                        anim = animJ;
                        print(Esquiva.defender);
                    } else if ((danoDobradoJ == false && danoMetadeJ == false) || (hab1 == true || hab2 == true || hab3 == true)) {
                        if (hab1 == true) {
                            animJ.SetTrigger("Habilidade1");
                            name2 = "Habilidade1";
                            anim = animJ;
                        } else if (hab2 == true) {
                            animJ.SetTrigger("Habilidade2");
                            name2 = "Habilidade2";
                            anim = animJ;
                        } else if (hab3 == true) {
                            animJ.SetTrigger("Habilidade3");
                            name2 = "Habilidade3";
                            anim = animJ;
                        }
                    }

                    //ESPECIFICO PARA A DEMODAY
                    if ((danoDobradoJ == false && danoMetadeJ == false) || (danoDobradoJ == true && danoMetadeJ == false)) {
                        if (danoDobradoJ == true) {
                            inimigo.GetComponent<Classes>().hp -= jogador.GetComponent<Classes>().dano;
                            danoDobradoJ = false;
                        }
                        if (hab1 == true) {
                            inimigo.GetComponent<Classes>().hp += jogador.GetComponent<Classes>().dano / 2;
                            jogador.GetComponent<Classes>().hp += jogador.GetComponent<Classes>().dano / 2;
                            sliderJ.value = jogador.GetComponent<Classes>().hp;
                            hab1 = false;
                        }
                        if (hab2 == true) {
                            inimigo.GetComponent<Classes>().hp -= jogador.GetComponent<Classes>().dano;
                            hab2 = false;
                        }
                        if (hab3 == true) {
                            inimigo.GetComponent<Classes>().hp -= (jogador.GetComponent<Classes>().dano * 2);
                            hab3 = false;
                        }
                        yield return new WaitWhile(() => DetectorDeHit.encostou == false);
                        // yield return new WaitForSecondsRealtime(1.5f);

                        yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                        //  inimigo.GetComponent<Classes>().hp -= jogador.GetComponent<Classes>().dano;
                        animI.SetTrigger("Dano");
                        name = "Dano";
                        duasAnim = true;

                        slider.value = inimigo.GetComponent<Classes>().hp;
                    } else if (danoMetadeJ == true) {
                        yield return new WaitUntil(() => Esquiva.defender == true);
                        yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                        int r = Random.Range(0, 2);
                        if (r == 0) {
                            //  inimigo.GetComponent<Classes>().hp -= jogador.GetComponent<Classes>().dano;
                            animI.SetTrigger("Defesa1");
                            name = "Defesa1";
                            duasAnim = true;
                            inimigo.GetComponent<Classes>().hp -= (jogador.GetComponent<Classes>().dano / 2);
                            slider.value = inimigo.GetComponent<Classes>().hp;
                            danoMetadeJ = false;
                            Esquiva.defender = false;
                        } else {
                            //   inimigo.GetComponent<Classes>().hp -= jogador.GetComponent<Classes>().dano;
                            animI.SetTrigger("Defesa2");
                            name = "Defesa2";
                            duasAnim = true;
                            inimigo.GetComponent<Classes>().hp -= (jogador.GetComponent<Classes>().dano / 2);
                            slider.value = inimigo.GetComponent<Classes>().hp;
                            danoMetadeJ = false;
                            Esquiva.defender = false;
                        }
                    }
                } else {
                    tipoAtk = 3;
                    animJ.SetTrigger("AtaqueEspecial");
                    name2 = "AtaqueEspecial";
                    anim = animJ;
                    animJ.SetBool("Vitoria", true);
                    yield return new WaitWhile(() => DetectorDeHit.encostou == false);
                    // yield return new WaitForSecondsRealtime(1.5f);

                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    //  inimigo.GetComponent<Classes>().hp -= jogador.GetComponent<Classes>().dano;
                    slider.value = inimigo.GetComponent<Classes>().hp;
                    animI.SetTrigger("Dano");
                    name = "Dano";
                    duasAnim = true;
                }
                //print("Vida Inimigo = " + inimigo.GetComponent<Classes>().hp);
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
        if(name == "Dano"|| name == "Esquiva" || name=="Defesa1" || name=="Defesa2"){
            yield return new WaitWhile(() => animIRodando == true);
        }
        else if(name2 == "Dano"|| name2 == "Esquiva" || name2 == "Defesa1" || name2 == "Defesa2") {
            yield return new WaitWhile(() => animJRodando == true);
        }
        Time.timeScale = 1;
       // print(animJRodando);
        //print(animIRodando);
		verificAnims = false;
		//print("DADADA");
		 if(duasAnim == true) {
			duasAnim = false;
			float porcI = (inimigo.GetComponent<Classes> ().hp / inimigo.GetComponent<Classes> ().maxHp) * 100;
			float porcJ = (jogador.GetComponent<Classes> ().hp / jogador.GetComponent<Classes> ().maxHp) * 100;
            if (name == "Dano" || name == "Esquiva" || name == "Defesa1" || name == "Defesa2")
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
                  //  print("DADAD!");
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
                 //   print(DetectorDeHit.encostou);
                    vezDoInimigo = false;
                    vezDoJogador = false;
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    StartCoroutine(InimigoVez());
                }
                else if (vezDeQuem == "Jogador")
                {
                  //  print(DetectorDeHit.encostou);
                    vezDoInimigo = false;
                    vezDoJogador = false;
                    yield return new WaitUntil(() => DefaultTrackableEventHandler.qrCodeAtivado == true);
                    StartCoroutine(JogadorVez());
                }
            }
            else if (name2 == "Dano" || name2 == "Esquiva" || name2 == "Defesa1" || name2 == "Defesa2")
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
                  //  print(DetectorDeHit.encostou);
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

    IEnumerator QuickTimeEventDEMODAY() {
        yield return new WaitForSecondsRealtime(0.5f);
        y = Random.Range(-150, -5);
        x = Random.Range(-110, 90);
        qTE.transform.localPosition = new Vector3(x, y);
        qTE.SetActive(true);
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(0.8f);
        StartCoroutine(TempoPraDesativar());
        if(qTEOcorrendo == false) {
            ButaoAtkeDefDEMODAY();
        }
        jaRodou = true;
        yield return new WaitWhile(()=> qTEOcorrendo == true);
        if(SQTE.GetComponent<Slider>().value>75 && acertouBotao == true) {
            print("200% DE DANO");
            danoDobradoJ = true;
        } else if (SQTE.GetComponent<Slider>().value > 75 && acertouBotao == false) {
            print("SOFREU 150% DE DANO");
            danoMetadeI = true;
        } else if (SQTE.GetComponent<Slider>().value <75 && acertouBotao == true) {
            print("150% DE DANO");
            danoMetadeJ = true;
        } else if (SQTE.GetComponent<Slider>().value < 75 && acertouBotao == false) {
            print("SOFREU 200% DE DANO");
            danoDobradoI = true;
        }
        acertouBotao = false;
        SQTE.GetComponent<Slider>().value = 0;
        SQTE.gameObject.SetActive(false);
        Time.timeScale = 1;
        GameObject.Find("QuickTimeEventStarter").GetComponent<Image>().sprite = sprtNEU;
        GameObject.Find("QuickTimeEventStarter").GetComponent<Button>().transition = Selectable.Transition.ColorTint;
        qTE.SetActive(false);
        yield return new WaitForSecondsRealtime(20f);
        if(danoDobradoJ == true) {
            yield return new WaitWhile(() => danoDobradoJ == true);
        }else if(danoDobradoI == true) {
            yield return new WaitWhile(() => danoDobradoI == true);
        } else if(danoMetadeI == true) {
            yield return new WaitWhile(() => danoMetadeI == true);
        } else if (danoMetadeJ == true) {
            yield return new WaitWhile(() => danoMetadeJ == true);
        }
        print("ASD");
        jaRodou = false;
        

    }

    public void Clicou() {
        if (qTEOcorrendo == false) {
            acertouBotao = true;
        }
    }
    public void ButaoAtkeDefDEMODAY() {
        SQTE.gameObject.SetActive(true);
        qTEOcorrendo = true;
        if (acertouBotao == true) {
            Time.timeScale = 0.1f;
            GameObject.Find("QuickTimeEventStarter").GetComponent<Image>().sprite = sprtATK;
            GameObject.Find("QuickTimeEventStarter").GetComponent<Button>().transition = Selectable.Transition.SpriteSwap;
            GameObject.Find("QuickTimeEventStarter").GetComponent<Button>().spriteState = sprstatATK;
            qTE.transform.localPosition = new Vector3(300, -360);
            //  qTE.rectTransform.localScale = new Vector3(0.08f, 0.08f);
            GameObject.Find("SliderQTE").GetComponent<Slider>().value += 10;
        } else {
            Time.timeScale = 0.1f;
            GameObject.Find("QuickTimeEventStarter").GetComponent<Image>().sprite = sprtDEF;
            GameObject.Find("QuickTimeEventStarter").GetComponent<Button>().transition = Selectable.Transition.SpriteSwap;
            GameObject.Find("QuickTimeEventStarter").GetComponent<Button>().spriteState = sprstatDEF;
            qTE.transform.localPosition = new Vector3(300, -360);
            //  qTE.rectTransform.localScale = new Vector3(0.08f, 0.08f);
            GameObject.Find("SliderQTE").GetComponent<Slider>().value += 10;
        }
        if(GameObject.Find("SliderQTE").GetComponent<Slider>().value == GameObject.Find("SliderQTE").GetComponent<Slider>().maxValue) {
            cheio = true;
            GameObject.Find("QuickTimeEventStarter").GetComponent<Image>().sprite = sprtNEU;
            GameObject.Find("QuickTimeEventStarter").GetComponent<Button>().transition = Selectable.Transition.ColorTint;
            qTEOcorrendo = false;
            

        }
    }

    IEnumerator TempoPraDesativar() {
        yield return new WaitForSecondsRealtime(4f);
        if (qTEOcorrendo == true) {
            GameObject.Find("QuickTimeEventStarter").GetComponent<Image>().sprite = sprtNEU;
            GameObject.Find("QuickTimeEventStarter").GetComponent<Button>().transition = Selectable.Transition.ColorTint;
            qTEOcorrendo = false;
        }


    }

    public void Habilidade1() {
        hab1 = true;
        pontoDeHab -= 2;
        if (pontoDeHab < 0) {
            pontoDeHab = 0;
        }
        if (pontoDeHab < 6) {
            hab3Botao.SetActive(false);
        }
        if (pontoDeHab < 4) {
            hab2Botao.SetActive(false);
        }
        if (pontoDeHab < 2) {
            hab1Botao.SetActive(false);
        }
        
    }

    public void Habilidade2() {
        hab2 = true;
        pontoDeHab -= 4;
        if (pontoDeHab < 0) {
            pontoDeHab = 0;
        }
        if (pontoDeHab < 6) {
            hab3Botao.SetActive(false);
        }
        if (pontoDeHab < 4) {
            hab2Botao.SetActive(false);
        }
        if (pontoDeHab < 2) {
            hab1Botao.SetActive(false);
        }

    }
    public void Habilidade3() {
        hab3 = true;
        pontoDeHab -= 6;
        if (pontoDeHab < 0) {
            pontoDeHab = 0;
        }
        if (pontoDeHab < 6) {
            hab3Botao.SetActive(false);
        }
        if (pontoDeHab < 4) {
            hab2Botao.SetActive(false);
        }
        if (pontoDeHab < 2) {
            hab1Botao.SetActive(false);
        }

    }

    void HabilidadeFillTeste() {
        float porcHab = pontoDeHab / 6 * 100;
        if (pontoDeHab <= 2) {
            float porcFill = porcHab / 33;
            GameObject.Find("HabilidadeFundo (1)").GetComponent<Image>().fillAmount = porcFill;
            GameObject.Find("HabilidadeFundo (2)").GetComponent<Image>().fillAmount = 0;
            GameObject.Find("HabilidadeFundo (3)").GetComponent<Image>().fillAmount = 0;
        }
        if (pontoDeHab >2 && pontoDeHab <= 4) {
            float porcFill = (porcHab-33) / 33;
            GameObject.Find("HabilidadeFundo (2)").GetComponent<Image>().fillAmount = porcFill;
            GameObject.Find("HabilidadeFundo (3)").GetComponent<Image>().fillAmount = 0;
        }
        if(pontoDeHab >4 && pontoDeHab <=6){
            float porcFill = (porcHab-66) / 33;
            GameObject.Find("HabilidadeFundo (3)").GetComponent<Image>().fillAmount = porcFill;
        }
        
    }



}
