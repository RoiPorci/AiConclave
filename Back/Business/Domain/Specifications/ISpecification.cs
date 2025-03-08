using System.Threading.Tasks;

namespace Business.Domain.Specifications;

public interface ISpecification<in T>
{
    Task<ValidationResult> ValidateAsync(T entity);
}
