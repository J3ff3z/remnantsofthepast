using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CorpsePhysicManager : MonoBehaviour
{
    [SerializeField]
    private float gravity;
    Rigidbody2D rb;

    [SerializeField]
    float force;
    [SerializeField]
    private float decreaseJumpingForce;
    [SerializeField]
    private float decreaseMoveForce;
    [SerializeField]
    private float defaultMoveForce;
    [SerializeField]
    private float defaultJumpingForce;
    private float jumpForce;
    private float moveForce;
    private int direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Physics2D.IgnoreCollision(CharacterManager.Instance.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
    }

    private void FixedUpdate()
    {
        DecreasingForce();
        Vector3 movingVector = new Vector3( direction * moveForce * Time.fixedDeltaTime * force, (jumpForce -gravity) * Time.fixedDeltaTime * force, 0);

        if (movingVector != Vector3.zero)
        {
            rb.MovePosition(movingVector + transform.position);
            
        }
    }

    public void Throw(int direction) 
    {
        this.direction = direction;
        moveForce = defaultMoveForce;
        jumpForce = defaultJumpingForce;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(),GetComponent<Collider2D>(),true);
            GetComponent<Collider2D>().isTrigger = false;
        }
    }
    void DecreasingForce()
    {
        if (jumpForce > -defaultJumpingForce)
        {
            jumpForce -= decreaseJumpingForce;
        }
        if (moveForce > 0)
        {
            moveForce -= decreaseMoveForce;
        }
        else moveForce = 0;
    }
}
