using System.ComponentModel.DataAnnotations;

namespace WA.Pizza.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; init; }
    }
}
