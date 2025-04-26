using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreator : MonoBehaviour
{
    [SerializeField] Renderer m_Renderer;
    CardObject cardObject;
    private void Start()
    {
        if(GetComponentInParent<CardController>())
        {
           cardObject = GetComponentInParent<CardController>().card;

            m_Renderer.material = cardObject.material;
        }
    }
}
