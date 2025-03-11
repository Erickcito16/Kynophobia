using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlllerLuis : MonoBehaviour
{
    private Rigidbody rb;
    public Transform comienzoRayo;

    private Animator animator;
    private bool caminarDerecha = true;

    private Score ScorePlayer;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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

    }

    private void FixedUpdate()
    {
        rb.transform.position = transform.position + transform.forward * 2 * Time.fixedDeltaTime;
    }

    private void CambiarDireccion()
    {
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ScorePlayer.UpdateScore(10);
            Destroy(other.gameObject);
        }

    }
}