using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cañon : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject point;
    [SerializeField] Animator cañonAnimator;
    [SerializeField] float force;
    [SerializeField] AudioSource shootAudio;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(point.transform.right * force, ForceMode2D.Impulse);
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            player.SetActive(false);
            cañonAnimator.SetBool("Shoot", true);
           StartCoroutine(disparo());
            
        }
        
    }
    IEnumerator disparo()
    {
        yield return new WaitForSeconds(1f);
        shootAudio.Play();
        player.transform.position = point.transform.position;
        player.SetActive(true);
        Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(point.transform.right * force, ForceMode2D.Impulse);
        Debug.Log("ff");
    }
}
