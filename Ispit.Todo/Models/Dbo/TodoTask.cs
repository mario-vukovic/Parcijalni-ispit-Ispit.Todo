using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ispit.Todo.Models.Dbo.Base;

namespace Ispit.Todo.Models.Dbo
{
    public class TodoTask:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string TaskName { get; set; }
        public bool Status { get; set; }
        
        public ICollection<TodoList> TodoList { get; set; }
    }
}
