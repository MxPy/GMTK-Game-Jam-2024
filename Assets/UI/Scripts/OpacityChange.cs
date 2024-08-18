using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityChange : MonoBehaviour
{
    private Renderer renderer;
    private Material material;
    private Color startColor;
    private Color endColor;
    public float fadeDuration = 1.5f;
    private float startTime;
    private bool isFading = false;

    private void OnEnable()
    {
        renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            startColor = material.color;
            // Changing the alpha only
            endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);
            startColor.a = 0f;
            material.color = startColor;
            isFading = true;
            startTime = Time.time;
        }
    }

    private void Update()
    {
        if (isFading)
        {
            float elapsed = (Time.time - startTime) / fadeDuration;
            float alpha = Mathf.Clamp01(elapsed);
            material.color = Color.Lerp(startColor, endColor, alpha);

            if (alpha >= 1.0f)
            {
                isFading = false;
            }
        }
    }
}