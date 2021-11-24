using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UIManager : Singleton<UIManager>
{
    [HideInInspector] public Canvas mainCanvas;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < transform.childCount; i++)
        {
            UIElement uiElement = transform.GetChild(i).GetComponent<UIElement>();
            if (uiElement) uiElement.Initialize();
        }


        mainCanvas = GetComponent<Canvas>();
    }


}
