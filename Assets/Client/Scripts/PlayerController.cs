// Copyright (c) 2012-2024 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 5;

    private Rigidbody2D rb;
    private Transform myTransform;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = transform;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Finish"))
        {
            EndGame();
        }
    }
    
    public void Update()
    {
        if (myTransform.position.y < -10)
        {
            EndGame();
        }
        
        MovePlayer();
        
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.05f)
        {
            Jump();
        }
    }
    
    private void MovePlayer()
    {
        myTransform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void EndGame()
    {
        myTransform.position = new Vector3(-8.05f, 0.5f, 0);
    }
}