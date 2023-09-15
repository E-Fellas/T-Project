using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Variaveis para IA de perseguição
    public NavMeshAgent agente;
    public Transform player;
    public LayerMask camadaChao, camadaPlayer;

    // Patrulha
    public Transform[] pontos;
    private int destPonto = 0;
    private bool pontoSetado;

    // Estados
    public float alcanceVisao;
    public bool playerEmAlcanceVisao;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agente = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Veridica se player esta no alcance de visao
        playerEmAlcanceVisao = Physics.CheckSphere(transform.position, alcanceVisao, camadaPlayer);

        if (!playerEmAlcanceVisao) Patrulha();
        if (playerEmAlcanceVisao) SeguirPlayer();
    }

    private void Patrulha()
    {
        if (!pontoSetado)
        {
            // Seta o destino para o proximo ponto
            agente.destination = pontos[destPonto].position;
            pontoSetado = true;
        }

        // Ao chegar em um ponto procura o proximo
        if (!agente.pathPending && agente.remainingDistance < 0.5f)
            ProximoPonto();
            
    }

    private void ProximoPonto()
    {
        // Se lista de pontos estiver vazia retorna
        if (pontos.Length == 0) return;

        pontoSetado = false;

        // Pega o proximo ponto, retornando ao inicio quando chegar no fim da lista
        destPonto = (destPonto + 1) % pontos.Length;
    }

    private void SeguirPlayer()
    {
        if (pontoSetado) pontoSetado = false;
        agente.SetDestination(player.position);
    }
}
