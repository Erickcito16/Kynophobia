using UnityEngine;
using System.Collections.Generic;

public class Enemy2 : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotationSpeed = 5.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    private Animator animator;
    private bool canMove = false;
    private Queue<Vector3> playerPath = new Queue<Vector3>(); 
    public float pathUpdateRate = 0.1f; // Cada cuánto tiempo se guarda la posición del jugador

    void Start()
    {
        
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>(); // Obtiene el Animator
        

        
        
        // Comenzar a registrar el camino del jugador
        InvokeRepeating("RecordPlayerPath", 0f, pathUpdateRate);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            canMove = true;
            animator.SetBool("IsSitting", false);
            animator.SetBool("IsRunning", true);
            
            
        }

        if (canMove && playerPath.Count > 0)
        {
            Vector3 nextPosition = playerPath.Peek(); // Ver la próxima posición
            float distance = Vector3.Distance(transform.position, nextPosition);

            if (distance > 0.1f)
            {
                Vector3 direction = (nextPosition - transform.position).normalized;
                enemyRb.MovePosition(transform.position + direction * speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                
                // 
                if (distance > 1)
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsRunning", true);
                }
                else
                {
                    animator.SetBool("IsWalking", true);
                    animator.SetBool("IsRunning", false);
                }
            }
            else
            {
                playerPath.Dequeue(); 
            }
        }
    }

    void RecordPlayerPath()
    {
        if (player != null)
        {
            playerPath.Enqueue(player.transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            animator.SetTrigger("IsBarking");
        }
    }
}


