using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharater : MonoBehaviour
{
    public float displayTime = 4f;
    public GameObject dialogBox;
    float timeDisplay;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        timeDisplay = -1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDisplay >= 0)
        {
            timeDisplay -= Time.deltaTime;
            if (timeDisplay < 0)
            {
                dialogBox.SetActive(false);
            }

        }
        
    }
    public void DisplayDialog()
    {
        timeDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}
