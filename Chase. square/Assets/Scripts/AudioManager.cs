
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        print(s + name);
        s.source.Play();
        
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.isPlaying;
    }

    /*public void Update()
    {
        if(GameManager.instance != null)
        {
            if (GameManager.instance.isRunning)
            {
                if (isPlaying("Main"))
                    Stop("Main");
                if (!isPlaying("Game"))
                    Play("Game");

            }
            else
            {
                if (isPlaying("Game"))
                    Stop("Game");
                if (!isPlaying("Main"))
                    Play("Main");
            }
        }


    }*/
}
