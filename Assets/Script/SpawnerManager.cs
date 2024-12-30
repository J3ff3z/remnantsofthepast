using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField]
    private float gravity;

    private Rigidbody2D controller;


    private Vector3 startingPosition;

    private void Awake()
    {
        Application.targetFrameRate = 30;
        controller = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 movingVector = new Vector3(0, (-gravity) * Time.fixedDeltaTime,0);
        if (movingVector != Vector3.zero)
        {
            controller.MovePosition(movingVector + transform.position);
        }
    }

    public void Restart()
    {
        transform.position = startingPosition;
    }

}
