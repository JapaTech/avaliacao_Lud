using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveControlador : MonoBehaviour
{
    //Componete Rigibody do objeto
    Rigidbody _rb;
    //Vector para calcular a velocidade da nave
    Vector3 _velocidade;
    //Animation curve para controlar o crescimento da velocidade
    [SerializeField] AnimationCurve _ac;
    //Intervalo em que a velocidade vai aumentantodo
    float _tempo;
    //Multiplicador para mudar a velocidade do objetos
    float multiplicador;

    void Start()
    {
        _tempo = 0;
        _rb = GetComponent<Rigidbody>();
        multiplicador = Random.Range(1, 4);
    }

    private void FixedUpdate()
    {
        MoveNave();
    }

    //Ações tomadas quando o componente vem da pool
    public void Inicializa()
    {
        gameObject.SetActive(true);
        _tempo = 0;
        multiplicador = Random.Range(1, 4);
    }

    //Função para mover a nave
    private void MoveNave()
    {
        //Calcula a velocidade da nave, multiplicando um valor que vai aumentando ao longo do tempo
        _velocidade.x = multiplicador * _ac.Evaluate(_tempo);
        //Aplica a velocidade no rigibody
        _rb.velocity = _velocidade;
        //Aumenta o tempo
        _tempo += Time.fixedDeltaTime;
    }
}
