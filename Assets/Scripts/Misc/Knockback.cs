using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool GettingKnockedBack {
        get;
        private set;
    }
    // grabs the rigidbody2D component
    private Rigidbody2D myRigidbody;
    [SerializeField] private float knockBackTime = .2f;

    private void Awake()
    {
        // gets the rigidbody2D component
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void GetKnockback(Transform damageSource, float knockbackThrust)
    {
        GettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockbackThrust * myRigidbody.mass;
        myRigidbody.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());

    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        myRigidbody.linearVelocity = Vector2.zero;
        GettingKnockedBack = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
