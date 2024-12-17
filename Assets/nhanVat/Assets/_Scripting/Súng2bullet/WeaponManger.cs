using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManger : MonoBehaviour
{
    public static WeaponManger Instance { get; set; }

    public List<GameObject> weaponSlots;
    public GameObject activeWeaponSlot;

    [Header(" Ammo")]
    public int totalRifleAmmo = 0;
    public int totalPistolAmmo = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        activeWeaponSlot = weaponSlots[0];
    }

    private void Update()
    {
        foreach (GameObject weaponSlot in weaponSlots)
        {
            if (weaponSlot == activeWeaponSlot)
            {
                weaponSlot.SetActive(true);
            }
            else
            {
                weaponSlot.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchActiveSlot(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchActiveSlot(1);
            }
        }
        // Duyệt qua các phím để đổi slot
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveSlot(0); // Đổi sang slot 0
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveSlot(1); // Đổi sang slot 1
        }
    }
    public void PickeupWeapon(GameObject pickeupWeapon)
    {
        AddWeaponIntoActiveSlot(pickeupWeapon);
    }
    public void PickUpWeapon(GameObject pickupWeapon)
    {
        AddWeaponIntoActiveSlot(pickupWeapon);
    }

    private void AddWeaponIntoActiveSlot(GameObject pickupWeapon)
    {
        DropCurrentWeapon(); // Bỏ vũ khí hiện tại (nếu có)

        // Thêm vũ khí mới vào slot đang hoạt động
        pickupWeapon.transform.SetParent(activeWeaponSlot.transform, false);
        Weapon weapon = pickupWeapon.GetComponent<Weapon>();

        // Thiết lập vị trí và góc quay của vũ khí trong slot
        pickupWeapon.transform.localPosition = weapon.spawnPosition;
        pickupWeapon.transform.localRotation = Quaternion.Euler(weapon.spawnRotation);

        // Kích hoạt trạng thái của vũ khí
        weapon.isActiveWeapon = true;
    }

    private void DropCurrentWeapon()
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;

            // Tắt trạng thái vũ khí đang sử dụng
            weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false;

            // Đặt vũ khí rơi ra ngoài slot hiện tại
            weaponToDrop.transform.SetParent(null); // Hoặc có thể thay bằng một parent khác tùy vào cách bạn quản lý vũ khí

            // Thiết lập lại vị trí và góc quay của vũ khí
            weaponToDrop.transform.localPosition = Vector3.zero;
            weaponToDrop.transform.localRotation = Quaternion.identity;
        }
    }

    public void SwitchActiveSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= weaponSlots.Count)
        {
            Debug.LogError("Slot index out of range!");
            return;
        }

        // Tắt trạng thái của vũ khí trong slot hiện tại
        if (activeWeaponSlot.transform.childCount > 0)
        {
            Weapon currentWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            currentWeapon.isActiveWeapon = false; // Tắt vũ khí cũ
        }

        // Cập nhật slot đang hoạt động
        activeWeaponSlot = weaponSlots[slotIndex];

        // Kích hoạt vũ khí trong slot mới
        ActivateWeaponInSlot(activeWeaponSlot);
    }
    private void ActivateWeaponInSlot(GameObject slot)
    {
        if (slot.transform.childCount > 0)
        {
            Weapon weapon = slot.transform.GetChild(0).GetComponent<Weapon>();
            weapon.isActiveWeapon = true; // Bật trạng thái vũ khí
        }
    }

    internal void PickupAmmo(AmmoBox ammo)
    {
        switch (ammo.ammoType)
        {
            case AmmoBox.AmmoType.PistolAmmo:
                totalPistolAmmo += ammo.ammoAmount;
                break;

            case AmmoBox.AmmoType.RifleAmmo:
                totalRifleAmmo += ammo.ammoAmount;
                break;
        }
    }


    //public void DecreaseTotalAmmo(int amount, string ammoType)
    //{
    //    switch (ammoType)
    //    {
    //        case "Pistol":
    //            totalPistolAmmo -= amount;
    //            break;
    //        case "Rifle":
    //            totalRifleAmmo -= amount;
    //            break;
    //    }
    //}

}