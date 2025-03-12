using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotationSpeed = 5.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    private Animator animator;
    private bool canMove = false;
    private Queue<Vector3> playerPath = new Queue<Vector3>();
    public float pathUpdateRate = 0.1f;

    // Variables de sonido
    public AudioSource audioSource;
    public AudioClip barkSound;
    public AudioClip runSound;

    // Variables de encogimiento
    private bool isShrunk = false;
    private Vector3 originalScale;
    public float shrinkFactor = 0.5f;
    public float shrinkDuration = 5.0f;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        originalScale = transform.localScale; // Guarda la escala original

        InvokeRepeating("RecordPlayerPath", 0f, pathUpdateRate);
    }

    public void ShrinkTemporarily()
    {
        if (!isShrunk)
        {
            StartCoroutine(ShrinkCoroutine());
        }
    }

    private IEnumerator ShrinkCoroutine()
    {
        isShrunk = true;
        transform.localScale = originalScale * shrinkFactor;
        Debug.Log("¡Enemigo encogido!");
        yield return new WaitForSeconds(shrinkDuration);
        transform.localScale = originalScale;
        Debug.Log("¡Enemigo restaurado!");
        isShrunk = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            canMove = true;
            animator.SetBool("IsSitting", false);
            animator.SetBool("IsRunning", true);

            if (!audioSource.isPlaying)
            {
                audioSource.clip = runSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        if (canMove && playerPath.Count > 0)
        {
            Vector3 nextPosition = playerPath.Peek();
            float distance = Vector3.Distance(transform.position, nextPosition);

            if (distance > 0.1f)
            {
                Vector3 direction = (nextPosition - transform.position).normalized;
                enemyRb.MovePosition(transform.position + direction * speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

                animator.SetBool("IsSitting", false);
                animator.SetBool("IsRunning", true);
            }
            else
            {
                playerPath.Dequeue();
            }
        }
        else
        {
            if (audioSource.clip == runSound && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            //animator.SetBool("IsRunning", false);
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
            if(audioSource.clip == runSound && audioSource.isPlaying){
                audioSource.Stop();
            }
            if(canMove == true){
            audioSource.PlayOneShot(barkSound);
            }
            //canMove = false;
        }
    }
}


