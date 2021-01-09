using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        if (startFresh)
        {
            PlayerPrefs.DeleteAll();
            startFresh = false;
        }


        instance = this;
        SceneManager.sceneLoaded += LoadState;  //Add loading after every scene change
        DontDestroyOnLoad(gameObject);
    }

    // Start Game Fresh?
    public bool startFresh = false;

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> exTable;

    // References
    public Player player;
    // public weapon weapon . . .
    public FloatingTextManager floatingTextManager;

    public int spawn = 0;

    public RectTransform hitpointBar;

    public string startButtonScene;

    // Logic
    public int skin;
    public int currency;
    public int experience;
    public int weaponLevel;
    public int abilities;


    // ------------------- METHODS -----------------------

    // Start Button on click
    public void StartBtn()
    {
        SceneManager.LoadScene(startButtonScene);
    }

    

    public void heal()
    {
        player.hitpoint = 10;
        OnHitpointChange();
    }

    // Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // HP BAR
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }


    // Save/Load State
    /*
     * 0 - INT preferedSkin
     * 1 - INT currency
     * 2 - INT experience
     * 3 - INT weaponLevel
     * 4 - INT abilities
     */
    public void SaveState()
    {
        string s = "";

        s += skin.ToString() + "|";
        s += currency.ToString() + "|";
        s += experience.ToString() + "|";
        s += weaponLevel.ToString() + "|";
        s += abilities.ToString();

        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("SaveState");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        skin = int.Parse(data[0]);
        currency = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        weaponLevel = int.Parse(data[3]);
        abilities = int.Parse(data[4]);

        //SceneManager.sceneLoaded -= LoadState; //Remove loading after every scene change
        Debug.Log("LoadState");


        if(spawn == 1)
        {
            player.transform.position = GameObject.Find("SpawnPoint (1)").transform.position;
            spawn = 0;
        }
        else if (spawn == 2)
        {
            player.transform.position = GameObject.Find("SpawnPoint (2)").transform.position;
            spawn = 0;
        }
        else
        {
            player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        }
       
    }

}
