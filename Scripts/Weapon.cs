using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    // Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Swing
    private float cooldown = 0.5f;
    private float lastswing;
    private Animator animator;

    protected override void Start()
    {
        base.Start();  // assign box collider
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        // Collider starts turned off
        boxCollider.enabled = false;
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if  (Time.time - lastswing > cooldown)
            {
                lastswing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name != "Player")
        {
            // Create damage object and send to fighter that is hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        // Enable collider when swinging

        animator.SetTrigger("Swing");


       // Debug.Log("Swing");
       
    }

}
