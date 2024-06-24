using System.Collections.Generic;
using UnityEngine;

public class BeerBottle : MonoBehaviour
{
    [Header("Shatter Settings")]
    public List<Rigidbody> allParts = new List<Rigidbody>(); // Lista de todas as partes da garrafa

    // Método para quebrar a garrafa
    public void Shatter()
    {
        foreach (Rigidbody part in allParts)
        {
            part.isKinematic = false; // Desativa a cinemática das partes para permitir a física
        }
    }
}
