using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] AnimationCurve opacity;

    float time = 0;
    
    void Start()
    {
        StartCoroutine(TextAnimation());
    }

    IEnumerator TextAnimation()
    {
        while(true)
        {
            if(time >= 2)
                time = 0;
            time += Time.deltaTime;
            loadingText.color = new Color(loadingText.color.r, loadingText.color.b, loadingText.color.g, opacity.Evaluate(time));
            yield return null;
        }   
    }
}
