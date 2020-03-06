using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorBoss : MonoBehaviour {

    private float tempoParaProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    public GameObject chefePrefab;
    private ControlaInterface scriptControlaInteface;
    public Transform[] PosicoesPossiveisDeGeracao;
    private Transform jogador;

	
	void Start () {

        tempoParaProximaGeracao = tempoEntreGeracoes;
        scriptControlaInteface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("Jogador").transform;
	}
	
	// Update is called once per frame
	void Update () {

		if(Time.timeSinceLevelLoad > tempoParaProximaGeracao)
        {
            Vector3 posicaoDeCriacao = CalculaPosicaoMaisDistanteDoJogador();
            Instantiate(chefePrefab, posicaoDeCriacao, Quaternion.identity);
            scriptControlaInteface.AparecerTextoChefeCriado();
            tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
	}

    Vector3 CalculaPosicaoMaisDistanteDoJogador()
    {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;

        foreach(Transform posicao in PosicoesPossiveisDeGeracao)
        {
            float distanciaEntreJogador = Vector3.Distance(posicao.position, jogador.position);
            if(distanciaEntreJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }
        }

        return posicaoDeMaiorDistancia;
    }
}
