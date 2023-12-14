using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private Animator camAnimator;
    [SerializeField] private Animator uiAnimator;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject cutsceneUi;
    [SerializeField] private Text messageText;
    [SerializeField] private PlayerInput playerInput;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCutscene();
            StartCoroutine(Message());
        }
    }

    private IEnumerator Message()
    {
        yield return new WaitForSeconds(2f);
        messageText.text = "Thousand devils! Just take a look! This is the map that will lead me to the next treasure island!";
        yield return new WaitForSeconds(4f);
        messageText.text = "Maybe there I'll finally find the golden skull of Blackbeard, the first pirate!";
        yield return new WaitForSeconds(4f);
        messageText.text = "I need to quickly gather all the treasures on this island, grab the map, and get back to the ship!";
        yield return new WaitForSeconds(4f);
        StopCutscene();
    }

    public void StartCutscene()
    {
        playerInput.enabled = false;
        camAnimator.SetBool("startCutscene", true);
        uiAnimator.SetTrigger("FadeIn");
        playerUI.SetActive(false);
        cutsceneUi.SetActive(true);
    }

    public void StopCutscene()
    {
        playerInput.enabled = true;
        camAnimator.SetBool("startCutscene", false);
        uiAnimator.SetTrigger("FadeOut");
        playerUI.SetActive(true);
        cutsceneUi.SetActive(false);
        StopAllCoroutines();
        DeactivateTrigger();
    }

    private void DeactivateTrigger()
    {
        gameObject.SetActive(false);
    }
}
