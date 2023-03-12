using UnityEngine;
using UnityEngine.UI;


public class Homescreen : MonoBehaviour
{
    private Text coins;

    private void Start()
    {
        coins = GameObject.Find("Coins").GetComponent<Text>();
        transform.Find("Score").GetComponent<Text>().text = Storage.instance.highScore.ToString();
        AudioManager.instance.Play("Main");

    }

    private void Update()
    {
        coins.text = Storage.instance.coins.ToString();

    }


    public void OnPlayClick()
    {
        SceneLoader.instance.ChangeScene("Game", true);
    }


}
