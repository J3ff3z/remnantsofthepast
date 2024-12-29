

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
        foreach(Collider collider in Physics.OverlapSphere(transform.position, radius)){
            if(collider.gameObject.tag != "Player")
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