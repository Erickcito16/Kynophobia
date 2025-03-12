using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
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

        
        if (!DetectarSuelo())
        {
            animator.SetTrigger("cayendo");
        }

        
        if (transform.position.y < -3)
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

        animator.SetTrigger("comenzoJuego");

        
        rb.MovePosition(transform.position + transform.forward * 2 * Time.fixedDeltaTime);
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
    }

    private bool DetectarSuelo()
    {
        float alturaChequeo = 1.5f;
        Vector3 boxSize = new Vector3(0.8f, 0.1f, 0.8f);

        return Physics.BoxCast(comienzoRayo.position, boxSize / 2, Vector3.down, Quaternion.identity, alturaChequeo);
    }
}
