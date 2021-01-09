using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MAKE SURE ENEMY BOX COLLIDER HAS X OFFSET OF 0!!

public class Enemy : Mover
{
    // Experience
    public int xpValue = 1;

    // Logic
    public float triggerLength = 0.3f;
    public float chaseLength = 3;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();

        playerTransform = GameManager.instance.player.transform; 
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
        // Is the player in range to KEEP chasing?
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            // Is the player in range to START chasing?
            if(Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            {
                chasing = true;
            }

            // Am I chasing?
            if (chasing)
            {
                // Am I running into player or am I getting knockedback?
                if (!collidingWithPlayer || knockedBack)
                {
                    if (rb.constraints == RigidbodyConstraints2D.FreezeAll)
                    {
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    }

                    // Run To Player
                    UnitMovement((playerTransform.position - transform.position).normalized);
                    
                }
                else
                {
                    // Stop
                    UnitMovement(transform.position, false);
                }
            }
            else
            {
                // Run To Home
                UnitMovement(startingPosition - transform.position);
            }
        }
        else
        {
            // Run To Home
            UnitMovement(startingPosition - transform.position);
            chasing = false;
        }

        // Check for overlaps
        collidingWithPlayer = false;

        // collision work
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            if (hits[i].CompareTag("Fighter") && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
                if (rb.constraints == RigidbodyConstraints2D.FreezeRotation && !knockedBack)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }

            // clean array
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.experience += xpValue;
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 0.4f, 1.0f);
    }

}
