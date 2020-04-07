using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Public Properties
    public List<AudioContainer> audioSources;

    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioContainer container in audioSources)
        {
            container.source = gameObject.AddComponent<AudioSource>();
            container.source.clip = container.clip;
            container.source.volume = container.volume;
            container.source.loop = container.repeating;
            if (container.startOnSceneLoad)
                container.source.Play();
        }
    }

    // PlayAudio()
    public void PlayAudio(string audioName /*, float delayInSeconds/**/)
    {
        // yield return new WaitForSeconds(delayInSeconds);
        bool audioFound = false;
        for (int num = 0; num < audioSources.Count; num++)
        {
            if (audioName == audioSources[num].name)
            {
                if (audioSources[num].source.isPlaying)
                    audioSources[num].source.Stop();
                audioSources[num].source.Play();
                num = audioSources.Count;
                audioFound = true;
            }
        }

        if (!audioFound)
            Debug.LogError("\t[ AudioManager ] could not find container with name \"" + audioName + "\" !");
    }

    // StopAudio()
    public void StopAudio(string audioName /*, float delayInSeconds/**/ )
    {
        // yield return new WaitForSeconds(delayInSeconds);
        bool audioFound = false;
        for (int num = 0; num < audioSources.Count; num++)
        {
            if (audioName == audioSources[num].name)
            {
                if (audioSources[num].source.isPlaying)
                    audioSources[num].source.Stop();
                audioFound = true;
            }
        }

        if (!audioFound)
            Debug.LogError("\t[ AudioManager ] could not find container with name \"" + audioName + "\" !");
    }
}

[System.Serializable]
public class AudioContainer
{
    // Public Properties
    public string name = "";
    public AudioClip clip;
    [Range(0, 1)]
    public float volume = 1;
    public bool repeating = false;
    public bool startOnSceneLoad = false;
    [HideInInspector]
    public AudioSource source;
}
