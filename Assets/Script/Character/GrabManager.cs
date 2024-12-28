using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    public int direction =1;

    private bool canGrab;
    [SerializeField]
    private float radius =0.45f;
    [SerializeField]
    private string grabbableTag;


    private Collider[] colliders;
    private GameObject grabbableObject;
    [SerializeField]
    private GameObject grabbedObject;

    // Instance to Access Grab Manager from other script
    private static GrabManager instance;
    public static GrabManager Instance => instance;

    private void Awake()
    {
        colliders = new Collider[2];

        instance = this;
    }

    private void Update()
    {
        if (Physics.OverlapSphere(transform.position, radius).Length != colliders.Length) {
            colliders = Physics.OverlapSphere(transform.position, radius);
            Trigger();
        }
        if (grabbedObject != null)
        {
            grabbedObject.transform.position = transform.parent.position;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void Trigger()
    {
        canGrab = false;
        foreach (Collider collider in colliders)
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
            grabbedObject.layer = LayerMask.NameToLayer("CorpseGrabbed");
        }
    }

    public void OnDrop()
    {
        if (canGrab)
        {
            grabbedObject.GetComponent<CorpsePhysicManager>().Throw();
            StartCoroutine(Untrigger(grabbedObject));
            grabbedObject = null;
            canGrab = false;
        }
    }

    IEnumerator Untrigger(GameObject go)
    {
        yield return new WaitForSeconds(0.5f);
        go.layer = LayerMask.NameToLayer("Default");
        //grabbedObject.GetComponent<Collider>().isTrigger = false;
    }
}
