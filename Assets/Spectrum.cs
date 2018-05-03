using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Spectrum : MonoBehaviour
{
    List<Image> SpectrumBars = new List<Image>();
    public int SpectrumBarSize;
    private void Awake()
    {
        CreateSpectrumBar();

    }
    void CreateSpectrumBar()
    {
        var parentObj = FindObjectOfType<Canvas>();
        float rotaionvalue = 360f / SpectrumBarSize;
        Debug.Log(rotaionvalue);
        for (int i = 0; i < SpectrumBarSize; i++)
        {

            GameObject bar = new GameObject();
            Image NewImage = bar.AddComponent<Image>();
          
            var newTransform = bar.GetComponent<RectTransform>();
            newTransform.SetParent(parentObj.transform);
            newTransform.sizeDelta = new Vector2(2f, 20);
            newTransform.localScale = Vector3.one;
       
            var direction = new Vector3(0, 0, i * rotaionvalue);
            var rotation = Quaternion.Euler(direction);
            newTransform.rotation = rotation;

            newTransform.transform.position = newTransform.up * 5;
            newTransform.pivot = new Vector2(0.5f, 0);
            SpectrumBars.Add(NewImage);
            SpectrumBars[i].color = new Color(Random.value, Random.value, Random.value);

        }
    }
    void Update()
    {
        float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 0; i < SpectrumBarSize; i++)
        {
            SpectrumBars[i].rectTransform.sizeDelta = new Vector2(2, spectrum[i]*100+0);
        

            //Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            //Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }
    }
}
