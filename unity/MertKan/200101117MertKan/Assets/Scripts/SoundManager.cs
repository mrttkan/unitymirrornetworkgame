using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SoundManager");
                _instance = go.AddComponent<SoundManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private AudioSource audioSource;
    private bool isMuted = false;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                // Burada müzik dosyanızı atayın
                audioSource.clip = Resources.Load<AudioClip>("YourMusicFileName"); // Replace with your actual music file name
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        audioSource.mute = isMuted;
    }
}
