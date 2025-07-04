using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    private Animator myAnimator;
    
    // this improved performance by avoiding string lookups
    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    public void Attack()
    {
        myAnimator.SetTrigger(FIRE_HASH);
        Debug.Log("Bow Attack");
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        // something is wrong with the projectile script, it doesn't have the weapon info
        newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
