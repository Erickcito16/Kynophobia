using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool juegoIniciado = false;
    public int puntaje;

     public GameObject inicialText;
     public bool isPaused;
    [SerializeField] private GameObject menuPausa;


    void Start()
    {
            AudioManager.Instance.PlayMusic("Game Theme");
        
    }
    public void IniciarJuego()
    {
        juegoIniciado = true;
        Time.timeScale = 1f;
        FindObjectOfType<Ruta>().IniciarConstruccion();
        inicialText.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IniciarJuego();
            inicialText.SetActive(false);
        }

      
    }

    public void GameOver()
    {
        SceneManager.LoadScene(1);
        AudioManager.Instance.PlayMusic("Game Over Theme");
  
        
    }

    public void AumentarPuntaje()
    {
        puntaje++;
    }
  


}
