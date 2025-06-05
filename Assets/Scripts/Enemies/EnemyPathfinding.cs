using System.Numerics;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private UnityEngine.Vector2 moveDir;
    private Knockback knockback;


    void Awake()
    {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void FixedUpdate()
    {
        if (knockback.GettingKnockedBack)
        {
            return;
        }
        // if its not getting knockedback we can move to this code. this is a bool check
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.deltaTime));

    }

    public void MoveTo(UnityEngine.Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }
}
