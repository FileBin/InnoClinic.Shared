using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Application.Messaging.Abstraction;

public class UpdatedConsumer<T, E>(ILogger logger, IRepository<E> repository, IUnitOfWork unitOfWork) : BaseConsumer<T, E>(logger)
where T : class, IEntity
where E : class, INamedEntity {
    public override async Task Consume(ConsumeContext<T> context) {
        await base.Consume(context);

        var entity = await repository.GetByIdOrThrow(context.Message.Id, context.CancellationToken);
        context.Message.Adapt(entity);
        repository.Update(entity);
        await unitOfWork.SaveChangesAsync();

        LogEntity(EntityOperation.Updated, entity);
    }
}
