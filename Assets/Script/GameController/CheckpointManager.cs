using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SwitchSavePoint(transform.position);
        }
    }

}
