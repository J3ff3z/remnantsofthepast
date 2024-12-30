using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class GameManager : MonoBehaviour
{
    GameObject savePoint;
    [SerializeField] GameObject corpsePrefab;

    List<GameObject> corpseList;
    private static GameManager instance;
    public static GameManager Instance => instance;

    private bool hasDie = false;

    private void Awake()
    {
        savePoint = GameObject.Find("StartPoint");
        instance = this;
        corpseList = new List<GameObject>();
    }
    private void Start()
    {
        TransitionManager.Instance.StartTransition();
    }

    public void DieHere(Vector3 _currentPosition, bool isCharac = true)
    {
        if (!hasDie)
        {
            GameObject newCorpse = Instantiate(corpsePrefab);
            newCorpse.transform.position = _currentPosition;
            corpseList.Add(newCorpse);
            if (isCharac)
            {
                CharacterManager.Instance.Restart(savePoint.transform.position);
            }
            hasDie = true;
            StartCoroutine(ResetDeath());
        }
    }

    public void CleanCorpse()
    {
        foreach (GameObject corpse in corpseList) { Destroy(corpse); }
        corpseList.Clear();
    }

    public void CleanCorpse(GameObject corpse)
    {
        corpseList.Remove(corpse);
        Destroy(corpse);
    }

    public void SwitchSavePoint(Vector2 position)
    {
        savePoint.transform.position = position;
    }
    IEnumerator ResetDeath()
    {
        yield return new WaitForSeconds(0.5f);
        hasDie = false;
    }
}
