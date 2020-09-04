using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHeathBar : MonoBehaviour
{
    public static UiHeathBar intance { get; private set; }
    public Image mask;
    float originalsize;
    private void Awake()
    {
        intance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        originalsize = mask.rectTransform.rect.width;
    }

    public void SetValues(float values)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalsize * values);
    }
}
