using System;
using UnityEngine;

public class Detector : MonoBehaviour
{
    //public delegate void DetectedHandler();
    public event Action OnEntered;
    public event Action OnExited;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Mover Crook))
            OnEntered?.Invoke();
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Mover Crook))
            OnExited?.Invoke();
    }
}