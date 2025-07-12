using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AimTrainerManager : MonoBehaviour
{
    public Sprite[] targetSprites;
    public RectTransform targetImage;
    private Image targetUIImage; 
    public RectTransform spawnArea;
    public Text resultText;
    public Text countClick;
    public GameObject uiCanvas;

    private float gameDuration = 5f;
    private float targetTimeout = 3f;

    private int targetsClicked = 0;
    private float targetTimer = 0f;
    private float gameTimer = 0f;

    private bool gameRunning = false;

    void Start()
    {
        targetUIImage = targetImage.GetComponent<Image>();
        StartGame();
    }

    void Update()
    {
        if (!gameRunning) return;

        gameTimer += Time.deltaTime;
        targetTimer += Time.deltaTime;

        // ถ้าเลย 3 วิ แล้วยังไม่คลิกเป้า
        if (targetTimer >= targetTimeout)
        {
            GameFail();
        }

        // ถ้าเลยเวลาเกม
        if (gameTimer >= gameDuration)
        {
            GameFail();
        }
    }

    void StartGame()
    {
        countClick.text = "" + +5;
        resultText.gameObject.SetActive(false);
        gameRunning = true;
        gameTimer = 0f;
        targetsClicked = 0;

        SpawnNewTarget();
    }

    public void OnTargetClicked()
    {
        targetsClicked++;
        countClick.text = ""+ (5-targetsClicked);
        targetTimer = 0f;

        if (targetsClicked >= 5)
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
        // สุ่มภาพก่อน
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
        EndGame("WINNER");
    }

    void GameFail()
    {
        EndGame("FAIL");
    }

    void EndGame(string result)
    {
        gameRunning = false;
        targetImage.gameObject.SetActive(false);
        resultText.gameObject.SetActive(true);
        resultText.text = result;

        // ปิด Canvas หลังแสดงผล 1 วินาที
        Invoke(nameof(CloseCanvas), 1f);
    }

    void CloseCanvas()
    {
        uiCanvas.SetActive(false);
    }
}
