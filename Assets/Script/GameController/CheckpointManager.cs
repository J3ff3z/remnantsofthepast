using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SwitchSavePoint(transform.position);
        }
    }

}
