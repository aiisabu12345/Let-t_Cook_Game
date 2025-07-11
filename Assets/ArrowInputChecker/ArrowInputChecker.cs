﻿using AnimatedBattleText.Examples;
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
    [SerializeField] private string triggerMessage;
    [SerializeField] public TextAnimation textToUse;

    public GameObject UIPanel;
    List<ArrowIcon> arrows = new List<ArrowIcon>();
    int currentIndex = 0;
    int sequenceLength = 6;

    bool isInputLocked = false;

    AudioManager audioManager;

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
        List<int> sequence = new List<int> { 0, 1, 2, 3 };
        // สลับลำดับแบบสุ่ม
        for (int i = sequence.Count - 1; i > 0; i--)
        {
            sequence.Add(UnityEngine.Random.Range(0, sequenceLength));
         
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
                PrimeTween.Sequence.Create()
                   .Chain(PrimeTween.Tween.Scale(arrowContainer, Vector3.one * 1.5f, 0.3f, ease: PrimeTween.Ease.OutBack))
                   .Chain(PrimeTween.Tween.Scale(arrowContainer, Vector3.one, 0.1f));
                audioManager.PlaySFX(audioManager.winner);
                textManager.LastUsed = textToUse;
                textManager.ShowInputText(triggerMessage);
                StartCoroutine(closeUI());
               
            }
        }
        else
        {
            isInputLocked = true;

            arrows[currentIndex].Shake();
            StartCoroutine(closeUI());
        }
    }
    public void RestartSequenceFromTrigger()
    {

        GenerateArrowSequence();
        currentIndex = 0;
        isInputLocked = false; 
    }

    IEnumerator closeUI()
    {
        yield return new WaitForSeconds(1f);
        isInputLocked = false;
        UIPanel.SetActive(false);

        if (playerController != null)
            playerController.canMove = true;

    }
}




