

using UnityEngine;

public class GroundManager : MonoBehaviour
{
    [SerializeField]
    private float radius = 0.2f;

    public bool isGrounded;

    private static GroundManager instance;
    public static GroundManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        isGrounded = false;
        foreach(Collider2D collider in Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius))
        {
            if(!collider.gameObject.CompareTag("Player"))
            {
                isGrounded = true;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}