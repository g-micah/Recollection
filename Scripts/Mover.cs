using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    public Animator animator;

    protected BoxCollider2D boxCollider;
    protected Vector3 movement;
    protected Rigidbody2D rb;
    //protected RaycastHit2D hit;
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected void UnitMovement(Vector3 input)
    {
        UnitMovement(input, true);
    }

    protected void UnitMovement(Vector3 input, bool transScale)
    {
        movement = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);


        if (movement.magnitude > 1)
        {
            animator.SetFloat("Magnitude", 1.0f);
        }
        else
        {
            animator.SetFloat("Magnitude", movement.magnitude);
        }


        if (transScale)
        {
            if (movement.x < 0)
            {
                animator.SetInteger("Direction", 2);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (movement.x > 0)
            {
                animator.SetInteger("Direction", 0);
                transform.localScale = Vector3.one;
            }

            /* THIS IS FOR UP AND DOWN ANIMATIONS
            else if (movement.y > 0)
            {
                animator.SetInteger("Direction", 3);
            } 
            else if (movement.y < 0)
            {
                animator.SetInteger("Direction", 1);
            }
            */
        }

        // Add push vector, if any.
        movement += pushDirection;

        // Reduce push force every frame, based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);


        // Movement without Rigidbody:
        /*
        // Make sure can move in current direction by casting box first. If box returns null, free to move.
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, movement.y), Mathf.Abs(movement.y * Time.deltaTime * speed), LayerMask.GetMask("Person", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, (movement.y * Time.deltaTime * speed), 0);
        }


        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, movement.x), Mathf.Abs(movement.x * Time.deltaTime * speed), LayerMask.GetMask("Person", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate((movement.x * Time.deltaTime * speed), 0, 0);
        }*/


        rb.velocity = new Vector2(movement.x, movement.y);

    }

}
