using UnityEngine;

public class PNJManager : MonoBehaviour
{
    [SerializeField] GameObject customDialogue;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.tag == "Player")
        {
            customDialogue.SetActive(true);
            CharacterManager.Instance.DialogueManager = customDialogue.GetComponent<Dialogue>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        customDialogue.SetActive(false);
        CharacterManager.Instance.DialogueManager = null;
    }
}
