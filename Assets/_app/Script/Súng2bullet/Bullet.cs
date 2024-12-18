using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.LogWarning("hit" + collision.gameObject.name + "!");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            CreateBulletImpactEffect(collision);
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            Debug.Log("hit" + other.gameObject.name + "!");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }

    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];
        GameObject hole = Instantiate
            (
            GlobalReferences.Instance.bulletImpactEffectPrefab,contact.point,
            Quaternion.LookRotation(contact.normal)
            );
        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
