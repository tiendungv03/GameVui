using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticalEffect : MonoBehaviour
{
    [SerializeField] float delay = 4f;
    // Start is called before the first frame update
    void Awake()
    {
        /*StartCoroutine(nameof(DestroyParticle));*/
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private IEnumerable DestroyParticle() { yield return new WaitForSeconds(delay); }*/
}
