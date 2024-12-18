using Unity.VisualScripting;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    public GameObject Gun;
    public Transform WeaponParent;

    private void Start()
    {
        Gun.GetComponent<Rigidbody>().isKinematic = true; 
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.F)) 
        {
            Drop();
        }
    }

    void Drop () 
    {
        WeaponParent.DetachChildren();
        Gun.transform.eulerAngles = new Vector3(90f /*Gun.transform.position.x*/, Gun.transform.position.z, Gun.transform.position.y);
        Gun.GetComponent<Rigidbody>().isKinematic = false;
        Gun.GetComponent<MeshCollider>().enabled = true;
    }

    void Equip()
    {
        Gun.GetComponent <Rigidbody>().isKinematic = true;
        Gun.transform.position = WeaponParent.transform.position;
        Gun.transform.rotation= WeaponParent.transform.rotation;

        Gun.GetComponent<MeshCollider>().enabled= false;
        Gun.transform.SetParent(WeaponParent);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            if (Input.GetKey(KeyCode.E))
            {
                Equip();
            }
        }
    }
}
