using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardCreator : MonoBehaviour
{
    [SerializeField] TextMeshPro tmp;
    [SerializeField] Renderer m_Renderer;
    CardObject cardObject;
    private void Start()
    {
        if(GetComponentInParent<CardController>())
        {
           cardObject = GetComponentInParent<CardController>().card;

            m_Renderer.material = cardObject.material;

            if (cardObject.value <= 0 || cardObject.visibleValue == false)
            {
                tmp.text = "";
            }
            else
            {
                tmp.text = cardObject.value.ToString();
            }
        }
    }
}
