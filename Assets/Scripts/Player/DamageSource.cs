using Unity.VisualScripting;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
                // question mark is a null check
                // check if enemy health component exists
            enemyHealth?.TakeDamage(damageAmount);
        
    }
}
