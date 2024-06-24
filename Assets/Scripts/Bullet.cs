using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Este método é chamado quando a bala colide com outro objeto
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log($"Hit a {tag}!");

        switch (tag)
        {
            case "Target":
                CreateBulletImpactEffect(collision);
                Destroy(gameObject);
                break;
            case "Wall":
                CreateBulletImpactEffect(collision);
                Destroy(gameObject);
                break;
            case "Beer":
                collision.gameObject.GetComponent<BeerBottle>().Shatter();
                break;
        }
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
