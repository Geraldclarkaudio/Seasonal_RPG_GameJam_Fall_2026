using UnityEngine;

public class BasicAttack : MonoBehaviour
{

    [SerializeField]
    private int _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy_Combat hitEnemy = other.GetComponent<Enemy_Combat>();

            hitEnemy.TakeDamage(_damage);
        }
    }
}
