using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    [SerializeField] bool move;
    [SerializeField] bool jump;
    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] bool powerUpAble;
    [SerializeField] GameObject powerUpL;
    [SerializeField] GameObject UIFin;

    [SerializeField] AudioSource powerUpAudio;
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource deadAudio;

    // Start is called before the first frame update
    void Start()
    {
        powerUpAble = false;
        move = true;
        jump = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(1);
        }
        if (move)
        {
            float horizontal = Input.GetAxis("Horizontal");
            Vector2 move = new Vector2(horizontal, 0);
            transform.Translate(move * speed);
            if(horizontal < 0)
            {
                
                if (powerUpAble)
                {
                    animator.SetBool("PowerWalk", true);
                    spriteRenderer.flipX = true;
                }
                else
                {
                    animator.SetBool("Walk", true);
                    spriteRenderer.flipX = true;
                }
            }
            if (horizontal > 0)
            {
                if (powerUpAble)
                {
                    animator.SetBool("PowerWalk", true);
                    spriteRenderer.flipX = false;
                }
                else
                {
                    animator.SetBool("Walk", true);
                    spriteRenderer.flipX = false;
                }
            }
            if(horizontal == 0)
            {
                if(powerUpAble)
                {
                    animator.SetBool("PowerIdle", true);
                    animator.SetBool("PowerWalk", false);

                }
                else
                {
                     animator.SetBool("Walk", false);
                }
                
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && jump)
        {
            jumpAudio.Play();
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
            Debug.Log("jump");
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Level2"))
        {
            SceneManager.LoadScene(2);
        }
        if (collision.gameObject.CompareTag("Collect"))
        {
            
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Fin"))
        {
            SceneManager.LoadScene(4);
        }
        if (collision != null)
        {
            jump = true;
        }
        if (collision.gameObject.CompareTag("Enemy") && powerUpAble == false)
        {
            deadAudio.Play();
            move = false;           
            animator.SetBool("Dead", true);
            StartCoroutine(loadSceneGameover());
        }
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            powerUpAudio.Play();
            animator.SetBool("Transform", true);
            powerUpAble = true;
            powerUpL.SetActive(true);
            Destroy(collision.gameObject);
            StartCoroutine(powerUp());
        }
    }
 
    IEnumerator loadSceneGameover()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(3);
        
    }
    IEnumerator powerUp()
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("PowerIdle", true);
        animator.SetBool("Transform", false);
        yield return new WaitForSeconds(5);
        animator.SetBool("Distransform", true);
        yield return new WaitForSeconds(2);
        powerUpAble = false;
        powerUpL.SetActive(false);
        animator.SetBool("Idle", true);
        animator.SetBool("Distransform", false);
    }
}
