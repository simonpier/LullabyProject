using UnityEngine;

[System.Serializable]
public class Sound
{
    private AudioSource source;

    [SerializeField] public string name;
    [SerializeField] AudioClip clip;
    [SerializeField] [Range(0f , 1f)] float volume = 0.7f;
    [SerializeField] [Range(0.5f , 1.5f)] float pitch = 0.5f;

    public void SetSource(AudioSource _source)
    {

        source = _source;
        source.clip = clip;

    }

    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();

    }

}

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField] Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene");

        }

        else
            instance = this;

    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {

            GameObject go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            go.transform.SetParent(this.transform);
            sounds[i].SetSource(go.AddComponent<AudioSource>()); 

        }
    }

    public void PlaySound(string soundName)
    {

        for (int i = 0; i < sounds.Length; i++)
        {

            if (sounds[i].name == soundName)
            {

                sounds[i].Play();
                return;

            }

        }

        //In case the audio file is not found
        Debug.LogWarning("AudioManager: Sound not found in list: " + soundName);
    }

}
