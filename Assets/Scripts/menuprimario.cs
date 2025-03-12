using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuprimario : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jugar()
    {
        SceneManager.LoadScene("Juego_Erick"); 
    
    
    }
    public void salir()
    {
        Debug.Log("salir");
        Application.Quit();
    }
    public void salirMenu()
    {        
        SceneManager.LoadScene("menubasico");
        AudioManager.Instance.PlayMusic("Menu Theme");
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
