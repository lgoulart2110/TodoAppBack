using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoApp.Application._Common.Interfaces;
using TodoApp.Application.Models;
using TodoApp.Application.Models.Abstract;
using TodoApp.Domain._Common.Params;
using TodoApp.Domain.Entities.Abstract;

namespace TodoApp.Api.Controllers.Abstract
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TEntity, TModel, TParams> : ControllerBase
        where TEntity : EntityBase
        where TModel : ModelBase
        where TParams : IParams
    {
        protected readonly INotificationService _notificationService;
        protected readonly IServiceBase<TEntity, TModel, TParams> _service;

        public BaseController(
            INotificationService notificationService,
            IServiceBase<TEntity, TModel, TParams> service
        )
        {
            _notificationService = notificationService;
            _service = service;
        }

        [HttpPut("{id}")]
        public virtual async Task<Result<TModel>> PutAsync([FromRoute] int id, [FromBody] TModel model)
        {
            model.Id = id;
            return GetResult(await _service.UpdateAsync(model));
        }

        [HttpPost]
        public virtual async Task<Result<TModel>> PostAsync([FromBody] TModel model)
            => GetResult(await _service.CreateAsync(model));

        [HttpDelete("{id}")]
        public virtual async Task<Result<int>> DeleteAsync([FromRoute] int id)
            => GetResult(await _service.DeleteAsync(id));


        [HttpGet]
        public virtual async Task<Result<IEnumerable<TModel>>> GetAllAsync([FromQuery] TParams @params)
        {
            return GetResult(
                await _service.SearchAsync(@params),
                await _service.CountAsync(default)
            );
        }

        [HttpGet("{id}")]
        public virtual async Task<Result<TModel>> GetByIdAsync([FromRoute] int id)
            => GetResult(await _service.GetByIdAsync(id));

        protected Result<T> GetResult<T>(T data, int? total = null) => GetResult(new Result<T>(data, total));

        protected Result<T> GetResult<T>(Result<T> result)
        {
            ConfigureResult(result);
            return result;
        }

        protected Result GetResult(Result result)
        {
            ConfigureResult(result);
            return result;
        }

        private void ConfigureResult<T>(Result<T> result)
        {
            var notificationStatus = _notificationService.Notifications?.LastOrDefault()?.StatusCode ?? result.StatusCode;
            Response.StatusCode = (int)(Enum.IsDefined(notificationStatus) ? notificationStatus : HttpStatusCode.OK);
            result.AddErrors(_notificationService.Notifications);
        }
    }
}
