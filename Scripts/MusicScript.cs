using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;
    
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
            
            audioSource.loop = true;
            audioSource.volume = 0.5f;
            
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu") PlayMusic(menuMusic);
        else if (scene.name == "Play") PlayMusic(gameMusic);
    }
    
    void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        
        if (audioSource.clip == clip && audioSource.isPlaying) return;
        
        audioSource.clip = clip;
        audioSource.Play();
    }
}