using Assets.Project.Scripts.Infrastructure.EventBus;
using UnityEngine;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<InputSystem_Actions>().AsSingle();
        Container.BindInterfacesAndSelfTo<EventBus>().AsSingle();
        Container.BindInterfacesAndSelfTo<SceneLoaderWithZenject>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
    }
}