using AnimatedBattleText.Examples;
using PixelBattleText;
using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;



public class ArrowInputUI : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowContainer;
    public Sprite arrowSprite;

    playerController playerController;



    [SerializeField] private ExampleTextManager textManager;
    [Header("---Text Trigger---")]
    [SerializeField] private string triggerMessageWinner;
    [SerializeField] private string triggerMessageLose;
    [Header("---Text use anim---")]
    [SerializeField] public TextAnimation textToUse;
    [SerializeField] public TextAnimation textToUseLose;


    [Header("--Timer---")]
    [SerializeField] private float timeLimit = 10f;
    [SerializeField] private Text timerText;
    private float timeRemaining;
    //private bool isTimerRunning = false;


    public GameObject UIPanel;
    List<ArrowIcon> arrows = new List<ArrowIcon>();
    int currentIndex = 0;
    int sequenceLength = 6;

    bool isInputLocked = false;

    AudioManager audioManager;
    [HideInInspector] public bool isMinigameDone = false;
    [HideInInspector] public bool isWin = false;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {

        playerController = GameObject.FindWithTag("Player").GetComponent<playerController>();
        playerController.EnableControl(false);

        GenerateArrowSequence();

    }

    void Update()
    {
        if (isInputLocked) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) CheckInput(0);
        if (Input.GetKeyDown(KeyCode.DownArrow)) CheckInput(1);
        if (Input.GetKeyDown(KeyCode.UpArrow)) CheckInput(2);
        if (Input.GetKeyDown(KeyCode.RightArrow)) CheckInput(3);
    }

    void GenerateArrowSequence()
    {
        List<int> sequence = new List<int>();

        for (int i = 0; i < sequenceLength; i++)
        {
            int direction = UnityEngine.Random.Range(0, 4); // สุ่มทั้ง 4 แบบ
            sequence.Add(direction);
        }

        // สร้าง UI ลูกศร
        foreach (Transform child in arrowContainer)
        {
            Destroy(child.gameObject);
        }
        arrows.Clear();

        for (int i = 0; i < sequenceLength; i++)
        {
            GameObject arrowObj = Instantiate(arrowPrefab, arrowContainer);
            ArrowIcon icon = arrowObj.GetComponent<ArrowIcon>();
            icon.SetArrow(arrowSprite, sequence[i]);
            arrows.Add(icon);
        }

        currentIndex = 0;
    }

    void CheckInput(int input)
    {
        if (input == arrows[currentIndex].arrowDirection)
        {

            arrows[currentIndex].MarkAsCorrect();
            currentIndex++;

            if (currentIndex >= arrows.Count)
            {
                isMinigameDone = true;
                isWin = true;

                PrimeTween.Sequence.Create()
                   .Chain(PrimeTween.Tween.Scale(arrowContainer, Vector3.one * 1.5f, 0.3f, ease: PrimeTween.Ease.OutBack))
                   .Chain(PrimeTween.Tween.Scale(arrowContainer, Vector3.one, 0.1f));
                audioManager.PlaySFX(audioManager.winner);
                textManager.LastUsed = textToUse;
                textManager.ShowInputText(triggerMessageWinner);

                StartCoroutine(HideUI());

            }
        }
        else
        {
            Lose();
        }
    }
    public void RestartSequenceFromTrigger()
    {

        GenerateArrowSequence();
        currentIndex = 0;
        isInputLocked = false;
        StopAllCoroutines();

    }

    IEnumerator HideUI()
    {
        yield return new WaitForSeconds(1f);
        isInputLocked = false;
        UIPanel.SetActive(false);


        if (playerController != null)
            playerController.canMove = true;

    }

    public void resetCountdown()
    {

        timeRemaining = timeLimit;
        StartCoroutine(StartCountdown());
    }

    // CountDown
    IEnumerator StartCountdown()
    {

        timeRemaining = timeLimit;

        //isTimerRunning = true;

        while (timeRemaining > 0 && !isInputLocked)
        {

            timeRemaining -= Time.deltaTime;

            timerText.text = "TIME : " + Mathf.Ceil(timeRemaining).ToString();

            yield return null;
        }

        if (!isInputLocked)
        {
            Lose();
        }
    }

    public void Lose()
    {
        isInputLocked = true;
        isMinigameDone = true;
        isWin = false;


        audioManager.PlaySFX(audioManager.lose);
        textManager.LastUsed = textToUseLose;
        textManager.ShowInputText(triggerMessageLose);

        arrows[currentIndex].Shake();
        StartCoroutine(HideUI());
    }

}




