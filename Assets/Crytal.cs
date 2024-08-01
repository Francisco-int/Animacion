using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crytal : MonoBehaviour
{
    [SerializeField] SpriteRenderer crytalDead;
    [SerializeField] float speed;
   [SerializeField] Sprite[] spriteDead;
    int spriteIndex;
    [SerializeField] bool colisionJugador;
    float time;
    [SerializeField] Animator animator;
    int i;
    [SerializeField] AudioSource hitAudio;
    // Start is called before the first frame update
    void Start()
    {
        spriteIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(transform.right * speed * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.Y))
        {
            crytalDead.sprite = spriteDead[spriteIndex];
            spriteIndex++;
        }
   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            crytalDead.flipX = !crytalDead.flipX;
            speed = speed * -1;
        }        
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hitAudio.Play();
            speed = 0.3f;
            animator.SetBool("Dead", true);
            Destroy(gameObject, 0.8f);
            
        }
    }
}
