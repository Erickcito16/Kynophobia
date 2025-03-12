using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl2 : MonoBehaviour
{
    private Rigidbody rb;
    public Transform comienzoRayo;
    public ParticleSystem explocionDeParticulas;

    private Animator animator;
    private bool caminarDerecha = true;
    private GameManager gameManager;
    
    private Score ScorePlayer;
    private Enemy2 enemy;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

      void Start()
    {
        ScorePlayer = GameObject.FindObjectOfType<Score>();
        enemy = FindObjectOfType<Enemy2>(); 
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
            AudioManager.Instance.PlaySFX("Collect Item");
            ScorePlayer.UpdateScore(10);
            Destroy(other.gameObject);
            gameManager.AumentarPuntaje();
        }

        if (other.CompareTag("Enemy"))
    {
        Debug.Log("¡El jugador ha sido atrapado por el enemigo!");       
        gameManager.GameOver(); 
    }

    if (other.gameObject.layer == LayerMask.NameToLayer("Powerup"))
    {
        Debug.Log("¡Power-Up recogido! Encogiendo enemigo...");
        AudioManager.Instance.PlaySFX("PowerUp");

        if (enemy != null) // Verifica que el enemigo exista en la escena
        {
            enemy.ShrinkTemporarily(); // Llama al método del enemigo
        }

        Destroy(other.gameObject); // Elimina el Power-Up tras recogerlo
    }
    }

    
}
