using System.Collections;
using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager Instance { get; private set; }

    [Header("UI Settings")]
    public TextMeshProUGUI ammoDisplay; // Exibição de munição na UI

    private void Awake()
    {
        // Garante que apenas uma instância do AmmoManager exista
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Preserva o objeto ao carregar uma nova cena
        }
    }
}
