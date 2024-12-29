using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterManager : PhysicElement
{
    [SerializeField]
    private float decreaseJumpingForce;
    [SerializeField]
    private float defaultJumpingForce;

    private Rigidbody2D controller;
    private float m_Move;

    private float jumpForce;

    public Dialogue DialogueManager;

    private static CharacterManager instance;
    public static CharacterManager Instance => instance;
    private void Awake()
    {
        controller = GetComponent<Rigidbody2D>();
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
        if (GroundManager.Instance.isGrounded)
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
    }

    void FixedUpdate()
    {
        Vector3 movingVector = new Vector3(m_Move * Time.fixedDeltaTime * speed, (-gravity + jumpForce) * Time.fixedDeltaTime,0);

        controller.MovePosition(movingVector + transform.position);
    }

    public void Restart(Vector3 position)
    {
        transform.position = position;
    }

    void DecreasingForce()
    {
        if(jumpForce > 0)
        {
            jumpForce -= decreaseJumpingForce ;
        }
    }

}
