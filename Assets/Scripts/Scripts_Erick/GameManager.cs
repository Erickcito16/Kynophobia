using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool juegoIniciado = false;
    public int puntaje;


    public void IniciarJuego()
    {
        juegoIniciado = true;
        FindObjectOfType<Ruta>().IniciarConstruccion();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IniciarJuego();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public void AumentarPuntaje()
    {
        puntaje++;
    }


}
