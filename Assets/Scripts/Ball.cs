using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random=UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public bool isBounce;
    public bool bonusGoal;
    public bool isLastHit1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        int random = Random.Range(0, 2);
        Debug.Log(random);
        if (random == 0)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            rb.velocity = Vector2.left * speed;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (transform.position.x > 12 || transform.position.x < -12 || transform.position.y > 8 || transform.position.y < -8)
        {
            GameManager.instance.spawnBall();
            Destroy(gameObject); 
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "RacketRed" && !isBounce)
        {
            Vector2 dir = new Vector2(1, 0).normalized;
            rb.velocity = dir * speed;
            StartCoroutine("DelayBounce");
            isLastHit1 = true;
        }
        if (col.gameObject.tag == "RacketBlue" && !isBounce)
        {
            Vector2 dir = new Vector2(-1, 0).normalized;
            rb.velocity = dir * speed;
            StartCoroutine("DelayBounce");
            isLastHit1 = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Goal1")
        {
            GameManager.instance.player2Score++;
            Debug.Log("Player 2 Goal");
            if(bonusGoal)
            {
                GameManager.instance.player2Score++;
            }
            GameManager.instance.spawnBall();
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "Goal2")
        {
            GameManager.instance.player1Score++;
            Debug.Log("Player 1 Goal");
            if(bonusGoal)
            {
                GameManager.instance.player1Score++;
            }
            GameManager.instance.spawnBall();
            Destroy(gameObject);
        }
    }

    private IEnumerator DelayBounce()
    {
        isBounce = true;
        yield return new WaitForSeconds(1f);
        isBounce = false;
    }
}
