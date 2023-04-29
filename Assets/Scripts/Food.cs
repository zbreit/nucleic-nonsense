using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Food : MonoBehaviour
{
    public event Action OnEaten;

    [SerializeField]
    private float resource = 50.0f;

    void FixedUpdate() {
        if (resource <= 0.0f) { Destroy(this.gameObject); }
    }

    public float getResourceAmount() { return resource; }

    public float Consume(float amount) {
        if (amount >= resource) {
            float retVal = resource;
            resource = 0f;
            return retVal;
        } else {
            resource -= amount;
            return amount;
        }
    }

    private void OnDestroy()
    {
        OnEaten?.Invoke();
    }
}
