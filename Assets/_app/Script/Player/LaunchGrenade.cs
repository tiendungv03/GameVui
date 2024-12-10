using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchGrenade : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject grenade;

    public float range = 40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) { Launch(); }
    }

    private void Launch()
    {
        GameObject grenadeInstance = Instantiate(grenade, spawnPoint.position, spawnPoint.rotation);
        grenadeInstance.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * range, ForceMode.Impulse);
    }
}
