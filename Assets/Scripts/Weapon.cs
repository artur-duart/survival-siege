using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Player Settings")]
    public Camera playerCamera;

    [Header("Shooting Settings")]
    public bool isShooting, readyToShoot;
    private bool allowReset = true;
    public float shootingDelay = 0.1f; // Atraso entre os tiros
    public float burstDelay = 0.1f; // Atraso entre os tiros em modo burst

    [Header("Burst Settings")]
    public int bulletsPerBurst = 3;
    private int burstBulletsLeft;

    [Header("Spread Settings")]
    public float spreadIntensity = 0.1f; // Intensidade da propagação

    [Header("Bullet Settings")]
    public GameObject bulletPrefab; // Prefab da munição
    public Transform bulletSpawn; // Local de spawn da munição
    public float bulletVelocity = 30f; // Velocidade da munição
    public float bulletPrefabLifeTime = 3f; // Tempo de vida da munição

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
    }

    void Update()
    {
        HandleShootingInput();
    }

    private void HandleShootingInput()
    {
        // Verifica o modo de tiro e atualiza o estado de tiro
        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        // Se pronto para atirar e o jogador está atirando
        if (readyToShoot && isShooting)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instancia a munição na posição e rotação do spawn
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // Apontando a bala para enfrentar a direção do tiro
        bullet.transform.forward = shootingDirection;

        // Aplica uma força à munição na direção para frente do spawn
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        }

        // Inicia a corrotina para destruir a munição após um tempo
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        // Reseta o estado de tiro após um atraso
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        // Modo Burst
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", burstDelay);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            // Atingindo algo
            targetPoint = hit.point;
        }
        else
        {
            // Atirando no ar
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;
        float x = Random.Range(-spreadIntensity, spreadIntensity);
        float y = Random.Range(-spreadIntensity, spreadIntensity);

        // Retornando a direção do tiro com propagação
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        // Espera pelo tempo especificado antes de destruir a munição
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
