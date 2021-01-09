using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warp : Collidable
{
    public string teleportScene;
    public int teleportPosition = 0;

    protected override void OnCollide(Collider2D coll)
    {

        if (coll.name == "Player")
        {
            //Teleport the player scene
            //Save First
            GameManager.instance.SaveState();
            //string sceneName = sceneNames[Random.Range(0, sceneNames.Length)]; //For random scene teleportation
            if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName(teleportScene))
            {
                SceneManager.LoadScene(teleportScene);
            }


            //Teleport player position
            GameManager.instance.spawn = teleportPosition;
        }
    }

}
