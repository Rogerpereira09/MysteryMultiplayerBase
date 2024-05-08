using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Jogador : MonoBehaviour
{
    // Caso verdadeiro irá agir no comportamento do jogador 1
    // senão controlará o jogador 2
    public bool jogador1;
    // Define a velocidade de deslocamento do jogador
    public float velocidade = 5.0f;

    void Start()
    {
        // Verifica qual jogador o script controlara.
        // Busca na cena uma camera com o nome declarado e 
        // faz o parentesco com o jogador, fazendo com que ela siga
        // o mesmo quando estiver em movimento.
        if (jogador1)
        {
            GameObject.Find("CameraJogador1").transform.parent = this.transform;
        }
        else
        {
            GameObject.Find("CameraJogador2").transform.parent = this.transform;
        }


    }
    void Update()
    {
        if (jogador1)
        {
            InputJogador1(velocidade);
        }
        else
        {
            InputJogador2(velocidade);
        }

    }
    // Lê os inputs do jogador 1 para controlar seus movimentos
    void InputJogador1(float v)
    {
        if (Input.GetKey(KeyCode.W))            // Move para frente
        {
            transform.Translate(Vector3.forward * v * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))       // Move para trás
        {
            transform.Translate(Vector3.back * v * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))       // Move para direita
        {
            transform.Translate(Vector3.left * v * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))       // Move para direita
        {
            transform.Translate(Vector3.right * v * Time.deltaTime);
        }
    }

    // Lê os inputs do jogador 2 para controlar seus movimentos
    void InputJogador2(float v)
    {
        if (Input.GetKey(KeyCode.UpArrow))             // Move para frente
        {
            transform.Translate(Vector3.forward * v * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))       // Move para trás
        {
            transform.Translate(Vector3.back * v * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))       // Move para direita
        {
            transform.Translate(Vector3.right * v * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))       // Move para direita
        {
            transform.Translate(Vector3.left * v * Time.deltaTime);
        }
    }
}
