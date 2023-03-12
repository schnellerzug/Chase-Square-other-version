using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Menus : MonoBehaviour
{
    public static Menus instance;

    public GameObject[] menus;
    public GameObject quitMenu;  
    public int actuelMenu;

    public void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        actuelMenu = 0;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(actuelMenu == 0)
            {
                quitMenu.SetActive(true);
                return;
            }

            SwitchToHome();
        }
    }

    public void PlaySound(string soundName)
    {
        AudioManager.instance.Play(soundName);
    }

    public void SwitchMenu(string name)
    {
        menus[actuelMenu].SetActive(false);
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].name == name)
            {
                menus[i].SetActive(true);
                actuelMenu = i;
            }
        }
        
    }
    public void SwitchToHome()
    {
        menus[actuelMenu].SetActive(false);
        menus[0].SetActive(true);
        actuelMenu = 0;
    }
}
