// Copyright (c) 2012-2024 FuryLion Group. All Rights Reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D physic;
    private Transform myTransform;
    public Transform player;
    
    public float speed;
    public float agroDistance;
    
    public Transform leftPoint;
    public Transform rightPoint;
    
    private bool movingRight = true;
    
    public void Awake()
    {
        physic = GetComponent<Rigidbody2D>();
        myTransform = transform;
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
        Vector2 direction = (player.position.x < myTransform.position.x) ? Vector2.left : Vector2.right;
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
        myTransform.localScale = new Vector2(direction.x == -1 ? 0.5f : -0.5f, 0.5f);
    }

    private void CheckBounds()
    {
        if ((movingRight && myTransform.position.x >= rightPoint.position.x) || (!movingRight && myTransform.position.x <= leftPoint.position.x))
        {
            movingRight = !movingRight;
            myTransform.localScale = new Vector2(movingRight ? -0.5f : 0.5f, 0.5f);
        }
    }
}