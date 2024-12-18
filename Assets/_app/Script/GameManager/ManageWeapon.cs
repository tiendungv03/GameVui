using System.Collections.Generic;
using UnityEngine;

public class ManageWeapon
{
    private List<Weapon> weapons;
    private ManageData manageData;
    private string dbPath;

    public ManageWeapon(string dbPath)
    {
        this.dbPath = dbPath;
        manageData = new ManageData(dbPath);
        weapons = new List<Weapon>();
    }

    public void GetWeaponStatus()
    {
        manageData.OpenConnect();

        weapons = manageData.GetDataFromTable("Gun", reader =>
        {
            Weapon weapon = new Weapon();
            weapon.AddDataStatus(reader);
            return weapon;
        });
        manageData.CloseConnect();
    }

    public void ShowWeaponStatus()
    {
        foreach (Weapon weapon in weapons)
            Debug.Log(weapon);
    }

    public List<Weapon> Weapons() { return weapons; }
}
