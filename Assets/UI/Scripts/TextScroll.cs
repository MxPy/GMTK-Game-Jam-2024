using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScroll : MonoBehaviour
{
    public Transform scrollingText;
    public Button ExitButton;
    public Transform MenuContainer;
    public float speed = 1.0f;
    public int stopPositionY = 3;

    public float timeToShow = 3f;

    private bool hasStopped = false;

    void Start()
    {
        MenuContainer.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        Invoke("ShowButton", timeToShow);
    }

    void ShowButton()
    {
        ExitButton.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!hasStopped)
        {
            scrollingText.position += new Vector3(0, speed * Time.deltaTime, 0);
            Debug.Log(scrollingText.position.y);
            if (scrollingText.position.y >= stopPositionY)
            {
                Debug.Log("in cond");
                scrollingText.position = new Vector3(scrollingText.position.x, stopPositionY, scrollingText.position.z);
                hasStopped = true;
            }
        }
        else
        {
            ExitButton.gameObject.SetActive(false);
            scrollingText.gameObject.SetActive(false);
            MenuContainer.gameObject.SetActive(true);
        }
    }
}