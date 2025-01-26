using UnityEngine;

public class EndLevelBehaviour : MonoBehaviour
{
    public GameObject EndView;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent<PlayerBehaviour>(out var player))
        {
            if(player.GiftSpizhen) EndView.SetActive(true);
        }
    }
}
