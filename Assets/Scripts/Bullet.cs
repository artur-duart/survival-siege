using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Este método é chamado quando a bala colide com outro objeto
    private void OnCollisionEnter(Collision collision)
    {
        // Cria o efeito de impacto e destrói a bala
        CreateBulletImpactEffect(collision);
        Destroy(gameObject);
    }

    // Cria o efeito de impacto da bala na posição de contato
    private void CreateBulletImpactEffect(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        GameObject impactEffect = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        // Define o efeito de impacto como filho do objeto atingido
        impactEffect.transform.SetParent(collision.transform);
    }
}
