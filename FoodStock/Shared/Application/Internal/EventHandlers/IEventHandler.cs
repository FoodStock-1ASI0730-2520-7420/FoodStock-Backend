using Cortex.Mediator.Notifications;
using FoodStock.Shared.Domain.Model.Events;

namespace FoodStock.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
    
}