using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    protected BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];


    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // collision work
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            OnCollide(hits[i]);

            // clean array
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("ERROR: OnCollide was not implemented in " + this.name);
    }
}
