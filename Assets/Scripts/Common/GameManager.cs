using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Total coins")]
    private int totalSilver;
    private int collectedSilver;
    private int totalGold;
    private int collectedGold;
    private int totalDiamonds;
    private int collectedDiamonds;

    [Header("Score params")]
    private int score = 0;
    [SerializeField] private Text scoreText;

    [Header("Map params")]
    public int piecesOfMap;

    [Header("SFX")]
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip looseSound;
    [SerializeField] private AudioClip mapFound;

    [Header("References")]
    [SerializeField] private ShipController shipController;
    [SerializeField] private GameObject looseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private UIController uiController;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        totalSilver = GameObject.FindGameObjectsWithTag("SilverCoin").Length;
        totalGold = GameObject.FindGameObjectsWithTag("GoldCoin").Length;
        totalDiamonds = GameObject.FindGameObjectsWithTag("Diamond").Length;
    }

    public void AddScore(string coinType, int value)
    {
        switch (coinType)
        {
            case "SilverCoin":
                score += value;
                collectedSilver++;
                break;
            case "GoldCoin":
                score += value;
                collectedGold++;
                break;
            case "Diamond":
                score += value;
                collectedDiamonds++;
                break;
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void CollectMapPieces(int count)
    {
        piecesOfMap += count;

        if (piecesOfMap >= 4)
            SoundManager.instance.PlaySound(mapFound);
            shipController.SetLevelComplete();
    }

    public void LevelComplete()
    {
        winScreen.SetActive(true);
        SoundManager.instance.PlaySound(winSound);

        uiController.UpdateScoreText(totalSilver, collectedSilver, "Silver");
        uiController.UpdateScoreText(totalGold, collectedGold, "Gold");
        uiController.UpdateScoreText(totalDiamonds, collectedDiamonds, "Diamond");
    }

    public void LevelFailed()
    {
        looseScreen.SetActive(true);
        SoundManager.instance.PlaySound(looseSound);
    }












    //private void ShowDebugInfo()
    //{
    //    Debug.Log($"Total silver on level: {totalSilver}, gathered: {gatheredSilver}, ratio: {gatheredSilver / totalSilver * 100}");
    //    Debug.Log($"Total silver on level: {totalGold}, gathered: {gatheredGold}, ratio: {gatheredGold / totalGold * 100}");
    //    Debug.Log($"Total silver on level: {totalDiamonds}, gathered: {gatheredDiamonds}, ratio: {gatheredDiamonds / totalDiamonds * 100}");
    //}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            //ShowDebugInfo();
        }
    }
}
