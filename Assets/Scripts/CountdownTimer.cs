using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 5f; // ตั้งค่าเวลาเริ่มต้น
    private float currentTime;
    private bool timerRunning = false;

    public Text countdownText;

    void Update()
    {
        if (!timerRunning) return;

        currentTime -= Time.deltaTime;
        countdownText.text = "Time Left: " + Mathf.Ceil(currentTime).ToString();

        if (currentTime <= 0f)
        {
            timerRunning = false;
            countdownText.text = "Time Left: 0";

            // ถ้าต้องการ เรียก GameFail จาก AimTrainerManager (ตามโค้ดคุณ)
            var manager = FindObjectOfType<AimTrainerManager>();
            if (manager != null && !manager.isMinigameDone)
            {
                manager.GameFail();
            }
        }
    }

    public void StartTimer()
    {
        currentTime = startTime;
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = startTime;
        timerRunning = true;
        if (countdownText != null)
        {
            countdownText.text = "Time Left: " + Mathf.Ceil(currentTime).ToString();
        }
    }
}
