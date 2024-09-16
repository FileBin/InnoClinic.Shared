using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.LayeredWebApp.ApplicationLayer.Models;
using InnoClinic.Shared.LayeredWebApp.DomainLayer;
using InnoClinic.Shared.Misc.Services;

namespace InnoClinic.Shared.LayeredWebApp.ApplicationLayer;

public class TestCrudService(IRepository<TestEntity> repository, IUnitOfWork unitOfWork) 
: CrudServiceBase<TestEntity, TestEntityResponse, CreateRequest, UpdateRequest>(repository, unitOfWork) {}