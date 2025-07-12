using AnimatedBattleText.Examples;
using PixelBattleText;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AimTrainerManager : MonoBehaviour
{
    public Sprite[] targetSprites;
    public RectTransform targetImage;
    private Image targetUIImage;
    public RectTransform spawnArea;
    public Text resultText;
    public Text countClick;
    public GameObject uiCanvas;

    private float gameDuration = 6f;
    private float targetTimeout = 3f;

    private int targetsClicked = 0;
    private float targetTimer = 0f;
    private float gameTimer = 0f;

    private bool gameRunning = false;

    // เพิ่มสำหรับ PotionCraftManager
    public bool isMinigameDone = false;
    public bool isWin = false;

    [SerializeField] private ExampleTextManager textManager;
    [Header("---Text Trigger---")]
    [SerializeField] private string triggerMessageWinner;
    [SerializeField] private string triggerMessageLose;
    [Header("---Text use anim---")]
    [SerializeField] public TextAnimation textToUse;
    [SerializeField] public TextAnimation textToUseLose;


    AudioManager audioManager;

    public CountdownTimer countdownTimer;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        targetUIImage = targetImage.GetComponent<Image>();
        // ไม่ต้องเริ่มเกมตรงนี้แล้ว
        // StartGame();
    }

    void Update()
    {
        if (!gameRunning) return;

        gameTimer += Time.deltaTime;
        targetTimer += Time.deltaTime;

        if (targetTimer >= targetTimeout)
        {
            GameFail();
        }

        if (gameTimer >= gameDuration)
        {
            GameFail();
        }
    }

    public void resetCountdown()
    {
        isMinigameDone = false;
        isWin = false;

        uiCanvas.SetActive(true);
        countClick.text = "Count : 6" ;
        resultText.gameObject.SetActive(false);

        gameRunning = true;
        gameTimer = 0f;
        targetTimer = 0f;
        targetsClicked = 0;

        SpawnNewTarget();

        // ✅ เรียกรีเซ็ต countdown timer
        if (countdownTimer != null)
        {
            countdownTimer.ResetTimer();
        }
    }


    public void OnTargetClicked()
    {
        targetsClicked++;
        countClick.text = "Count : " + (6 - targetsClicked).ToString();
        targetTimer = 0f;

        if (targetsClicked >= 6)
        {
            GameWin();
        }
        else
        {
            SpawnNewTarget();
        }
    }

    void SpawnNewTarget()
    {
        if (targetSprites.Length > 0)
        {
            Sprite randomSprite = targetSprites[Random.Range(0, targetSprites.Length)];
            targetUIImage.sprite = randomSprite;
        }

        targetImage.gameObject.SetActive(true);

        Vector2 areaSize = spawnArea.rect.size;
        Vector2 targetSize = targetImage.rect.size;

        float maxX = (areaSize.x - targetSize.x) / 2f;
        float maxY = (areaSize.y - targetSize.y) / 2f;

        float randX = Random.Range(-maxX, maxX);
        float randY = Random.Range(-maxY, maxY);

        targetImage.anchoredPosition = new Vector2(randX, randY);
    }

    void GameWin()
    {
        isMinigameDone = true;
        isWin = true;
        EndGame("WINNER");
        audioManager.PlaySFX(audioManager.winner);
        textManager.LastUsed = textToUse;
        textManager.ShowInputText(triggerMessageWinner);
        if (countdownTimer != null)
        {
            countdownTimer.StopTimer();
        }
        else
        {
            Debug.LogWarning("CountdownTimer is missing!");
        }
    }

    public void GameFail()
    {
        audioManager.PlaySFX(audioManager.lose);
        textManager.LastUsed = textToUseLose;
        textManager.ShowInputText(triggerMessageLose);
        isMinigameDone = true;
        isWin = false;
        EndGame("FAIL");
    }

    void EndGame(string result)
    {
        gameRunning = false;
        targetImage.gameObject.SetActive(false);
        resultText.gameObject.SetActive(true);
        resultText.text = result;

        Invoke(nameof(CloseCanvas), 1f);
    }

    void CloseCanvas()
    {
        uiCanvas.SetActive(false);
    }
}
