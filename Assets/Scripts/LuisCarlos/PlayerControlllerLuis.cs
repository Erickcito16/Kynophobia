using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlllerLuis : MonoBehaviour
{
    private Rigidbody rb;
    public Transform comienzoRayo;
    public ParticleSystem explocionDeParticulas;

    private Animator animator;
    private bool caminarDerecha = true;
    private GameManager gameManager;

    private Score ScorePlayer;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        ScorePlayer = GameObject.FindObjectOfType<Score>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CambiarDireccion();
        }

        RaycastHit contacto;
        if (!Physics.Raycast(comienzoRayo.position, -transform.up, out contacto, Mathf.Infinity))
        {
            animator.SetTrigger("cayendo");
        }

        if( transform.position.y < -2) // Si el jugador cae al vacio.
        {
            gameManager.GameOver();
        }

    }

    private void FixedUpdate()
    {
        if (!gameManager.juegoIniciado)
        {
            return;
        }
        else
        {
            animator.SetTrigger("comenzoJuego");
        }

         rb.transform.position = transform.position + transform.forward * 2 * Time.fixedDeltaTime;
        
    }

    private void CambiarDireccion()
    {
        if(!gameManager.juegoIniciado)
        {
            return;
        }


        caminarDerecha = !caminarDerecha;
        if (caminarDerecha)
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Cristal"))
        {
            if(explocionDeParticulas != null)
            {
                explocionDeParticulas.Play();
            }



            Destroy(other.gameObject);
            gameManager.AumentarPuntaje();
        }
    }

    

    /*void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cristal"))
        {
            ScorePlayer.UpdateScore(10);
            Destroy(other.gameObject);
        }

    }*/
}


