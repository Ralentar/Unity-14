using UnityEngine;

public class Detector : MonoBehaviour
{
    public delegate void DetectedHandler();
    public event DetectedHandler OnEntered;
    public event DetectedHandler OnExited;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Mover crook))
            OnEntered?.Invoke();
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Mover crook))
            OnExited?.Invoke();
    }
}