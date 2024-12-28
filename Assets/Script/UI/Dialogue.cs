using UnityEditorInternal;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Linq;
using UnityEngine.InputSystem;



//S'active au dessus du personnage quand on s'en approche
public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;

    private int index;
    private bool coroutineEnCours = false;


    private void OnEnable()
    {
        textComponent.text = "";
        StartDialogue();
    }
    
    private void OnDisable()
    {
        textComponent.text = "";
    }


    private void Update()
    {
        //A MODIFIER
        if (Input.GetKeyDown(KeyCode.Space) && !coroutineEnCours) NextLine();
    }


    private void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            coroutineEnCours = true;
            textComponent.text += c;
            yield return new WaitForSeconds(1/textSpeed);
            coroutineEnCours = false;
        }
    }

    //Appel avec une touche pour changer de ligne si dans la hitbox du dialogue
    void NextLine()
    {
        if (index < lines.Count() - 1)
        {
            index++;
            textComponent.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
