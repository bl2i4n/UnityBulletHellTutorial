using System.Collections;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }
    private PlayerControls playerControls;
    private float timeBetweenAttacks;
    private bool attackButtonDown, isAttacking = false;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // underscore means we are not passing anything in as a parameter to the Attack function
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

        AttackCooldown(); // Initialize the attack cooldown
    }

    private void Update()
    {
        Attack();
    }

    public void NewWeapon(MonoBehaviour weapon)
    {
        CurrentActiveWeapon = weapon;
        AttackCooldown(); // Start the cooldown when a new weapon is equipped
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines(); // Stop any existing cooldown coroutines
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks); // Adjust the cooldown duration as needed
        isAttacking = false;
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            AttackCooldown(); // Reset the cooldown when the attack button is pressed
            // Call this class and use the IWeapon interface and use the Attack within that Monobehavior
            (CurrentActiveWeapon as IWeapon).Attack();

        }
    }
}
