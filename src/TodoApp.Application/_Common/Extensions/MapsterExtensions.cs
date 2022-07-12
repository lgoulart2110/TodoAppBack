using Mapster;
using MapsterMapper;
using System.Linq.Expressions;
using System.Reflection;
using TodoApp.Application.Models.Abstract;
using TodoApp.Domain.Entities.Abstract;

namespace TodoApp.Application._Common.Extensions
{
    public static class MapsterExtensions
    {
        public static IMapper Mapster;

        public static TypeAdapterSetter<TSource, TDestination> TrackList<
            TSource,
            TDestination,
            TDestinationMember,
            TSourceMember
        >
        (
            this TypeAdapterSetter<TSource, TDestination> mapster,
            Expression<Func<TDestination, IEnumerable<TDestinationMember>>> entitiesMember,
            Expression<Func<TSource, IEnumerable<TSourceMember>>> modelsMember
        )
            where TDestinationMember : EntityBase
            where TSourceMember : ModelBase
        {
            return mapster.Ignore(_ => entitiesMember)
                .AfterMapping((source, destination) =>
                {
                    var models = modelsMember.Compile().Invoke(source);
                    var entities = entitiesMember.Compile().Invoke(destination);
                    var entitiesProperty = GetPropertyInfo(entitiesMember);

                    if (models is null || entities is null)
                    {
                        entities = (models ?? Array.Empty<TSourceMember>()).Adapt<IEnumerable<TDestinationMember>>();
                        entitiesProperty.SetValue(destination, entities, null);
                        return;
                    }

                    var entitiesResult = entities.ToList();
                    entitiesResult.RemoveAll(e => !models.Any(m => m.Id == e.Id));
                    entitiesResult.ForEach(entity =>
                    {
                        var model = models.Single(m => m.Id == entity.Id);
                        Mapster.Map(model, entity);
                    });

                    var entitiesAppend = models
                        .Where(m => !entities.Any(e => e.Id == m.Id))
                        .Select(m => m.Adapt<TDestinationMember>());
                    entitiesResult.AddRange(entitiesAppend);

                    entitiesProperty.SetValue(destination, entitiesResult.ToArray(), null);
                });
        }

        private static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            var type = typeof(TSource);
            var member = expression.Body as MemberExpression;
            if (member is null)
            {
                var message = string.Format("Expression '{0}' refers to a method, not a property.", expression.ToString());
                throw new ArgumentException(message);
            }

            var propertyInfo = member.Member as PropertyInfo;
            if (propertyInfo is null)
            {
                var message = string.Format("Expression '{0}' refers to a field, not a property.", expression.ToString());
                throw new ArgumentException(message);
            }

            if (type != propertyInfo.ReflectedType && !type.IsSubclassOf(propertyInfo.ReflectedType))
            {
                var message = string.Format("Expression '{0}' refers to a property that is not from type {1}.", expression.ToString(), type);
                throw new ArgumentException(message);
            }

            return propertyInfo;
        }
    }
}
