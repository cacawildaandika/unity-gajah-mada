using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float maxDist = 0f;
    public float minDist = 0f;
    public float moveSpeed = 3f;

    private Vector2 initialPosition;
    private Rigidbody2D target;
    private Rigidbody2D player;
    private Animator animator;

    private float maxDistY = 0f;
    private float minDistY = 0f;
    private float jumpHeight = 1.5f;
    private bool isChase = false;
    private bool isDie = false;
    private int direction;

    void Start()
    {
        initialPosition = transform.position;
        target = gameObject.GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Rigidbody2D>();

        maxDist = transform.position.x;
        minDist = transform.position.x - minDist;

        maxDistY = transform.position.y + maxDistY;
        minDistY = transform.position.y;

        direction = (int) Mathf.RoundToInt(Random.Range(-1, 1));
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Chase();
        if (!isChase)
        {
            Move();
        }
        if (isDie)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Tags.PLAYER)
        {
            animator.enabled = false;
            Destroy(GetComponent<BoxCollider2D>());
            direction = 2;
            Die();
        }
    }

    void Move()
    {
        if(gameObject.tag == Tags.ENEMY)
        {
            switch (direction)
            {
                case -1:
                    if (transform.position.x > minDist)
                    {
                        MoveLeft();
                    }
                    else
                    {
                        direction = 1;
                    }
                    break;
                case 1:
                    if (transform.position.x < maxDist)
                    {
                        MoveRight();
                    }
                    else
                    {
                        direction = -1;
                    }
                    break;
                case 0:
                    direction = (int)Mathf.RoundToInt(Random.Range(-1, 1));
                    break;
            }
        }else if(gameObject.tag == Tags.ENEMY2)
        {
            switch (direction)
            {
                case -1:
                    if (transform.position.y < maxDistY)
                    {
                        target.velocity = new Vector2(target.velocity.x, moveSpeed);
                    }
                    else
                    {
                        direction = 1;
                    }
                    break;
                case 1:
                    if (transform.position.y > minDistY)
                    {
                        target.velocity = new Vector2(target.velocity.x, -moveSpeed);
                    }
                    else
                    {
                        direction = -1;
                    }
                    break;
                case 0:
                    direction = (int)Mathf.RoundToInt(Random.Range(-1, 1));
                    break;
            }
        }

    }

    void MoveRight()
    {
        target.velocity = new Vector2(moveSpeed, target.velocity.y);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void MoveLeft()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        target.velocity = new Vector2(-moveSpeed, target.velocity.y);
    }

    void Die()
    {
        isDie = true;

        if (gameObject.tag == Tags.ENEMY2)
        {
            float maxY = maxDistY + jumpHeight / 2;
            float minY = initialPosition.y * 2;
            print(maxY);
            if (target.position.y > maxY)
            {
                direction = 3;
            }
            if (target.position.y < minY)
            {
                Destroy(gameObject);
            }
            switch (direction)
            {
                case 2:
                    target.velocity = new Vector2(target.velocity.x, moveSpeed);
                    break;
                case 3:
                    transform.Translate(Vector2.down * jumpHeight * 3 * Time.deltaTime);
                    break;
            }
        }
        else
        {
            float maxY = initialPosition.y + jumpHeight;
            float minY = initialPosition.y * 2;
            if (target.position.y >= maxY)
            {
                direction = 3;
            }
            if (target.position.y <= minY)
            {
                Destroy(this.gameObject);
            }
            switch (direction)
            {
                case 2:
                    target.velocity = Vector2.zero;
                    transform.Translate(Vector2.up * jumpHeight * 5 * Time.deltaTime);
                    break;
                case 3:
                    transform.Translate(Vector2.down * Time.deltaTime);
                    break;
            }
        }
        
    }

    void Chase()
    {
        if (!isDie)
        {
            if (Vector2.Distance(transform.position, player.position) <= 3)
            {
                isChase = true;
                transform.position = Vector2.MoveTowards(transform.position, player.position, (moveSpeed + 0.5f) * Time.deltaTime);
            }
            else
            {
                isChase = false;
            }
        }
    }

}
