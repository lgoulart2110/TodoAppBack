using FluentValidation;
using MapsterMapper;
using System.Net;
using TodoApp.Application._Common.Interfaces;
using TodoApp.Application.Models.Abstract;
using TodoApp.Domain._Common.Adapters;
using TodoApp.Domain._Common.Params;
using TodoApp.Domain.Entities.Abstract;

namespace TodoApp.Application._Common.Services
{
    public class ServiceBase<TEntity, TModel, TParams> : NotificationBase, IServiceBase<TEntity, TModel, TParams>
        where TEntity : EntityBase
        where TModel : ModelBase
        where TParams : IParams
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IValidator<TModel> _validator;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly string _entityName;

        public ServiceBase(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IValidator<TModel> validator,
            IRepository<TEntity> repository,
            INotificationService notificationService
        ) : base(notificationService)
        {
            _mapper = mapper;
            _validator = validator;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _entityName = typeof(TEntity).Name;
        }


        public virtual async Task<int> CountAsync(TParams @params)
        {
            return await _repository.CountAsync(@params as IFiltrable<TEntity>);
        }

        public virtual async Task<TModel> CreateAsync(TModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                NotifyValidationResult(validationResult);
                return default;
            }

            var entity = _mapper.Map<TEntity>(model);
            entity = await _repository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<int> DeleteAsync(int id)
        {
            var existEntity = await _repository.ExistAsync(id);
            if (!existEntity)
            {
                Notify(HttpStatusCode.NotFound, nameof(id), $"{_entityName} is not found.");
                return default;
            }

            await _repository.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return id;
        }

        public virtual async Task<TModel> GetByIdAsync(int id)
        {
            var existEntity = await _repository.ExistAsync(id);
            if (!existEntity)
            {
                Notify(HttpStatusCode.NotFound, nameof(id), $"{_entityName} is not found.");
                return default;
            }

            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<IEnumerable<TModel>> SearchAsync(TParams @params)
        {
            var entities = await _repository.SearchAsync(@params);
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }

        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                NotifyValidationResult(validationResult);
                return default;
            }

            var existEntity = await _repository.ExistAsync(model.Id);
            if (!existEntity)
            {
                Notify(HttpStatusCode.NotFound, nameof(model.Id), $"{_entityName} is not found.");
                return default;
            }

            var entity = await _repository.GetByIdAsync(model.Id);
            _mapper.Map(model, entity);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TModel>(entity);
        }
    }
}
