using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Fibonacci
{    
    //Retorna o valor Fibonnaci de x, sendo x a contagem de vezes que o Fibonnaci é executado a partir de 1;
    public static int RetornaFibonacci(int x)
    {
        if (x <= 1)
        {
      
            return 1;
        }
        
        return RetornaFibonacci(x - 1) + RetornaFibonacci(x - 2);
    }

}
