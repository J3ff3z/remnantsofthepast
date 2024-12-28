using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private float gravity = 9.8f;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float decreaseJumpingForce;
    [SerializeField]
    private float defaultJumpingForce;

    private CharacterController controller;
    private Vector2 m_Move;

    private float jumpForce;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value)
    {
        m_Move = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (controller.isGrounded)
        {
            jumpForce = defaultJumpingForce;
        }
    }

    private void Update()
    {
        DecreasingForce();

        Vector2 movingVector = new Vector2(m_Move.x * Time.deltaTime * speed, (-gravity + jumpForce )* Time.deltaTime);

        controller.Move(movingVector);
    }

    void DecreasingForce()
    {
        if(jumpForce > 0)
        {
            jumpForce -= decreaseJumpingForce ;
        }
    }
}
