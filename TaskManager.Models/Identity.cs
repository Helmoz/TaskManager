using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public abstract class Identity
    {
        [Key]
        public int Id { get; set; }
    }
}
