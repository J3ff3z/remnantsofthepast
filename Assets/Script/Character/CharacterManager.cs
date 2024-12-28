using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterManager : PhysicElement
{
    [SerializeField]
    private float decreaseJumpingForce;
    [SerializeField]
    private float defaultJumpingForce;

    private CharacterController controller;
    private float m_Move;

    private float jumpForce;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_Move = context.ReadValue<float>();
        if(m_Move < 0)
        {
            GrabManager.Instance.direction = -1;
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        if(m_Move > 0)
        {
            GrabManager.Instance.direction = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void OnJump()
    {
        if (controller.isGrounded)
        {
            jumpForce = defaultJumpingForce;
        }
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GrabManager.Instance.OnGrab();
        }
        if (context.canceled)
        {
            GrabManager.Instance.OnDrop();
        }
    }

    public void OnInteract()
    {

    }

    public void OnClean()
    {

    }



    private void Update()
    {
        DecreasingForce();

        Vector2 movingVector = new Vector2(m_Move * Time.deltaTime * speed, (-gravity + jumpForce )* Time.deltaTime);

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
