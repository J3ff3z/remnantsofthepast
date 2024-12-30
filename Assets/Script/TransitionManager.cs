using UnityEngine;

public class TransitionManager : MonoBehaviour
{

    private static TransitionManager instance;
    public static TransitionManager Instance => instance;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 showPosition;
    [SerializeField]
    private Vector3 hidePosition;

    private Vector3 goingToPosition;

    private void Awake()
    {
        goingToPosition = transform.position;
        instance = this;
    }


    private void FixedUpdate()
    {
        if (Mathf.Abs(goingToPosition.x - transform.localPosition.x) > 25)
        {
            Vector3 normalizePosition = (goingToPosition - transform.localPosition);
            normalizePosition.Normalize();
            transform.localPosition += normalizePosition * Time.fixedDeltaTime * speed;
        }
    }
    [ContextMenu("End")]
    public void EndTransition()
    {
        goingToPosition = showPosition;
    }

    [ContextMenu("Start")]
    public void StartTransition()
    {
        goingToPosition = hidePosition;
    }
}
