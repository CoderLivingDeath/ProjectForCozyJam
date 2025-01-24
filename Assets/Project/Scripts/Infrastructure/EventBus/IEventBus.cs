using Assets.Project.Scripts.Infrastructure.EventBus.EventHandlers;
using System;

namespace Assets.Project.Scripts.Infrastructure.EventBus
{
    public interface IEventBus
    {
        void RaiseEvent<TSubscriber>(Action<TSubscriber> action) where TSubscriber : class, IGlobalSubscriber;
        void Subscribe(IGlobalSubscriber subscriber);
        void Unsubscribe(IGlobalSubscriber subscriber);
    }
}