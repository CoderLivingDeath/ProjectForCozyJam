using Assets.Project.Scripts.Infrastructure.EventBus;
using Assets.Project.Scripts.Infrastructure.EventBus.EventHandlers;
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
        _eventBus.RaiseEvent<IPlayerMoveEventHandler>(h => h.MoveHandle(value));
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        _eventBus.RaiseEvent<IPlayerInteractEventHanlder>(h => h.InteractHandle());
    }

    private void OnDropMass(InputAction.CallbackContext context)
    {
        _eventBus.RaiseEvent<IPlayerDropMassEventHandler>(h => h.DropMassHandle());
    }

    #endregion

    private void Subscribe()
    {
        _actions.Player.Move.performed += OnMove;
        _actions.Player.Move.canceled += OnMove;

        _actions.Player.Interact.performed += OnInteract;

        _actions.Player.DropMass.performed += OnDropMass;
    }

    private void UnSubscribe()
    {
        _actions.Player.Move.performed -= OnMove;
        _actions.Player.Move.canceled -= OnMove;

        _actions.Player.Interact.performed -= OnInteract;

        _actions.Player.DropMass.performed -= OnDropMass;
    }

    public void Dispose()
    {
        Disable();
    }
}
