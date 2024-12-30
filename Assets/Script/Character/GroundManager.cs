

using System;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    [SerializeField]
    private float radius = 0.2f;

    public bool isGrounded;

    private static GroundManager instance;
    public static GroundManager Instance => instance;

    private Collider2D lastCorpse;
    private Collider2D[] colliders;

    private void Awake()
    {
        instance = this;
        colliders = new Collider2D[2];
    }

    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircleAll(transform.position, radius).Length != colliders.Length)
        {
            bool increase = colliders.Length <= Physics2D.OverlapCircleAll(transform.position, radius).Length;
            colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius);
            Trigger(increase);
        }
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius))
        {
            if (!collider.gameObject.CompareTag("Player"))
            {
                isGrounded = true;
            }
        }
    }
    void Trigger(bool increase)
    {
        foreach (Collider2D collider in colliders)
        {
            if (!collider.gameObject.CompareTag("Player"))
            {
                isGrounded = true;
            }
            if (collider.gameObject.layer == 7)
            {
                Debug.Log("Collide");
                Physics2D.IgnoreCollision(collider, transform.parent.GetComponent<Collider2D>(), false);
                lastCorpse = collider;
            }
        }
        if (!increase)
        {
            isGrounded = false;
        }
        if (!increase && lastCorpse!=null)
        {
            Physics2D.IgnoreCollision(lastCorpse, transform.parent.GetComponent<Collider2D>(), true);
            lastCorpse = null;
        }
    }

}