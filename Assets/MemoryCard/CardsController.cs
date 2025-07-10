using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using System.Text.RegularExpressions;
using static UnityEngine.GraphicsBuffer;
using AnimatedBattleText.Examples;
using PixelBattleText;

public class CardsController : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Sprite[] sprites;
    private List<Sprite> spritesList;


    [SerializeField] private ExampleTextManager textManager;
    [SerializeField] private string triggerMessage;
    [SerializeField] public TextAnimation textToUse;



    public GameObject targetUI;

    Card firstSelected;
    Card secondSelected;

    int matchCounts;

    playerController playerController;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<playerController>();
        playerController.EnableControl(false);
        PrepareSprites();
       CreateCards();
    }
    private void PrepareSprites()
    {
        spritesList = new List<Sprite>();   

        for(int i = 0; i < sprites.Length; i++)
        {
            // add sprite 2 time
            spritesList.Add(sprites[i]);
            spritesList.Add(sprites[i]);

        }
        ShuffleSprites(spritesList);
    }
    
    void CreateCards()
    {
      

        for (int i = 0; i < spritesList.Count; i++)
        {
            Card card = Instantiate(cardPrefab, gridTransform);
            card.SetIconSprite(spritesList[i]);
            card.controller = this;
        }
    }
 

    public void SetSelected(Card card)
    {
        if(card.isSelected == false)
        {
            card.Show();
            // เลือกอันแรกเสร้จ ก้เด้งออก
            if(firstSelected == null)
            {
                firstSelected = card;
                return;
            }

            if (secondSelected == null)
            {
                secondSelected = card;
                StartCoroutine(CheckMatching(firstSelected, secondSelected));

                //รีค่า
                firstSelected = null;
                secondSelected = null;
           
            }
        }
    }
    IEnumerator CheckMatching(Card a, Card b)
    {
        yield return new WaitForSeconds(0.4f);

        if (a.iconSprite == b.iconSprite)
        {

            // Matched
            matchCounts++;

            if (matchCounts >= spritesList.Count/2f) {
                // ตอนเก็บครบแล้ว จะให้ทำไร ในนี้เลย
                audioManager.PlaySFX(audioManager.winner);
                textManager.LastUsed = textToUse;
                textManager.ShowInputText(triggerMessage);
                PrimeTween.Sequence.Create()
                    .Chain(PrimeTween.Tween.Scale(gridTransform, Vector3.one * 1.5f, 0.2f, ease: PrimeTween.Ease.OutBack))
                    .Chain(PrimeTween.Tween.Scale(gridTransform, Vector3.one, 0.1f));


       
                StartCoroutine(HideUI());

            }
            else
            {
                audioManager.PlaySFX(audioManager.correct);
            }
        }
        else
        {
            // filp them back
            a.Hide();
            b.Hide();
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
        StopAllCoroutines(); 
        foreach (Transform child in gridTransform)
        {
            Destroy(child.gameObject);
        }

        firstSelected = null;
        secondSelected = null;
        matchCounts = 0;

        if (playerController != null)
        {
            playerController.EnableControl(false);
        }

        if (!targetUI.activeSelf)
        {
            targetUI.SetActive(true);
        }

        PrepareSprites();
        CreateCards();

    }
   


    void ShuffleSprites(List<Sprite> spriteslist)
    {
        for (int i = spriteslist.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            Sprite temp = spriteslist[i];
            spriteslist[i] = spriteslist[randomIndex];
            spriteslist[randomIndex] = temp;
        }


    }

}
