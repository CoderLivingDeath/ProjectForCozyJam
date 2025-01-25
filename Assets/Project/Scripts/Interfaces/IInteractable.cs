using UnityEngine;

public interface IInteractable
{
    bool IsHifglithing { get; }

    void OnInteract(GameObject sender);
    void StartHigthLith(GameObject sender);
    void StopHigthLith(GameObject sende);
}