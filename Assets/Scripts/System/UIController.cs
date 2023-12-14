using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text silverScoreText;
    [SerializeField] private Text goldScoreText;
    [SerializeField] private Text diamondScoreText;

    [Header("SFX")]
    [SerializeField] private AudioClip clickSound;

    public void OnLevelBtnClick(int id)
    {
        if(id > (SceneManager.sceneCountInBuildSettings -1))
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(id);

    }

    public void OnMenuBtnClick()
    {
        OnLevelBtnClick(0);
    }

    public void OnRestartBtnClick()
    {
        OnLevelBtnClick(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnNextLvlBtnClick()
    {
        OnLevelBtnClick(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UpdateScoreText(int total, int collected, string coinName)
    {
        switch (coinName)
        {
            case "Silver":
                silverScoreText.text = collected.ToString() + "/" + total.ToString();
                break;
            case "Gold":
                goldScoreText.text = collected.ToString() + "/" + total.ToString();
                break;
            case "Diamond":
                diamondScoreText.text = collected.ToString() + "/" + total.ToString();
                break;
        }
    }

    public void PlayClickSound()
    {
        SoundManager.instance.PlaySound(clickSound);
    }
}
