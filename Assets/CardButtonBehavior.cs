using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardButtonBehavior : MonoBehaviour
{
    public RectTransform rectTransform;
    public Vector2 originalSize;
    public Vector2 enlargedSize;
    private static CardButtonBehavior lastSelectedCard;
    private bool isHovering = false;
    private Vector3 originalPosition;

    void Start()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
        originalSize = rectTransform.sizeDelta;
        originalPosition = rectTransform.localPosition;
    }

    public void OnCardClick()
    {
        if (lastSelectedCard != null && lastSelectedCard != this)
        {
            lastSelectedCard.rectTransform.sizeDelta = lastSelectedCard.originalSize;
        }

        if (lastSelectedCard == this)
        {
            rectTransform.sizeDelta = originalSize;
            lastSelectedCard = null;
        }
        else
        {
            rectTransform.sizeDelta = enlargedSize;
            lastSelectedCard = this;
        }
    }

    public void OnMouseEnter()
    {
        isHovering = true;
        StartCoroutine(Shake());
    }

    public void OnMouseExit()
    {
        isHovering = false;
        rectTransform.localPosition = originalPosition;
    }

    private IEnumerator Shake()
    {
        while (isHovering)
        {
            Vector3 randomPosition = originalPosition + (Vector3)Random.insideUnitCircle * 5f;
            rectTransform.localPosition = randomPosition;
            yield return new WaitForSeconds(0.1f);
        }
        rectTransform.localPosition = originalPosition;
    }
}
