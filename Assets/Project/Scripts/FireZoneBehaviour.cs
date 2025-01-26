using System.Collections;
using UnityEngine;

public class FireZoneBehaviour : MonoBehaviour
{
    public float damageInterval = 1f; // Интервал между нанесением урона

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.transform.parent.gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour player))
        {
            StartCoroutine(nameof(DealDamagePeriodically), player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.transform.parent.gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour player))
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator DealDamagePeriodically(PlayerBehaviour player)
    {
        while (true)
        {
            yield return new WaitForSeconds(damageInterval);
            TryDealDamageToPlayer(player);
        }
    }

    private void TryDealDamageToPlayer(PlayerBehaviour player)
    {
        if (player.PlayerModelType == PlayerModelType.Large || player.PlayerModelType == PlayerModelType.Normal)
        {
            player.DowngradePlayerModelState();
        }
    }
}
