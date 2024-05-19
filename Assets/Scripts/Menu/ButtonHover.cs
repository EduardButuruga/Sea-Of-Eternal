using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    public Text buttonText;
    public float hoverScale = 1.2f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = buttonText.transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.transform.localScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.transform.localScale = originalScale;
    }
}
