using Assets.Project.Scripts.Infrastructure.EventBus;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : IDisposable
{
    private InputSystem_Actions _actions;

    private IEventBus _eventBus;

    public InputService(InputSystem_Actions actions, IEventBus eventBus)
    {
        _actions = actions;
        _eventBus = eventBus;
        Enable();
    }

    public void Enable()
    {
        _actions.Enable();
        Subscribe();
    }

    public void Disable()
    {
        _actions.Disable();
        UnSubscribe();
    }

    #region Events

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        _eventBus.RaiseEvent<IPlayerMoveEventHandler>(h => h.Handle(value));
    }

    #endregion

    private void Subscribe()
    {
        _actions.Player.Move.performed += OnMove;
    }

    private void UnSubscribe()
    {
        _actions.Player.Move.performed -= OnMove;
    }

    public void Dispose()
    {
        Disable();
    }
}
