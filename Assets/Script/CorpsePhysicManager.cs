using Unity.VisualScripting;
using UnityEngine;

public class CorpsePhysicManager : PhysicElement
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(CharacterManager.Instance.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
    }

    public void Throw() 
    {
        Debug.Log("Throw");
        rb.AddForce(new Vector2(5 * GrabManager.Instance.direction, 10), ForceMode2D.Impulse);
        
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(),GetComponent<Collider2D>(),true);
        }
    }
}
