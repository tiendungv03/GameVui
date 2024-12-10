using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
    }

    // Start is called before the first frame update
    public GameObject[] objectToSpawn;
    public float heightHouseFoundation;
    public int numSpawn = 1;
    public float rotationX = 0;
    public float rotationY = 0;
    public float rotationZ = 0;
    private Transform objectCollider;

    void Awake()
    {
        //take meshcollider position of house
        objectCollider = gameObject.GetComponent<Collider>().transform;



        //spawn by number object
        for (int i = 0; i < numSpawn; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {

        //random object spawn
        int index = UnityEngine.Random.Range(0, objectToSpawn.Length);

        //set position spawn plus height of house foundation
        Vector3 positionSpawn = new Vector3(objectCollider.position.x,
                                objectCollider.position.y + heightHouseFoundation,
                                objectCollider.position.z);

        //set object rotation to above floor
        Quaternion rotationSpawn = Quaternion.Euler(rotationX, rotationY, rotationZ);
        Instantiate(objectToSpawn[index], positionSpawn, rotationSpawn);
    }
}
