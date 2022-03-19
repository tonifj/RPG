using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillBar : MonoBehaviour
{
    float percentage;   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = percentage;
    }

    public void SetPercentage(float p)
    {
        percentage = p;
        gameObject.GetComponentInChildren<TMP_Text>().text = "Accuracity " + (GetComponent<Image>().fillAmount * 100).ToString() + "%";
    }
}
