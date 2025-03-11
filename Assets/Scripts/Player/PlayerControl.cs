using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    public Transform comienzoRayo;

    private Animator animator;
    private bool caminarDerecha = true;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
}
