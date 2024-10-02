using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class AIRacket : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("NPC Setting")]
    public float speed;
    public float delayMove;

    private bool isMoveAI; //cek raket bergerak atau tidak
    private float randomPos; //-1 ke 1
    private bool isSingleTake;
    private bool isUp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameData.instance.isSinglePlayer)
        {
            // ! = invert == false
            if (!isMoveAI && !isSingleTake)
            {
                Debug.Log("Terpanggil");

                StartCoroutine("DelayAIMove");
                isSingleTake = true;
            }

            if (isMoveAI)
            {
                MoveAI();
            }
        }
    }

    private IEnumerator DelayAIMove()
    {
        yield return new WaitForSeconds(delayMove); // menunggu waktu dari delayMove yang kita setting
        randomPos = Random.Range(-1f, 1f);

        if (transform.position.y < randomPos)
        {
            isUp = true;
        }
        else
        {
            isUp = false;
        }

        isSingleTake = false;
        isMoveAI = true;
    }

    private void MoveAI()
    {
        // ! = invert == false
        if (!isUp) // raket kearah bawah
        {
            rb.velocity = new Vector2(0, -1) * speed; // velocity = acc -> vector2 x=0, y=-1
            if (transform.position.y <= randomPos) //posisi raket apakah sudah sampai di posisi random yang baru
            {
                rb.velocity = Vector2.zero;
                isMoveAI = false;
            }
        }

        if (isUp)
        {
            rb.velocity = new Vector2(0, 1) * speed;
            if (transform.position.y >= randomPos)
            {
                rb.velocity = Vector2.zero;
                isMoveAI = false;
            }
        }
    }
}
