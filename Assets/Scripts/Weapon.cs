using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Shooting Settings")]
    public bool isShooting; // Indica se está atirando
    private bool readyToShoot = true; // Indica se a arma está pronta para atirar
    private bool allowReset = true; // Permite resetar o estado de tiro
    public float shootingDelay = 0.1f; // Atraso entre os tiros
    public float burstDelay = 0.1f; // Atraso entre os tiros em modo burst

    [Header("Burst Settings")]
    public int bulletsPerBurst = 3; // Número de balas por rajada
    private int burstBulletsLeft; // Balas restantes na rajada atual

    [Header("Spread Settings")]
    public float spreadIntensity = 0.1f; // Intensidade da propagação

    [Header("Bullet Settings")]
    public GameObject bulletPrefab; // Prefab da munição
    public Transform bulletSpawn; // Local de spawn da munição
    public float bulletVelocity = 30f; // Velocidade da munição
    public float bulletPrefabLifeTime = 3f; // Tempo de vida da munição

    [Header("Effects")]
    public GameObject muzzleEffect; // Efeito de disparo
    private Animator animator; // Animador para o recoil

    public enum ShootingMode
    {
        Single, // Tiro único
        Burst, // Rajada
        Auto // Automático
    }

    public ShootingMode currentShootingMode; // Modo de tiro atual

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleShootingInput();
    }

    private void HandleShootingInput()
    {
        // Verifica o modo de tiro e atualiza o estado de tiro
        isShooting = currentShootingMode switch
        {
            ShootingMode.Auto => Input.GetKey(KeyCode.Mouse0),
            ShootingMode.Single => Input.GetKeyDown(KeyCode.Mouse0),
            ShootingMode.Burst => Input.GetKeyDown(KeyCode.Mouse0),
            _ => isShooting
        };

        // Se pronto para atirar e o jogador está atirando
        if (readyToShoot && isShooting)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        // Ativa o efeito do cano da arma e animação de recoil
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        // Toca o som de disparo da arma
        SoundManager.Instance.shootingSoundM1911.Play();

        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instancia a munição na posição e rotação do spawn
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.transform.forward = shootingDirection;

        // Aplica uma força à munição
        if (bullet.TryGetComponent(out Rigidbody bulletRb))
        {
            bulletRb.AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        }

        // Inicia a corrotina para destruir a munição após um tempo
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        // Reseta o estado de tiro após um atraso
        if (allowReset)
        {
            Invoke(nameof(ResetShot), shootingDelay);
            allowReset = false;
        }

        // Modo Burst
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke(nameof(FireWeapon), burstDelay);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint = Physics.Raycast(ray, out RaycastHit hit) ? hit.point : ray.GetPoint(100);

        Vector3 direction = targetPoint - bulletSpawn.position;
        float x = Random.Range(-spreadIntensity, spreadIntensity);
        float y = Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        // Espera pelo tempo especificado antes de destruir a munição
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
