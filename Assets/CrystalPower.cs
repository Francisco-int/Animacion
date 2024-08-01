using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPower : MonoBehaviour
{
    [SerializeField] SpriteRenderer crystalPower;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            crystalPower.flipX = !crystalPower.flipX;
            speed = speed * -1;
        }
    }
}
