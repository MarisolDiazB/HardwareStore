using System;
using System.Collections.Generic;

namespace HardwareStore.Core
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public List<string> Errors { get; set; } // Aquí inicializamos la lista de errores
        public string? Data { get; internal set; }

        public Response()
        {
            List<string> list = new List<string>();
            Errors = list;
        }
    }
}
