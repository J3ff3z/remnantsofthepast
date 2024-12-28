using Unity.VisualScripting;
using UnityEngine;

public class CorpsePhysicManager : PhysicElement
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Throw() 
    {
        Debug.Log("Throw");
        rb.AddForce(new Vector3(5 * GrabManager.Instance.direction, 10, 0), ForceMode.Impulse);
        
        
    }
}
