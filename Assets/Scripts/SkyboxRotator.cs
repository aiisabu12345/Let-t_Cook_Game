using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 1f; // ปรับความเร็วการหมุน

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
