using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioMixerGroup mainMixer;
    public SoundVariables[] sounds;
    Scene scene;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (SoundVariables s in sounds)
        {
            //Sets the variables of the audio sources in the gameobject
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.outputAudioMixerGroup = mainMixer;
            s.source.clip = s.clips;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.rolloffMode = AudioRolloffMode.Linear;

            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.spatialBlend = s.SpatialBlend;
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.maxDistance;
        }
    }
    private void Start()
    {
        //SoundVariables s = Array.Find(sounds, SoundVariables => SoundVariables.name == "WhiteNoise");
        //s.source.Play();
    }


    public void Play(string name, Transform location, bool playAtPoint)
    {
        SoundVariables s = Array.Find(sounds, SoundVariables => SoundVariables.name == name);
        if (s == null)
        {
            Debug.Log(name + " Not Found");
            return;
        }   

        if (playAtPoint)
        {
            //Plays audio at a specific location
            PlayAt(s.source, location.position);
        }
        else if (!playAtPoint)
        {

            s.source.Play();
        }

    }
    public void Play(string name)
    {
        SoundVariables s = Array.Find(sounds, SoundVariables => SoundVariables.name == name);
        if (s == null)
        {
            Debug.Log(name + " Not Found");
            return;
        }
        s.source.Play();
    }


    public void Stop(string name)
    {
        SoundVariables s = Array.Find(sounds, SoundVariables => SoundVariables.name == name);
        if (s == null)
        {
            Debug.Log(name + " Not Found");
            return;
        }
        //Stops music from playing
        s.source.Stop();
    }

    private AudioSource PlayAt(AudioSource audioSource, Vector3 audioLocation)
    {
        //Creates a temporary gameObject called TempAudio
        GameObject tempAudio = new GameObject("TempAudio");
        //sets its position to the value given
        tempAudio.transform.position = new Vector3(audioLocation.x, audioLocation.y, -10);
        //Adds a audio source
        AudioSource tempSource = tempAudio.AddComponent<AudioSource>();

        //Inputs the settings to the audio source
        tempSource.clip = audioSource.clip;
        tempSource.outputAudioMixerGroup = audioSource.outputAudioMixerGroup;

        tempSource.volume = audioSource.volume;
        tempSource.pitch = audioSource.pitch;

        tempSource.loop = audioSource.loop;
        tempSource.playOnAwake = audioSource.playOnAwake;
        tempSource.spatialBlend = audioSource.spatialBlend;
        tempSource.rolloffMode = AudioRolloffMode.Linear;
        tempSource.minDistance = audioSource.minDistance;
        tempSource.maxDistance = audioSource.maxDistance;

        tempSource.Play();

        //Destorys after the clip plays
        Destroy(tempAudio, tempSource.clip.length);

        return tempSource;
    }

    public void StopWhiteNoise()
    {
        SoundVariables s = Array.Find(sounds, SoundVariables => SoundVariables.name == "WhiteNoise");
        s.source.Stop();
    }

    [Serializable]
    public class SoundVariables
    {
        //audio settings that can be changed in the array
        public string name;

        public AudioClip clips;

        [Range(0f, 1f)]
        public float volume;

        [Range(0.1f, 3f)]
        public float pitch;

        public bool loop;

        public bool playOnAwake;


        [Range(0f, 1f)]
        public float SpatialBlend;

        public float maxDistance;

        public float minDistance;

        [HideInInspector]
        public AudioSource source;


    }


}
