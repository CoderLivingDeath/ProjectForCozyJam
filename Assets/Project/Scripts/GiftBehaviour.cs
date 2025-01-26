using UnityEngine;

public class GiftBehaviour : MonoBehaviour, IInteractable
{
    public bool IsHighlithing => false;

    public void OnInteract(GameObject sender)
    {
        sender.GetComponent<PlayerBehaviour>().GiftSpizhen = true;
        Destroy(this.gameObject);
    }

    public void StartHighlight(GameObject sender)
    {
    }

    public void StopHighLight(GameObject sender)
    {
    }
}
