using Assets.Project.Scripts;
using UnityEngine;

public interface IInteractable : IHighlightable
{
    bool IsHifglithing { get; }

    void OnInteract(GameObject sender);
}