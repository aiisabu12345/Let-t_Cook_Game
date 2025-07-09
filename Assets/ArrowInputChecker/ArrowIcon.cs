using PrimeTween;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class ArrowIcon : MonoBehaviour
{
    public int arrowDirection; 
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetArrow(Sprite sprite, int direction)
    {
        arrowDirection = direction;
        image.sprite = sprite;

        // หมุนภาพให้ตรงทิศ
        transform.rotation = Quaternion.Euler(0, 0, GetRotationFromDirection(direction));
        image.color = Color.white;
    }

    float GetRotationFromDirection(int dir)
    {
        switch (dir)
        {
            case 0: return 180f; // Left
            case 1: return -90f; // Down
            case 2: return 90f;  // Up
            case 3: return 0f;   // Right
            default: return 0f;
        }
    }

    public void MarkAsCorrect()
    {
        image.color = Color.green; 
    }


    // Shake
    public void Shake()
    {
        image.color = Color.red;
        Tween.ShakeLocalRotation(transform, strength: new Vector3(0, 0, 15), duration: 1f, frequency: 10);
    }
}
