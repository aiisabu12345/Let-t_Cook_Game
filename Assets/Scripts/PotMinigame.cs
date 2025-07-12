using UnityEngine;
using UnityEngine.UI;
using AnimatedBattleText.Examples;
using PixelBattleText;
using System.Collections;
using System.Collections.Generic;

public class PotMinigame : MonoBehaviour
{
    public Slider succes;
    public Slider fail;
    public Slider inputSlider;
    public Transform panel;

    [SerializeField] private ExampleTextManager textManager;
    [Header("---Text Trigger---")]
    [SerializeField] private string triggerMessageWinner;
    [SerializeField] private string triggerMessageLose;
    [Header("---Text use anim---")]
    [SerializeField] public TextAnimation textToUse;
    [SerializeField] public TextAnimation textToUseLose;

    private Coroutine countdownCoroutine;
    public bool isInputLocked = false;

    [Header("--Timer---")]
    [SerializeField] private float timeLimit = 5f;
    [SerializeField] private Text timerText;
    private float timeRemaining;
    //private bool isTimerRunning = false;


    public GameObject targetUI;

    int matchCounts;

    playerController playerController;

    AudioManager audioManager;

    public float succesincreaseRate;
    public float failincreaseRate;

    public float succesValue = 0f;
    public float failValue = 0f;

    public Animator animator;
    public Image potImage;

    bool end = false;
    [HideInInspector] public bool isMinigameDone = false;
    [HideInInspector] public bool isWin = false;


    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
        animator = potImage.GetComponent<Animator>();

    }

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<playerController>();
        playerController.EnableControl(false);
    }

    void Update()
    {
        if (!end)
        {
            if (inputSlider.value >= 66)
            {
                succesincreaseRate = 22f;
                failincreaseRate = 25f;
                animator.SetTrigger("HighFire");
            }
            else if (inputSlider.value >= 33)
            {
                succesincreaseRate = 17f;
                failincreaseRate = 20f;
                animator.SetTrigger("MidFire");
            }
            else if (inputSlider.value > 0)
            {
                succesincreaseRate = 7f;
                failincreaseRate = 10f;
                animator.SetTrigger("LowFire");
            }
            else
            {
                succesincreaseRate = -4f;
                failincreaseRate = -7f;
                animator.SetTrigger("NoFire");
            }

            succesValue += succesincreaseRate * Time.deltaTime;
            failValue += failincreaseRate * Time.deltaTime;

            succesValue = Mathf.Clamp(succesValue, 0, 100);
            failValue = Mathf.Clamp(failValue, 0, 100);

            fail.value = failValue;
            succes.value = succesValue;

            if (succesValue == 100)
            {
                end = true;
                Win();
            }
            else if (failValue == 100)
            {
                end = true;
                Lose();
                animator.SetTrigger("Broken");
            }
        }
    }

    IEnumerator HideUI()
    {

        yield return new WaitForSeconds(1.3f);
        RestartSequenceFromTrigger();
        targetUI.SetActive(false);

        if (playerController != null)
            playerController.EnableControl(true);
    }

    public void RestartSequenceFromTrigger()
    {
        Debug.Log("reset");
        StopAllCoroutines();
        isInputLocked = false;
        //isTimerRunning = false;
        timeRemaining = timeLimit;
        succes.value = 0;
        fail.value = 0;
        inputSlider.value = 0;
        succesValue = 0;
        failValue = 0;
        end = false;

        if (playerController != null)
        {
            playerController.EnableControl(false);
        }

        if (!targetUI.activeSelf)
        {
            targetUI.SetActive(true);
        }
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

        // isInputLocked = false;
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

        StartCoroutine(HideUI());
    }

    public void Win()
    {
        isMinigameDone = true;
        isWin = true;
        audioManager.PlaySFX(audioManager.winner);
        textManager.LastUsed = textToUse;
        textManager.ShowInputText(triggerMessageWinner);
        PrimeTween.Sequence.Create()
            .Chain(PrimeTween.Tween.Scale(panel, Vector3.one * 1.5f, 0.2f, ease: PrimeTween.Ease.OutBack))
            .Chain(PrimeTween.Tween.Scale(panel, Vector3.one, 0.1f));

        resetCountdown();
        StartCoroutine(HideUI());
    }
}
