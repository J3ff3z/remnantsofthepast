using Unity.VisualScripting;
using UnityEngine;

public class CorpsePhysicManager : PhysicElement
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Throw() 
    {
        Debug.Log("Throw");
        rb.AddForce(new Vector2(5 * GrabManager.Instance.direction, 10), ForceMode2D.Impulse);
        
        
    }
}
