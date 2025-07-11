using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class NewsManager : MonoBehaviour
{
    public static NewsManager Instance;
    public List<NewsData> newsSheet = new List<NewsData>();
    public float maxTime;
    public float currentTime;
    public TMP_Text title;
    public TMP_Text head;
    public TMP_Text timerText;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentTime = maxTime;

    }

    void Start()
    {
        SetBuff();
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            SetBuff();
            currentTime = maxTime;
        }

        timerText.text = "เหลือเวลา : " + Mathf.FloorToInt(currentTime).ToString();
    }

    public void SetBuff()
    {
        if (SellManager.Instance.buff.Count > 0)
        {
            SellManager.Instance.buff.Clear();
        }
        int rand = Random.Range(0, 5);
        SellManager.Instance.buff.Add(newsSheet[rand].buff);

        head.text = newsSheet[rand].head;
        title.text = newsSheet[rand].title;

    }
}
