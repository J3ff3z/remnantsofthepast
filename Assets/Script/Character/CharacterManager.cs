using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    private float m_Move;

    private float jumpForce;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_Move = context.ReadValue<float>();
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
        if (context.canceled)
        {
            Debug.Log("Drop");
            return;
        }
        if (context.performed)
        {
            Debug.Log("Grab");
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
