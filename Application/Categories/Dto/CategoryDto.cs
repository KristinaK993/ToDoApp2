using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Application.Categories.Dto
{
    public class CategoryDto //Dto klass som används för att skicka info till/från kunden
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
