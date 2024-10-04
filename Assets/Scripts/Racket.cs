using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private Animator anim;
    public string axis = "Vertical";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Disable Player 2 input untuk single player mode
        if (axis == "Vertical2" && GameData.instance.isSinglePlayer)
        {
            return;
        }
        //Mengambil variabel dari axing yang sudah di setting di Unity input dengan output (-1,1)
        float v = Input.GetAxis(axis);
        rb.velocity = new Vector2(0, v) * speed;

        //agar tidak keluar batas atas
        if (transform.position.y > 1f)
        {
            transform.position = new Vector2(transform.position.x, 1f);
        }

        //agar tidak keluar batas bawah
        if (transform.position.y < -1f)
        {
            transform.position = new Vector2(transform.position.x, -1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            anim.SetTrigger("Shoot");
        }
    }
}
