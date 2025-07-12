using UnityEngine;
using PrimeTween;

public class SlideFromRight : MonoBehaviour
{
    public RectTransform target;         // UI ที่จะ slide
    public float slideDistance = 1000f;  // ระยะที่ slide เข้ามา (เช่น 1000 units จากขวา)
    public float duration = 0.5f;

    private bool hasSlidIn = false;      // flag กันไม่ให้ทำซ้ำ

    private void Update()
    {
        if (!hasSlidIn)
        {
            SlideInFromRight();
            hasSlidIn = true;
        }
    }

    public void SlideInFromRight()
    {
        Vector2 targetPos = target.anchoredPosition;
        Vector3 startPos = (Vector3)(targetPos + new Vector2(slideDistance, 0));

        target.localPosition = startPos;

        Tween.LocalPosition(
            target,
            endValue: (Vector3)targetPos,
            duration: duration,
            ease: Ease.OutCubic
        );
    }

    public void ResetSlide()
    {
        hasSlidIn = false;
    }
}
