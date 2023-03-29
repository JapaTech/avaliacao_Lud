using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    //Refer�ncia para o gerenciador de spawn
    [SerializeField] GerenciadorSpawn _gerenciadorSpawn;

    //Refer�ncia para o texto que mostra a quantidade total de naves criadas
    [SerializeField] TMP_Text qtdNavesCriadas_txt;
    //Referencia para o texto que mostra a pr�xima quantidade de naves spawandas
    [SerializeField] TMP_Text ProximoSpawn_txt;

    void LateUpdate()
    {
        MostraTotalDeNaves();
        MostraQuantidadeProximoSpawn();
    }

    //Atualiza o texto com a quantidae total de naves spawanados
    private void MostraTotalDeNaves()
    {
        qtdNavesCriadas_txt.text = $"Naves criadas: {_gerenciadorSpawn.QtdInimigosSpwanados}";
    }

    //Atualiza o texto com a pr�xima quantidae total de naves que ser�o spwanadas
    private void MostraQuantidadeProximoSpawn()
    {
        if (_gerenciadorSpawn.Contador <= _gerenciadorSpawn.Fibonacci_N)
        {
            ProximoSpawn_txt.text = $"Pr�ximo spawn: {Fibonacci.RetornaFibonacci(_gerenciadorSpawn.Contador)}";

        }
        else
        {
            ProximoSpawn_txt.text = "�ltimo spawn";
        }
    }
}
