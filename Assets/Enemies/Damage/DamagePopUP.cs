using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUP : MonoBehaviour
{
    public static DamagePopUP Create(Vector3 position, int damageAmount, bool isCriticalHit, Vector3 offset)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position + offset, Quaternion.identity);
        DamagePopUP damagePopup = damagePopupTransform.GetComponent<DamagePopUP>();
        damagePopup.Setup(damageAmount, isCriticalHit);
        return damagePopup;
    }

    private static int sortingOrder;
    private const float DISSAPEAR_TIMER_MAX = 0.3f;
    private TextMeshPro textMesh;
    private float dissapearTimer;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            //Normal hit
            textMesh.fontSize = 8.5f;
            textColor = Color.yellow;
        }
        else
        {
            //Critical hit
            textMesh.fontSize = 11f;
            ColorUtility.TryParseHtmlString("#b20000", out textColor);
        }
        textMesh.color = textColor;
        dissapearTimer = DISSAPEAR_TIMER_MAX;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        moveVector = new Vector3(0.7f, 1) * 10f;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;
        if (dissapearTimer > DISSAPEAR_TIMER_MAX * .5f)
        {
            //First half of the popup
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            //Second half of the popup
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        dissapearTimer -= Time.deltaTime;
        if (dissapearTimer < 0)
        {
            //Start dissapearing
            float dissapearSpeed = 10f;
            textColor.a -= dissapearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}