using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class GUIManager : MonoBehaviour
{
    public Text score;
    public Text phase;
    public GameObject gameOver;
    public GameObject pause;
    public GameObject gameOverMenu;
    public ItemMenu itemMenu;

    public float doupleTime = 0.2f;
    private bool checking;
    private int clickCount;



    private void Update()
    {
        score.text = GameManager.instance.score.ToString();
        
    }



    public void ShowItem()
    {
        itemMenu.ShowItem();      

    }
    public void ShowBooster()
    {
        itemMenu.ShowBooster();
    }

    public void UpdatePhase()
    {
        phase.text = GameManager.instance.phase.ToString() + " PHASE";
    }
    public void OnButtonClick()
    {
        clickCount++;
        if (!checking)
            StartCoroutine(clickInterval());
    }

    IEnumerator clickInterval()
    {
        checking = true;
        yield return new WaitForSeconds(doupleTime);
        if (clickCount >= 2)
        {
            Back();

        }
        else
        {
            Spawn();

        }
        clickCount = 0;
        checking = false;
    }

    public void GameOver(float score, bool newHigh)
    {
        gameOver.SetActive(true);
        gameOverMenu.transform.Find("Score").GetComponent<Text>().text = "Score: " + score.ToString();
        gameOverMenu.transform.Find("Coins").GetComponent<Text>().text = "+" + GameManager.instance.coins + " Coins" + (Storage.instance.coinsMultiplier > 1 ? " X " + Storage.instance.coinsMultiplier.ToString() : "");

        /*if (newHigh)
            gameOverMenu.transform.Find("Highscore").GetComponent<Text>().text = "New Highscore";
        else
            gameOverMenu.transform.Find("Highscore").GetComponent<Text>().text = "Highscore: " + Storage.instance.highScore.ToString();
            */
        }

    public void Pause()
    {
        pause.SetActive(true);
    }

    public void Back()
    {
        SceneLoader.instance.ChangeScene("Homescreen", true);
    }

    public void Continue()
    {
        GameManager.instance.Continue();
        pause.SetActive(false);
    }

    public void Spawn()
    {
        GameManager.instance.Spawn();
        gameOver.SetActive(false);
    }

   public void PauseBack()
   {
        pause.SetActive(false);
        GameManager.instance.GameOver();
        Back();
   }

    public void PauseSpawn()
    {
        pause.SetActive(false);
        GameManager.instance.GameOver();
        Spawn();

    }
}
