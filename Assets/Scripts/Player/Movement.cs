using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float Speed = 10f;
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float deceleration = 80f;

    private PlayerInputActions playerinput;
    private Rigidbody2D rb;
    private Vector2 inputvector;

    private void Awake()
    {
        playerinput = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerinput.Enable();
    }

    private void OnDisable()
    {
        playerinput.Disable();
    }

    private void Update()
    {
        inputvector = GetMovementVector();

        float TargetX = inputvector.x * Speed;

        if (TargetX > 0)
        {
            transform.localScale = new Vector3(10, 10, 10);
        }
        else if (TargetX < 0)
        {
            transform.localScale = new Vector3(-10, 10, 10);
        }
    }

    private Vector2 GetMovementVector()
    {
        Vector2 MoveVector = playerinput.Player.Move.ReadValue<Vector2>();
        return MoveVector;
    }

    private void FixedUpdate()
    {
        float TargetX = inputvector.x * Speed;
        float TargetY = inputvector.y * Speed;

        float RateX = (Mathf.Abs(TargetX) > 0.01f) ? acceleration : deceleration;
        float RateY = (Mathf.Abs(TargetY) > 0.01f) ? acceleration : deceleration;

        rb.linearVelocityX = Mathf.MoveTowards(rb.linearVelocity.x, TargetX, RateX * Time.fixedDeltaTime);
        rb.linearVelocityY = Mathf.MoveTowards(rb.linearVelocity.y, TargetY, RateY * Time.fixedDeltaTime);
    }
}
