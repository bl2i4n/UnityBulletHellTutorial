using UnityEngine;
using UnityEngine.InputSystem;


public class Testing : MonoBehaviour
{
    private TestControls testControls;
    private Vector2 movement;
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;


    // private Animator myAnimator;
    // private SpriteRenderer mySpriteRenderer;
    [SerializeField] private float moveSpeed = 1f;

    private InputAction attackAction;
    private InputAction tauntAction;



    private Transform myTransform;

    private void Awake()
    {
        // initialize gameobject
        testControls = new TestControls();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        testControls.Enable();    
        // references the input action we created
        attackAction = new InputAction("Attack", binding: "<Keyboard>/e");
        // enable the input action
        attackAction.Enable();
        // subscribe to the performed event
        attackAction.performed += OnAttack;

        tauntAction = new InputAction("Taunt", binding: "<Keyboard>/f");
        tauntAction.Enable();
        tauntAction.performed += OnTaunt;
        
    }

    private void OnDisable()
    {
        attackAction.performed -= OnAttack;
        attackAction.Disable();

        tauntAction.performed -= OnTaunt;
        tauntAction.Disable();

    }

    // update is for player movement
    private void Update()
    {
        moveInput();
        // FlipSprite();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void moveInput()
    {
        movement = testControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);


        Debug.Log(movement.x);

    }

    private void Move()
    {                           // get current position
        myRigidbody.MovePosition(myRigidbody.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        myAnimator.SetBool("isAttacking", true);
        Debug.Log("Attack@!");
    }

    private void ResetAttack()
    {
        myAnimator.SetBool("isAttacking", false);
        Debug.Log("Attack reset");
    }

    private void OnTaunt(InputAction.CallbackContext context)
    {
        myAnimator.SetBool("isTaunting", true);
        Debug.Log("Who's your parents @$%&");
    }

    private void ResetTaunt()
    {
        myAnimator.SetBool("isTaunting", false);
        Debug.Log("Taunt reset");
    }

    // private void FlipSprite()
    // {                                                                           // another way of saying zero
    //     bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;

    //     if (playerHasHorizontalSpeed)
    //     {
    //         transform.localScale = new Vector3(Mathf.Sign(myRigidbody.linearVelocity.x), 1f, 1f);
    //     }
    // }

    private void FlipSprite()
    {
        // Check if the player has horizontal speed
        bool playerHasHorizontalSpeed = Mathf.Abs(movement.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            // Flip the sprite based on the direction of movement
            transform.localScale = new Vector3(Mathf.Sign(movement.x), 1f, 1f);
        }
    }


}
