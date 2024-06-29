using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; } // Singleton instance

    [Header("Shooting Channel")]
    public AudioSource ShootingChannel; // Canal de áudio para tiros

    [Header("Pistol M1911 Sounds")]
    public AudioClip M1911Shot; // Áudio de disparo da M1911
    public AudioSource reloadingSoundM1911; // Áudio de recarregamento da M1911
    public AudioSource emptyMagazineSoundM1911; // Áudio de disparo sem munição da M1911

    [Header("M4 Sounds")]
    public AudioClip M4Shot; // Áudio de disparo da M4
    public AudioSource reloadingSoundM4; // Áudio de recarregamento da M4

    private void Awake()
    {
        // Garantir que apenas uma instância do SoundManager exista
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

    public void PlayShootingSound(Weapon.WeaponModel weapon)
    {
        // Toca o som de disparo baseado no modelo da arma
        switch (weapon)
        {
            case Weapon.WeaponModel.PistolM1911:
                ShootingChannel.PlayOneShot(M1911Shot);
                break;
            case Weapon.WeaponModel.M4:
                ShootingChannel.PlayOneShot(M4Shot);
                break;
        }
    }

    public void PlayReloadSound(Weapon.WeaponModel weapon)
    {
        // Toca o som de recarregamento baseado no modelo da arma
        switch (weapon)
        {
            case Weapon.WeaponModel.PistolM1911:
                reloadingSoundM1911.Play();
                break;
            case Weapon.WeaponModel.M4:
                reloadingSoundM4.Play();
                break;
        }
    }
}
