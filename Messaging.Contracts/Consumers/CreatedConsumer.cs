using InnoClinic.Shared.Domain.Abstractions;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Abstraction;

public class CreatedConsumer<T, E>(ILogger logger, IRepository<E> repository, IUnitOfWork unitOfWork) : BaseConsumer<T, E>(logger)
where T : class, IEntity
where E : class, INamedEntity {
    public override async Task Consume(ConsumeContext<T> context) {
        await base.Consume(context);
        var entity = context.Message.Adapt<E>();
        repository.Create(entity);
        await unitOfWork.SaveChangesAsync(context.CancellationToken);

        LogEntity(EntityOperation.Created, entity);
    }
}
