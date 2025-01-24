using UnityEngine;
using Zenject;

public class SceneLoaderWithZenject : ISceneLoader
{
    private Zenject.ZenjectSceneLoader _loader;

    public SceneLoaderWithZenject(ZenjectSceneLoader loader)
    {
        _loader = loader;
    }

    public void LoadScene(int id)
    {
        _loader.LoadScene(id);
    }
}
