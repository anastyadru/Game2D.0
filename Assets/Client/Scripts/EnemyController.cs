// Copyright (c) 2012-2024 FuryLion Group. All Rights Reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D physic;
    public Transform player;
    
    public float speed;
    public float agroDistance;
    
    public Transform leftPoint;
    public Transform rightPoint;
    
    private bool movingRight = true;

    public void Start()
    {
        physic = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < agroDistance)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Patrol();
        }
        
        CheckBounds();
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position.x < transform.position.x) ? Vector2.left : Vector2.right;
        UpdateMovement(direction);
    }

    private void Patrol()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        UpdateMovement(direction);
    }
    
    private void UpdateMovement(Vector2 direction)
    {
        physic.velocity = direction * speed;
        transform.localScale = new Vector2(direction.x == -1 ? 0.5f : -0.5f, 0.5f);
    }

    private void CheckBounds()
    {
        if ((movingRight && transform.position.x >= rightPoint.position.x) || (!movingRight && transform.position.x <= leftPoint.position.x))
        {
            movingRight = !movingRight;
            transform.localScale = new Vector2(movingRight ? -0.5f : 0.5f, 0.5f);
        }
    }
}