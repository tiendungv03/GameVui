using System.Collections;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;

    public bool isActiveWeapon;

    public bool readyToShoot;
    public float shootingDelay = 2f;

    public enum FireMode { Single, Burst, Auto }
    public FireMode currentFireMode = FireMode.Single;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 3f;

    public int burstCount = 3;
    public float burstFireRate = 0.1f;
    public float autoFireRate = 0.1f;

    public GameObject muzzleEffect;
    public float reloadTime;
    public int currentAmmo = 150; // số lượng đạn hiện tại trong túi
    public int maxCurrentAmmo = 300;
    public int magazineSize, bulletLeft;
    public bool isReloading;

    public TextMeshProUGUI notificationText;
    private bool isFiring = false;

    // Animator Integration
    internal Animator animator;

    //
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    private void Start()
    {
        readyToShoot = true;
        animator = GetComponent<Animator>();
        bulletLeft = magazineSize;
        SetAnimatorState("Idle");
    }

    void Update()
    {
        if (isActiveWeapon)
        {
            if (Input.GetKeyDown(KeyCode.V))
                SwitchFireMode();

            // Single Shot: Bắn một viên khi nhấn chuột trái
            if (Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot)
            {
                if (bulletLeft > 0)
                {
                    isFiring = true;
                    if (currentFireMode == FireMode.Single)
                    {
                        FireSingleShot();
                    }
                    else if (currentFireMode == FireMode.Burst)
                    {
                        StartCoroutine(BurstFire());
                    }
                    else if (currentFireMode == FireMode.Auto)
                    {

                        StartCoroutine(AutoFire());
                    }
                }
                else
                {
                    HandleEmptyMagazine();
                }
            }

            // Đảm bảo khi giữ chuột không bắn liên tục trong chế độ Single
            if (Input.GetKey(KeyCode.Mouse0) && currentFireMode == FireMode.Single)
            {
                SetAnimatorState("Idle"); // Chuyển về Idle khi giữ chuột
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                isFiring = false;
                if (currentFireMode == FireMode.Single || currentFireMode == FireMode.Burst)
                {
                    SetAnimatorState("Idle");
                    readyToShoot = true; // Allow firing again for Single and Burst
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && bulletLeft < magazineSize)
            {
                if (!isReloading && !isFiring)
                {

                    Debug.LogWarning("qdjhabjahfis");
                    StartCoroutine(Reload()); // Bắt đầu Coroutine nạp đạn
                }
            }

            if (readyToShoot && !isFiring && !isReloading && bulletLeft <= 0)
                ShowNotification("Press R to Reload!");

            if (AmmoManager.Instance.ammoDisplay != null)
                AmmoManager.Instance.ammoDisplay.text = $"{bulletLeft} / {currentAmmo}";
        }
    }

    private void FireSingleShot()
    {
        readyToShoot = false; // Prevent firing again until mouse is released
        ShootBullet();
        SetAnimatorState("Recoil");
    }

    private void ShootBullet()
    {
        bulletLeft--;
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        SoundManager.Instance.shootingSound.Play();

        Vector3 shootingDirection = CalculateDirectionAndSpread();
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        if (bulletLeft <= 0)
            HandleEmptyMagazine();
    }

    IEnumerator Reload()
    {
        isReloading = true; // Đặt trạng thái đang nạp đạn
        SoundManager.Instance.reloadingSound.Play();
        yield return new WaitForSeconds(reloadTime); // Chờ thời gian nạp đạn
        Debug.Log("Reloading...");

        if (bulletLeft + currentAmmo >= magazineSize)
        {
            currentAmmo = currentAmmo + bulletLeft - magazineSize;
            bulletLeft = magazineSize; // Nạp lại đạn
        }
        else
        {
            bulletLeft += currentAmmo;
            currentAmmo = 0;
        }

        isReloading = false; // Hoàn tất nạp đạn
        Debug.Log("Reload complete!");
    }

    private void ReloadComplete()
    {
        bulletLeft = magazineSize;
        isReloading = false;
        SetAnimatorState("Idle");
        HideNotification();
    }

    private IEnumerator BurstFire()
    {
        readyToShoot = false; // Prevent firing again until mouse is released
        SetAnimatorState("Recoil");

        for (int i = 0; i < burstCount; i++)
        {
            if (bulletLeft > 0)
                ShootBullet();
            else
            {
                HandleEmptyMagazine();
                break;
            }
            yield return new WaitForSeconds(burstFireRate);
        }

        SetAnimatorState("Idle");
    }

    private IEnumerator AutoFire()
    {
        SetAnimatorState("Recoil");
        while (isFiring && bulletLeft > 0)
        {
            ShootBullet();
            yield return new WaitForSeconds(autoFireRate);
        }

        if (!isFiring || bulletLeft <= 0)
            SetAnimatorState("Idle");
    }

    private void HandleEmptyMagazine()
    {
        SetAnimatorState("Idle");
        SoundManager.Instance.emptyManagizeSound.Play();
        ShowNotification("Press R to Reload!");
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    private void SwitchFireMode()
    {
        currentFireMode = (FireMode)(((int)currentFireMode + 1) % 3);
        SetAnimatorState("FireModeSwitch");
        Debug.Log("Current Fire Mode: " + currentFireMode);
    }

    private void ShowNotification(string message)
    {
        if (notificationText != null)
        {
            notificationText.text = message;
            notificationText.gameObject.SetActive(true);
        }
    }

    private void HideNotification()
    {
        if (notificationText != null)
            notificationText.gameObject.SetActive(false);
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Vector3 direction = bulletSpawn.forward;
        float spread = 0.05f;
        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);
        return direction.normalized;
    }

    private void SetAnimatorState(string state)
    {
        if (animator != null)
        {
            animator.Play(state);
        }
    }
}
