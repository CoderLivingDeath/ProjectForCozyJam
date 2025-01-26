using UnityEngine;
using Zenject;

public class NextLevelTeleport : MonoBehaviour
{
    public int SceneId;

    [Inject] private ISceneLoader _sceneLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent<PlayerBehaviour>(out _))
        {
            _sceneLoader.LoadScene(SceneId);
        }
    }
}
