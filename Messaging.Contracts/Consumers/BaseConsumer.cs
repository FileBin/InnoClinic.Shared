using InnoClinic.Shared.Domain.Abstractions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Abstraction;

public class BaseConsumer<T, E>(ILogger logger) : IConsumer<T> 
where T : class, IEntity
where E : class, INamedEntity {
    public virtual Task Consume(ConsumeContext<T> context) {
        logger.LogInformation("{MessageType} message received with Id={MessageId}", nameof(T), context.MessageId);
        return Task.CompletedTask;
    }

    public void LogEntity(EntityOperation operation, E entity) {
        logger.LogInformation("{EntityType} was {Operation} with Id={EntityId} Name={EntityName}", nameof(T), operation.ToString().ToLowerInvariant(), entity.Id, entity.Name);
    }

    public void LogEntityNotFound(T messageBody) {
        logger.LogWarning("{EntityType} with Id={EntityId} was not found", nameof(T), messageBody.Id);
    }

    public enum EntityOperation {
        Created,
        Deleted,
        Updated,
    }
}