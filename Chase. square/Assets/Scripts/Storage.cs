
using UnityEngine;
using UnityEngine.SceneManagement;

public class Storage : MonoBehaviour
{
    public static Storage instance;

    public GameObject homescreen;

    public int highScore;
    public int coins;

    public float scoreMultiplikator;
    public float coinsMultiplier;
    public float playerSpeedMultiplikator;

    public int playerSkin;
    public Skin[] playerSkins;

    public Planet[] planets;
    public Planet actuelLevel;

    public bool sounds;
    public bool vibration;

    public bool alreadyPlayed;

    public Item[] playerItem;
    public Item[] booster;
    



    void Awake()
    {
        
        
        SceneManager.sceneLoaded += LoadGame;

        if (instance != null)
        {


            Destroy(gameObject);
            return;
        }


        instance = this;
        DontDestroyOnLoad(gameObject);

        ResetItems();
    }

    public void LoadGame(Scene s, LoadSceneMode mode)
    {
        //check if player has save game ,if yes, set values
        if (PlayerPrefs.HasKey("SavedCoins"))
        {

            coins = PlayerPrefs.GetInt("SavedCoins");
            highScore = PlayerPrefs.GetInt("SavedScore");
            playerSpeedMultiplikator = PlayerPrefs.GetFloat("PlayerSpeed");
            SceneManager.sceneLoaded -= LoadGame;
            alreadyPlayed = true;


        }
        else
        {
            playerSpeedMultiplikator = 1;
            coins = 0;
            highScore = 0;
            alreadyPlayed = false;
            
        }

        if (PlayerPrefs.HasKey("playerSkin0"))
        {
            playerSkin = PlayerPrefs.GetInt("PlayerSkin");
            for (int i = 0; i < playerSkins.Length; i++)
            {
                playerSkins[i].hasBuy = PlayerPrefs.GetInt("playerSkin" + i.ToString()) > 0 ? true : false;
            }
        }
        sounds = PlayerPrefs.GetInt("Sounds") > 0 ? true : false;
        vibration = PlayerPrefs.GetInt("Vibration") > 0 ? true : false;


        /*if (PlayerPrefs.HasKey("level0"))
        {
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i].hasFinished = PlayerPrefs.GetInt("level" + i.ToString()) > 0 ? true : false;
            }
        }*/

    }

    public void SaveGame()
    {
        //Save Values
        PlayerPrefs.SetInt("SavedCoins", coins);
        PlayerPrefs.SetInt("SavedScore", highScore);

        //Shop Values
        PlayerPrefs.SetInt("PlayerSkin", playerSkin);
        for (int i = 0; i < playerSkins.Length; i++)
        {
            PlayerPrefs.SetInt("playerSkin" + i.ToString(), playerSkins[i].hasBuy ? 1 : 0);
        }

        //Settings Values
        PlayerPrefs.SetInt("Sounds",sounds ? 1 : 0);
        PlayerPrefs.SetInt("Vibration",vibration ? 1 : 0);

        PlayerPrefs.SetFloat("PlayerSpeed", playerSpeedMultiplikator);

        //Level Values

        /*for (int i = 0; i < levels.Length; i++)
        {
            PlayerPrefs.SetInt("level" + i.ToString(), levels[i].hasFinished ? 1 : 0);
        }*/

        PlayerPrefs.Save();
    }

    public void ResetItems()
    {
        foreach (var item in playerItem)
        {
            item.actuelDuration = 0;
            item.actuelCooldown = 0;
            item.active = false;
        }
    }

    /*public void Reset()
    {
        PlayerPrefs.DeleteAll();
        Storage.instance.lastMenu = "";
        foreach (var l in levels)
        {
            if (l.levelNumber == 0)
                return;

            l.hasFinished = false;
        }
        LevelLoader.instance.ChangeScene("Start");
        Storage.instance.LoadGame(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }*/


}
