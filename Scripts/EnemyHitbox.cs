using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    // Damage
    public int damagePoint = 1;
    public float pushForce = 3;

    protected override void OnCollide(Collider2D coll)
    {
        // if hit player
        if(coll.tag == "Fighter" && coll.name == "Player")
        {
            // Create damamge object to send to player
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);

        }
    }
}
