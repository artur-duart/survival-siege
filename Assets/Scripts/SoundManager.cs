using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; } // Singleton instance
    public AudioSource shootingSoundM1911; // Áudio de disparo da M1911
    public AudioSource reloadingSoundM1911; // Áudio de carregamento da M1911
    public AudioSource emptyMagazineSoundM1911; // Áudio de disparo sem munição da M1911

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destrói se já existir uma instância
        }
        else
        {
            Instance = this; // Define esta instância como a instância única
            DontDestroyOnLoad(gameObject); // Mantém a instância entre as cenas
        }
    }
}
