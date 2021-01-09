using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Mover
{
    protected override void Start()
    {
        base.Start();

        DontDestroyOnLoad(gameObject);
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);

        GameManager.instance.OnHitpointChange();
    }


    private void Update()
    {
        float x = Input.GetAxisRaw("NewHorizontal");
        float y = Input.GetAxisRaw("NewVertical");


        UnitMovement(new Vector3(x, y, 0.0f));

        if(GameManager.instance.experience >= 38)
        {
            GameManager.instance.experience = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("YouWin");
        }

    }

    protected override void Death()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndingScreen");
    }


}