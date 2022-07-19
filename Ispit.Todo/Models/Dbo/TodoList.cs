using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ispit.Todo.Models.Dbo.Base;

namespace Ispit.Todo.Models.Dbo
{
    public class TodoList:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string ListName { get; set; }
        public ICollection<TodoTask> Tasks { get; set; }
        public AspNetUser AspNetUser { get; set; }
    }
}
