﻿using UnityEngine;

public class LoadingCircleProgress : MonoBehaviour
{
    private RectTransform rectComponent;
    private float rotateSpeed = -900f;

    private void Start()
    {
        rectComponent = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}