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

    public Dialogue DialogueManager;

    private static CharacterManager instance;
    public static CharacterManager Instance => instance;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        instance = this;
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
        if (DialogueManager != null)
        {
            DialogueManager.OnInteract();
        }
    }

    public void OnClean()
    {
        GameManager.Instance.CleanCorpse();
    }



    private void Update()
    {
        DecreasingForce();

        Vector2 movingVector = new Vector2(m_Move * Time.deltaTime * speed, (-gravity + jumpForce )* Time.deltaTime);

        controller.Move(movingVector);
    }

    public void Restart(Vector3 position)
    {
        controller.enabled = false;
        transform.position = position;
        controller.enabled = true;
    }

    void DecreasingForce()
    {
        if(jumpForce > 0)
        {
            jumpForce -= decreaseJumpingForce ;
        }
    }
}
