using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Food : MonoBehaviour
{
    public event Action OnEaten;

    private void OnDestroy()
    {
        OnEaten?.Invoke();
    }
}
