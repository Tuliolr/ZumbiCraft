﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour {

    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoPontuacaoMax;
    private float textoPontuacaoSalvo;
    private int quantidadeeZumbisMortos;
    public Text TextoQuantidadeDeZumbisMortos;
    public Text TextoChefeAparece;

	// Use this for initialization
	void Start () {
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        textoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMax");

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void AtualizarQuantidadeDeZumbisMortos()
    {
        quantidadeeZumbisMortos++;
        TextoQuantidadeDeZumbisMortos.text = string.Format("x {0}", quantidadeeZumbisMortos);
    }

    public void GameOver()
    {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        TextoTempoDeSobrevivencia.text = "Você sobreviveu por " + minutos + "m e "+ segundos + "s." ;

        AjustaPontuacaoMax(minutos,segundos);

    }

    void AjustaPontuacaoMax(int min,int seg)
    {
        if(Time.timeSinceLevelLoad > textoPontuacaoSalvo)
        {
            textoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMax.text = string.Format("Seu melhor tempo é {0}m e {1}s.", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMax", textoPontuacaoSalvo);
        }
        if(TextoPontuacaoMax.text == "")
        {
            min = (int)textoPontuacaoSalvo / 60;
            seg = (int)textoPontuacaoSalvo % 60;
            TextoPontuacaoMax.text = string.Format("Seu melhor tempo é {0}m e {1}s.", min, seg);
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Game");
    }

    public void AparecerTextoChefeCriado()
    {
        StartCoroutine(DesaparecerTexto(1, TextoChefeAparece));
    }

    IEnumerator DesaparecerTexto (float tempodeSumico, Text textoParaSumir)
    {
        TextoChefeAparece.gameObject.SetActive(true);
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        while(textoParaSumir.color.a > 0)
        {
            contador += Time.deltaTime / tempodeSumico;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;
            if (textoParaSumir.color.a <= 0)
            {
                TextoChefeAparece.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
