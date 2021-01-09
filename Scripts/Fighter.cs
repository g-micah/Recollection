using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Public Fields
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    // Push
    protected Vector3 pushDirection;
    protected bool knockedBack = false;

    // All fighters can ReceiveDamage / Die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            StartCoroutine(KnockBackTime());
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText("-"+dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.3f);

            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {
        Debug.Log("ERROR: Death not defined in" + this.name);

    }

    protected IEnumerator KnockBackTime()
    {
        knockedBack = true;
        yield return new WaitForSeconds(0.05f);
        knockedBack = false;
    }

}
