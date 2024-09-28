using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public bool isFadeing = false;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public bool loop;
        public float volume = 1f;  // Individual sound volume
        [HideInInspector]
        public AudioSource source;
    }

    public Sound[] sounds;

    public string backgroundMusicName = "BackgroundMusic";  // Name of the background music clip

    [Range(0f, 1f)]  // Slider in the Unity editor
    public float globalVolume = 1f;  // Global volume multiplier

    private void Awake()
    {
        // Implementing Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize each sound with an AudioSource
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume * globalVolume;  // Set volume based on global volume
        }
    }

    private void Start()
    {
        // Start playing the background music with a fade-in effect
        StartCoroutine(PlayBackgroundMusicWithFadeIn(backgroundMusicName, 2f));  // 2 seconds fade-in duration
    }

    public void Play(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s != null && s.source != null)
        {
            s.source.volume = s.volume * globalVolume;  // Ensure volume is adjusted by global volume
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void Stop(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void Pause(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Pause();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void Resume(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.UnPause();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void SetGlobalVolume(float volume)
    {
        globalVolume = volume;
        // Update the volume of all currently playing sounds
        foreach (Sound s in sounds)
        {
            if (s.source != null && s.source.isPlaying && !isFadeing)
            {
                s.source.volume = s.volume * globalVolume;
            }
        }
    }

    private IEnumerator PlayBackgroundMusicWithFadeIn(string name, float duration)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            isFadeing = true;
            s.source.volume = 0f;  // Start with volume at 0
            s.source.Play();  // Start playing the music
            s.source.volume = 0f;  // Start with volume at 0
            float startVolume = s.volume * globalVolume;
            

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                Debug.Log("S.source.volume: " + s.source.volume);
                Debug.Log("S.volume: " + s.volume);
                elapsedTime += Time.deltaTime;
                s.source.volume = Mathf.Lerp(0f, startVolume, elapsedTime / duration);
                yield return null;
            }

            s.source.volume = s.volume * globalVolume;  // Ensure the final volume is set correctly
            isFadeing = false;
        }
        else
        {
            Debug.LogWarning("Background Music: " + name + " not found!");
        }
    }


    private void Update()
    {
        // Update all currently playing sounds' volume based on globalVolume
        foreach (Sound s in sounds)
        {
            if (s.source != null && s.source.isPlaying)
            {
                if(!isFadeing)
                {
                    Debug.Log("!isFadeing");
                    s.source.volume = s.volume * globalVolume;
                }
                else
                {
                    
                }
                
            }
        }
    }
}




/*
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Assuming the jump sound is named "Jump" in the AudioManager
            AudioManager.Instance.Play("Jump");

        }
    }
}

*/