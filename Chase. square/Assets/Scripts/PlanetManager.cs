using UnityEngine;

public class PlanetManager : GameManager
{
    public int actualLevel;
    public LevelSpawner levelSpawner;
    public override void Start()
    {
        //Find References
        player = FindObjectOfType<Player>();
        abilitySpawner = FindObjectOfType<AbilitySpawner>();
        levelSpawner = FindObjectOfType<LevelSpawner>();
        backgroundSpawnwer = GameObject.Find("Meteorites").GetComponent<ClutterSpawnwer>();
        gui = GameObject.Find("GUI").GetComponent<GUIManager>();

        startSpeed = speed;
        startSize = player.transform.localScale;

        isRunning = true;
        levelSpawner.SpawnLevel(actualLevel);



    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (isRunning)
        {
            //chech if player is death
            if (player.transform.position.x > deathpoint.position.x)
            {
                GameOver();
                return;
            }


            /* Nicht gemergte Änderung aus Projekt "Assembly-CSharp.Player"
            Vor:
                        //increase score and speed

                        speed = startSpeed * Mathf.Pow(speedMultiplier, (score / 1000));
            Nach:
                        //increase score and speed

                        speed = startSpeed * Mathf.Pow(speedMultiplier, (score / 1000));
            */
            //increase score and speed

            speed = startSpeed * Mathf.Pow(speedMultiplier, (score / 1000));


        }
    }

    public override void GameOver()
    {
        isRunning = false;
        if (actualLevel > Storage.instance.actuelLevel.high)
        {
            gui.GameOver(actualLevel, true);
            Storage.instance.highScore = actualLevel - 1;
            Storage.instance.SaveGame();
        }
        else
        {
            gui.GameOver(actualLevel, false);
        }

        //reset values and stop game
        ResetValues();
        StopAllCoroutines();

    }

    public override void ResetValues()
    {
        base.ResetValues();
        actualLevel = 0;
    }

    public override void Spawn()
    {
        //Reset values
        ResetValues();
        //Destroy all objects
        levelSpawner.DestroyAll();
        foreach (Obstacle b in backgroundSpawnwer.childs)
        {
            if (!(b.gameObject.name == backgroundSpawnwer.gameObject.name))
            {
                b.gameObject.SetActive(false);
            }
        }
        //Start Spawn
        backgroundSpawnwer.Starting();

        levelSpawner.SpawnLevel(actualLevel);



        //reset positions
        player.gameObject.transform.position = Vector3.zero;


        isRunning = true;
    }

    public override void NextLevel()
    {
        actualLevel += 1;
        levelSpawner.SpawnLevel(actualLevel);
    }
}
