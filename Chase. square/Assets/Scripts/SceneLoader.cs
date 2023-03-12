using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public GameObject loadingScreen;


    // Start is called before the first frame update
    void Start()
    {

        instance = this;

    }

    public void ChangeScene(string name, bool loadingScreen)
    {
        Storage.instance.SaveGame();
        if (loadingScreen)
        {
            StartCoroutine(LoadSceneAsync(name));
            AsyncOperation async = SceneManager.LoadSceneAsync(name);
        }
        else
        {

            AsyncOperation async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);

        }

    }

    IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(name);
        loadingScreen.SetActive(true);
        if (async.isDone)
        {
            loadingScreen.SetActive(false);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
        }
        else
        {
            yield return null;
        }

    }




}
