using Assets.Project.Scripts.Infrastructure.EventBus.EventHandlers;
using UnityEngine;

internal interface IPlayerMoveEventHandler : IGlobalSubscriber
{
    public void MoveHandle(Vector2 direction);
}