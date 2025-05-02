using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Application.Helpers
{
    public class OperationResult<T> //generell klass för att skicka tillbaka resultat
    {
        public bool Success { get; set; } //true=lyckat
        public string? ErrorMessage { get; set; } //felmeddelande om det misslyckas
        public T? Data { get; set; } //Det data som returneras

        public static OperationResult<T> Ok(T data) //används när allt gick bra
        {
            return new OperationResult<T> { Success = true, Data = data };
        }

        public static OperationResult<T> Fail(string error) //används när något blev fel 
        {
            return new OperationResult<T> { Success = false, ErrorMessage = error };
        }
    }
}
