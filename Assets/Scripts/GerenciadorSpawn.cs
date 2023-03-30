using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorSpawn : MonoBehaviour
{
    //Contador de quantas naves t�m
    int _contador = 0;
    //At� que n�mero de vezes s�r� executada
    [SerializeField] int _fibonacci_N = 6;

    [SerializeField] float _intervaloEntreSpawn = 3;

    //Prefab na nave
    [SerializeField] NaveControlador nave;

    //Naves ativas
    List<NaveControlador> naves = new List<NaveControlador>();
    //Naves do pooling
    Queue<NaveControlador> navesPool = new Queue<NaveControlador>();

    bool estaDesativandoInimmigos;

    int _qtdInimigosSpwanados = 0;

    //Propriedades privadas para serem expostas para outras scripts
    public int QtdInimigosSpwanados { get { return _qtdInimigosSpwanados; } }
    public int Fibonacci_N { get { return _fibonacci_N; } }
    public int Contador { get { return _contador; } }
    

    // Start is called before the first frame update
    void Start()
    {
        //Chama o spawn de inimigos a cada 3 segundos
        InvokeRepeating(nameof(ChamaSpawn), 2, _intervaloEntreSpawn);
    }

    private void Update()
    {
        //Cancela o spawn quando ele atingem o valor m�ximo
        if(_contador > _fibonacci_N)
        {
            CancelInvoke();
        }
    }

    //Fun��o que spawna as naves que recebe 
    private void Spawn(int vezes)
    {
        //Cria o compontente com a nave
        NaveControlador n;

        //Se n�o tiver nenhum objeto na pool de naves
        if(navesPool.Count == 0)
        {
            //Cria uma nave nova
            n = Instantiate(nave, new Vector3(-9.8f, Random.Range(-4.15f, 6.01f), 0), nave.transform.rotation);
        }
        //Caso contr�rio pega uma da pool
        else
        {
            n = navesPool.Dequeue();
            n.transform.position = new Vector3(-9.8f, Random.Range(-4.15f, 6.01f), 0);
        }

        //Inicializa as vari�veis necess�rias
        n.Inicializa();
        //Adiciona o objeto na lista de naves ativas
        naves.Add(n);
        //Aumenta a quantidade de inimigos spawanados
        _qtdInimigosSpwanados++;

        //Uso da recursevidade para spawnar um valor N de naves, at� que N seja 0
        vezes--;
        if (vezes > 0)
            Spawn(vezes);
    }

    //Spawna naves passando n�mero fibonnaci para ser spawnado
    private void ChamaSpawn()
    {
        //Verifica se a contagem fibonacci n�o chegou no limite
        if (_contador <= _fibonacci_N)
        {
            //Chama o spawn para passar o valor da contagem do fibonacci atual
            Spawn(Fibonacci.RetornaFibonacci(_contador));
            //Aumente o contador para o pr�ximo spawn
            _contador++;
        }
        //Come�a a corrotiva para desativar inimigos
        StartCoroutine(DesativaInimigos());
    }

    //Corrotiva para desativar inimigos a cada 1 segundo
    IEnumerator DesativaInimigos()
    {
        //Flag para garantir qu� s� essa corrotina est� ativa
        if (!estaDesativandoInimmigos)
        {
            estaDesativandoInimmigos = true;

            //Enquanto estiver naves ativas
            while (naves.Count > 0)
            {
                //Espera um segundo
                yield return new WaitForSeconds(1);
                //Pega uma nave da lista de naves
                NaveControlador n = naves[naves.Count - 1];
                //Tira ela lista de naves ativas
                naves.Remove(n);
                //Desativa a nave
                n.gameObject.SetActive(false);
                //Joga ela na fila do pool
                navesPool.Enqueue(n);
            }
            //Desativa a flag
            estaDesativandoInimmigos = false;
        }
    }
}
