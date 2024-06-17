using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab; // Prefab da munição
    public Transform bulletSpawn;   // Local de spawn da munição
    public float bulletVelocity = 30f; // Velocidade da munição
    public float bulletPrefabLifeTime = 3f; // Tempo de vida da munição

    void Update()
    {
        // Disparo da arma ao clicar o botão esquerdo do mouse
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        // Instancia a munição na posição e rotação do spawn
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // Aplica uma força à munição na direção para frente do spawn
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(bulletSpawn.forward * bulletVelocity, ForceMode.Impulse);
        }

        // Inicia a corrotina para destruir a munição após um tempo
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        // Espera pelo tempo especificado antes de destruir a munição
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
