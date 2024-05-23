// Este archivo define una clase ResponseHelper<T> en el espacio de nombres HardwareStore.Helpers.
// La clase proporciona métodos estáticos para generar respuestas de éxito o error encapsuladas en objetos de tipo Response<T>.

using HardwareStore.Core; // Importa el espacio de nombres que contiene la clase Response<T>.
using System; // Importa el espacio de nombres que contiene la clase Exception.
using System.Collections.Generic; // Importa el espacio de nombres para List<T>.

namespace HardwareStore.Helpers
{
    // Define una clase ResponseHelper<T> con métodos estáticos para generar respuestas de éxito o error.
    public static class ResponseHelper<T>
    {
        // Método estático para generar una respuesta de éxito con un modelo especificado.
        public static Response<T> MakeResponseSuccess( T model)
        {
            return new Response<T>
            {
                IsSuccess = true,
                Message = "Tarea realizada con éxito",
                Result = model
            };
        }

        // Método estático para generar una respuesta de éxito con un modelo y un mensaje personalizado.
        public static Response<T> MakeResponseSuccess(T model, string message)
        {
            return new Response<T>
            {
                IsSuccess = true,
                Message = message,
                Result = model
            };
        }

        // Método estático para generar una respuesta de éxito con un mensaje personalizado.
        public static Response<T> MakeResponseSuccess(string message)
        {
            return new Response<T>
            {
                IsSuccess = true,
                Message = message
            };
        }

        // Método estático para generar una respuesta de error con una lista de errores.
        public static Response<object> MakeResponseFail(List<string> errors)
        {
            return new Response<object>
            {
                IsSuccess = false,
                Message = "Error al generar la solicitud",
                Errors = errors
            };
        }

        // Método estático para generar una respuesta de error con una lista de errores y un mensaje personalizado.
        public static Response<T> MakeResponseFail(List<string> errors, string message)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }

        // Método estático para generar una respuesta de error a partir de una excepción.
        public static Response<T> MakeResponseFail(Exception ex)
        {
            List<string> errors = new List<string>
            {
                ex.Message
            };

            return new Response<T>
            {
                IsSuccess = false,
                Message = "Error al generar la solicitud",
                Errors = errors
            };
        }

        // Método estático para generar una respuesta de error con un único mensaje de error.
        public static Response<T> MakeResponseFail(string error)
        {
            List<string> errors = new List<string>
            {
                error
            };

            return new Response<T>
            {
                IsSuccess = false,
                Message = error,
                Errors = errors
            };
        }

        // Nueva sobrecarga para manejar la respuesta de éxito con paginación.
        public static Response<List<T>> MakeResponseSuccess(List<T> model, string message, int totalCount)
        {
            return new Response<List<T>>
            {
                IsSuccess = true,
                Message = message,
                Result = model,
                TotalCount = totalCount
            };
        }
    }
}
