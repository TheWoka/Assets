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
            
            InitializeAudioSource();
            
            // 🔥 Подписка на событие
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            // Сразу запускаем музыку для текущей сцены
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }
        else
        {
            // Уничтожаем дубликат
            Destroy(gameObject);
        }
    }
    
    // 🔥 ВАЖНО: Отписка от события при уничтожении объекта
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    // Вынесли инициализацию в отдельный метод
    void InitializeAudioSource()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        
        audioSource.loop = true;
        audioSource.volume = 0.5f;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 🔥 Проверка: не уничтожен ли сам объект (специальная проверка Unity)
        if (this == null) return;
        
        if (scene.name == "Menu") 
            PlayMusic(menuMusic);
        else if (scene.name == "Play" || scene.name == "Village") 
            PlayMusic(gameMusic);
    }
    
    void PlayMusic(AudioClip clip)
    {
        // 🔥 Проверка: жив ли AudioSource, если нет — создаём заново
        if (audioSource == null)
        {
            InitializeAudioSource();
            if (audioSource == null) return;
        }
        
        if (clip == null) return;
        
        // Если та же музыка уже играет — не перезапускаем
        if (audioSource.clip == clip && audioSource.isPlaying) return;
        
        audioSource.clip = clip;
        audioSource.Play();
    }
}