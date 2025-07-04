using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    private WeaponInfo weaponInfo;

    private void Update()
    {
        MoveProjectile();
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructable indestructable = other.gameObject.GetComponent<Indestructable>();

        if (!other.isTrigger && (enemyHealth || indestructable))
        {
            enemyHealth?.TakeDamage(1);
            Instantiate(particleOnHitPrefabVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}

