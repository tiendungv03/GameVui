using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }
    public Weapon hovereWeapon = null;
    public AmmoBox hoveredAmmoBox = null;
    private void Awake()
    {
        // Singleton Pattern để đảm bảo chỉ có một InteractionManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        // Raycast từ trung tâm màn hình
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Kiểm tra va chạm với các vật thể
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform?.gameObject;

            // Kiểm tra xem vật thể có phải là vũ khí không
            if (objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false)
            {
                hovereWeapon = objectHitByRaycast.GetComponent<Weapon>();

                // Kiểm tra nếu vũ khí có component Outline
                Outline outline = hovereWeapon.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true; // Bật hiệu ứng Outline khi hover vào vũ khí
                }

                // Kiểm tra nhấn phím F để nhặt vũ khí
                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManger.Instance.PickeupWeapon(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                // Nếu không trỏ vào vũ khí, tắt Outline
                if (hovereWeapon != null)
                {
                    Outline outline = hovereWeapon.GetComponent<Outline>();
                    if (outline != null)
                    {
                        outline.enabled = false; // Tắt hiệu ứng Outline khi không hover vào vũ khí
                    }
                    hovereWeapon = null;
                }
            }
            
            
            //AmmoBox
            if (objectHitByRaycast.GetComponent < AmmoBox>())
            {
                hoveredAmmoBox = objectHitByRaycast.gameObject.GetComponent<AmmoBox>();
                hoveredAmmoBox.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (hoveredAmmoBox != null && WeaponManger.Instance != null)
                    {
                        WeaponManger.Instance.PickupAmmo(hoveredAmmoBox);
                    }
                    Destroy(objectHitByRaycast.gameObject);

                }
            }
            else
            {
                if (hoveredAmmoBox)
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                }
            }

        }
        else
        {
            // Nếu không va chạm với gì, tắt Outline
            if (hovereWeapon != null)
            {
                Outline outline = hovereWeapon.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = false; // Tắt hiệu ứng Outline khi không trỏ vào vũ khí
                }
                hovereWeapon = null;
            }
        }

     
    }
}