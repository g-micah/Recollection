using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int currencyAmount = 5;

    protected override void OnCollect()
    {
        if(!collected)
        { 
            base.OnCollect(); //collected = true
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.currency += currencyAmount;
            GameManager.instance.ShowText("+" + currencyAmount + " silver!", 25, Color.yellow, transform.position, Vector3.up * 0.1f, 1.5f);
            //Debug.Log("Gain " + currencyAmount + " Silver!");
        }
    
    }
}
