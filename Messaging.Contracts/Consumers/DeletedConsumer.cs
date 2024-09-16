using InnoClinic.Shared.Domain.Abstractions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Abstraction;

public class DeletedConsumer<T, E>(ILogger logger, IRepository<E> repository, IUnitOfWork unitOfWork) : BaseConsumer<T, E>(logger)
where T : class, IEntity
where E : class, INamedEntity {
    public override async Task Consume(ConsumeContext<T> context) {
        await base.Consume(context);

        var entity = await repository.GetByIdAsync(context.Message.Id, context.CancellationToken);
        
        if (entity == null) {
            LogEntityNotFound(context.Message);
            return;
        }

        repository.Delete(entity);
        await unitOfWork.SaveChangesAsync();

        LogEntity(EntityOperation.Deleted, entity);
    }
}