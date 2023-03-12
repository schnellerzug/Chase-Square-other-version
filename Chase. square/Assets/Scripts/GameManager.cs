using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public Player player;
    public ClutterSpawnwer backgroundSpawnwer;
    public ClutterSpawnwer obstacleSpawnwer;
    public AbilitySpawner abilitySpawner;
    public Transform deathpoint;
    public GUIManager gui;
    public UnityEngine.Rendering.Universal.Light2D globalLight;

    public bool isRunning;

    public int phase = 0;
    public float[] phaseScore;
    public Color[] phaseColor;

    public float speed;
    protected float startSpeed;
    [SerializeField] protected float speedMultiplier;


    public int score;
    [SerializeField] private float scoreMultiplikator;

    public int coins;
    public int coinsperscore = 100;

    protected Vector3 startSize;

    public void Awake()
    {
        if (instance != null)
            Destroy(this);

        instance = this;
    }

    public virtual void Start()
    {
        //Find References
        player = Instantiate(Storage.instance.playerSkins[Storage.instance.playerSkin].player).GetComponent<Player>();
        backgroundSpawnwer = GameObject.Find("Meteorites").GetComponent<ClutterSpawnwer>();
        obstacleSpawnwer = GameObject.Find("Planets").GetComponent<ClutterSpawnwer>();
        abilitySpawner = FindObjectOfType<AbilitySpawner>();
        gui = GameObject.Find("GUI").GetComponent<GUIManager>();
        globalLight = GameObject.Find("GlobalLight").GetComponent<UnityEngine.Rendering.Universal.Light2D>();

        startSpeed = speed;
        startSize = player.transform.localScale;

        ResetValues();
        gui.ShowBooster();
        isRunning = true;
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        
     

        if (isRunning)
        {
            //check douple tap
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).tapCount >= 2)
                {
                    print("douple click");

                    ShowItem();
                }
            }

            //chech if player is death
            if (player.transform.position.x > deathpoint.position.x)
            {
                GameOver();
                return;
            }

            //increase score and speed
            score += (int)(scoreMultiplikator * Time.deltaTime) ;
            score = (int)(score * Storage.instance.scoreMultiplikator);
            //speed = startSpeed * Mathf.Pow(speedMultiplier, (score / 1000));
            if (score > phaseScore[(int)phase + 1])
            {
                phase += 1;
                gui.UpdatePhase();
                PhaseLight();
            }
                

        }
    }

    void PhaseLight()
    {
        globalLight.color = phaseColor[phase];
        /*if(phase != 1)
        {
            globalLight.intensity = 1;
        }
        else
        {
            globalLight.intensity = 0.5f;
        }*/
    }
    public virtual void GameOver()
    {
        isRunning = false;
       
        Coins();
        Storage.instance.coins += (int)(coins * Storage.instance.coinsMultiplier);
        if (score > Storage.instance.highScore)
        {
            gui.GameOver(score, true);
            Storage.instance.highScore = score;
            Storage.instance.SaveGame();
        }
        else
        {
            gui.GameOver(score, false);
        }

        //reset values and stop game
        ResetBooster();
        ResetValues();
        
        

    }

    public virtual void Coins()
    {
        var sc = score;
        for (int i = phase; i > 0 ; i--)
        {
            while(sc > phaseScore[i]+coinsperscore)
            {
                sc -= coinsperscore;
                coins += 1 * i;
            }
            
            
        }
        

    }

    public virtual void ResetValues()
    {
        StopAllCoroutines();
        score = 0;
        coins = 0;
        phase = 0;
        player.transform.localScale = startSize;
        Storage.instance.ResetItems();
        speed = startSpeed;
        Time.timeScale = 1;
    }

    public virtual void Spawn()
    {
        //Reset values
        ResetValues();
        //Destroy all objects
        foreach (Obstacle o in obstacleSpawnwer.childs)
        {

            if (!(o.gameObject.name == obstacleSpawnwer.gameObject.name))
            {
                o.gameObject.SetActive(false);

            }


        }
        foreach (Obstacle b in backgroundSpawnwer.childs)
        {
            if (!(b.gameObject.name == backgroundSpawnwer.gameObject.name))
            {
                b.gameObject.SetActive(false);
            }



        }
        //Start Spawn
        obstacleSpawnwer.StopAllCoroutines();

        backgroundSpawnwer.Starting();
        obstacleSpawnwer.Starting();
        //reset positions
        player.gameObject.transform.position = Vector3.zero;
        isRunning = true;
        //Show Booster
        gui.ShowBooster();
    }

    public void Pause()
    {

        Time.timeScale = 0;
        isRunning = false;
        gui.Pause();
    }

    public void Continue()
    {
        Time.timeScale = 1;
        isRunning = true;
    }

    /*public IEnumerator Ability(Ability.AbilityType type)
    {
        switch (type)
        {
            case global::Ability.AbilityType.BiggerShip:
                player.transform.localScale = new Vector2(player.transform.localScale.x * 1.5f, player.transform.localScale.y * 1.5f);
                yield return new WaitForSeconds(3f);
                print("Back to small");
                player.transform.localScale = new Vector2(player.transform.localScale.x / 1.5f, player.transform.localScale.y / 1.5f);
                break;

            case global::Ability.AbilityType.SmallerShip:
                player.transform.localScale = new Vector2(player.transform.localScale.x * 0.5f, player.transform.localScale.y * 0.5f);
                yield return new WaitForSeconds (3f);
                player.transform.localScale = new Vector2(player.transform.localScale.x / 0.5f, player.transform.localScale.y / 0.5f);
                break;

            case global::Ability.AbilityType.TimeStop:
                speed *= 0.5f;
                yield return new WaitForSeconds (3f);
                speed *= 1 / 0.5f;
                break;
        }
    }*/

    public void Activate(Ability.AbilityType type, float duration, float power)
    {
        switch (type)
        {
            case Ability.AbilityType.BiggerShip:
                StartCoroutine(BiggerShip(duration, power));
                break;
            case Ability.AbilityType.SmallerShip:
                StartCoroutine(SmallerShip(duration, power));
                break;
            case Ability.AbilityType.TimeStop:
                StartCoroutine(TimeStop(duration, power));
                break;
        }



    }

    public IEnumerator BiggerShip(float duration, float power)
    {
        print("hello");
        player.transform.localScale *= power; //new Vector2(player.transform.localScale.x * 1.5f, player.transform.localScale.y * 1.5f);
        yield return new WaitForSeconds(duration);
        print("Back to small");
        player.transform.localScale /= power;//new Vector2(player.transform.localScale.x / 1.5f, instance.player.transform.localScale.y / 1.5f);

    }

    public IEnumerator SmallerShip(float duration, float power)
    {
        player.transform.localScale *= power;//new Vector2(GameManager.instance.player.transform.localScale.x * 0.5f, GameManager.instance.player.transform.localScale.y * 0.5f);
        yield return new WaitForSeconds(duration);
        print("Back to Big");
        player.transform.localScale /= power; //new Vector2(GameManager.instance.player.transform.localScale.x / 0.5f, GameManager.instance.player.transform.localScale.y / 0.5f);

    }

    public IEnumerator TimeStop(float duration, float power)
    {
        speed *= power;
        yield return new WaitForSeconds(duration);
        speed /= power;
    }

    public virtual void NextLevel()
    {

    }

    

    public void ShowItem()
    {
        gui.ShowItem();
    }
    
    public void UseItem(Item item)
    {
        StartCoroutine(item.Use());
    }

    public void ActivateBooster(string id)
    {
        foreach (var item in Storage.instance.booster)
        {
            if(item.id ==  id)
            {
                if (!item.active)
                {
                    StartCoroutine(item.Use());

                }
            }
        }
    }

    public void ResetBooster()
    {
        foreach (var item in Storage.instance.booster)
        {
            if (item.active)
            {
                item.OnGameEnd();
            }
        }
    }
}
