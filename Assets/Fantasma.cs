using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    bool move;
    [SerializeField] AudioSource deadAudio;
    // Start is called before the first frame update
    void Start()
    {
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            speed = speed * -1;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            deadAudio.Play();
            move = false;
            animator.SetBool("Dead", true);
            Destroy(gameObject, 2);
        }
    }
}
