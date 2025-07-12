using UnityEngine;
using PrimeTween;

public class SlideFromRight : MonoBehaviour
{
    public RectTransform target;         // UI ที่จะ slide
    public float slideDistance = 1000f;  // ระยะที่ slide เข้ามา (เช่น 1000 units จากขวา)
    public float duration = 0.5f;

    void Start()
    {
        SlideInFromRight();
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
}
