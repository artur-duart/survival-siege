using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    // Instância Singleton para referência global
    public static GlobalReferences Instance { get; private set; }

    [Header("Bullet Settings")]
    public GameObject bulletImpactEffectPrefab; // Prefab para o efeito de impacto da bala

    private void Awake()
    {
        // Configuração do Singleton: garante que haja apenas uma instância
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: mantem o objeto ao trocar de cena
        }
    }
}
