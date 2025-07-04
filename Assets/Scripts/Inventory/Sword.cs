using System;
using System.Runtime.CompilerServices;
using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{

    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = 0.5f;
    [SerializeField] private WeaponInfo weaponInfo;


    private Transform weaponCollider;
    private Animator myAnimator;

    private GameObject slashAnim;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }


    public void Attack()
    {
        // fire our sword animation
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
                                                                    // same rotation as it's prefab
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        // set prefab to the parent which is ActiveWeapon
        slashAnim.transform.parent = this.transform.parent;

    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    private void Start()
    {
        // these are 2 different ways of doing the same thing. The latter is more volatile
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashAnimSpawnPoint").transform;
        DoneAttackingAnimEvent();

    }
    // Update is called once per frame
    void Update()
    {
        MouseFollowWithOffset();
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        Vector2 playerPosition = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y - playerPosition.y, Mathf.Abs(mousePos.x - playerPosition.x)) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);

        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
