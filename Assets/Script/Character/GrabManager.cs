using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    public int direction =1;

    public bool canGrab;
    [SerializeField]
    private float radius =0.45f;
    [SerializeField]
    private string grabbableTag;


    private Collider2D[] colliders;
    private GameObject grabbableObject;
    [SerializeField]
    private GameObject grabbedObject;

    // Instance to Access Grab Manager from other script
    private static GrabManager instance;
    public static GrabManager Instance => instance;

    private void Awake()
    {
        colliders = new Collider2D[2];

        instance = this;
    }

    private void Update()
    {
        if (Physics2D.OverlapCircleAll(transform.position, radius).Length != colliders.Length) {
            colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius);
            Trigger();
        }
        if (grabbedObject != null)
        {
            grabbedObject.transform.position = transform.parent.position;
        }
    }

    private void Trigger()
    {
        canGrab = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == grabbableTag)
            {
                canGrab = true;
                grabbableObject = collider.gameObject;
                return;
            }
        }
    }

    public void OnGrab()
    {
        if (canGrab)
        {
            grabbedObject = grabbableObject;
            grabbedObject.transform.position = transform.parent.position;
        }
    }

    public void OnDrop()
    {
        if (grabbedObject != null)
        {
            grabbedObject.GetComponent<CorpsePhysicManager>().Throw(direction);
            grabbedObject = null;
            canGrab = false;
        }
    }
}
