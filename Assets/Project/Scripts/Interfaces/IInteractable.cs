using Assets.Project.Scripts;
using UnityEngine;

public interface IInteractable : IHighlightable
{
    bool IsHighlithing { get; }

    void OnInteract(GameObject sender);
}