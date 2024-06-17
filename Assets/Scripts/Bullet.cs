using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Este método é chamado quando a bala colide com outro objeto
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto colidido possui a tag "Target"
        if (collision.gameObject.CompareTag("Target"))
        {
            // Imprime no console que o alvo foi atingido
            Debug.Log("Hit " + collision.gameObject.name + "!");

            // Destroi a bala
            Destroy(gameObject);
        }
    }
}
