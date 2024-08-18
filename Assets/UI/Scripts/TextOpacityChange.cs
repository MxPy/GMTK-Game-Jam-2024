using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextOpacityChange : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private Color startColor;
    private Color endColor;
    public float fadeDuration = 1.5f;
    private float startTime;
    private bool isFading = false;

    private void OnEnable()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro != null)
        {
            startColor = textMeshPro.color;
            endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);
            startColor.a = 0f;
            textMeshPro.color = startColor;
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
            textMeshPro.color = Color.Lerp(startColor, endColor, alpha);

            if (alpha >= 1.0f)
            {
                isFading = false;
            }
        }
    }
}
