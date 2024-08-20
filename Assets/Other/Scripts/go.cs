using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class go : MonoBehaviour
{
    public string sceneName;
    public VariableTimer timer;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        timer = gameObject.AddComponent(typeof(VariableTimer)) as VariableTimer;
        timer.StartTimer(15);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown || timer.finished){
            SceneManager.LoadScene(sceneName);
        }
    }
}
