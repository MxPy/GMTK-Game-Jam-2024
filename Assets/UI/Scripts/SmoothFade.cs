using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFade : MonoBehaviour
{
    public Canvas canvas;
    public SpriteRenderer sprite;
    public float delay = 1;
    void Start()
    {
        if (canvas != null && sprite != null)
        {
            sprite.gameObject.SetActive(false);
            canvas.gameObject.SetActive(false);
            StartCoroutine(TimeoutCoroutine(delay));
        }
    }

    IEnumerator TimeoutCoroutine(float delayNum)
    {
        yield return new WaitForSeconds(delayNum);
        SmoothShow();
    }

    IEnumerator TimeoutCoroutinePress(float delayNum)
    {
        yield return new WaitForSeconds(delayNum);
        sprite.gameObject.SetActive(true);
    }


    void SmoothShow()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(true);
            StartCoroutine(TimeoutCoroutinePress(delay + 1));
        }
    }
    void Update()
    {

    }
}
