using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{


    public bool isPaused;
    [SerializeField] private GameObject menuPausa;

    // Start is called before the first frame update
    void Start()
    {
        //menuPausa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Reanudar();
            }
            else
            {
                GamePause();
            }


        }
    }

    public void GamePause()
    {
        menuPausa.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;

    }
    public void Reanudar()
    {
        isPaused = false;
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
    }
}
