using Utilities.Enumerations;

namespace Utilities.TextResponse
{
    public abstract class TextResponseMessage
    {
        private const string textProblem = "por favor corrija el problema y vuelva a intentarlo.";

        internal virtual TextResponse GetTextResponseMessage(EnumerationException.Message cusMessage)
        {
            return TextResponse.GetValueOrDefault(cusMessage);
        }

        protected static Dictionary<EnumerationException.Message, TextResponse> TextResponse { get; } =
            new Dictionary<EnumerationException.Message, TextResponse>
        {
            {
                EnumerationMessage.Message.MsjCreacion,
                new TextResponse("Creación de Registro",
                "El registro fue creado exitosamente.",
                "Ocurrio un error al intentar crear el registro.")
            },
            {
                EnumerationMessage.Message.MsjActualizacion,
                new TextResponse("Modificación de Registro",
                "El registro fue modificado exitosamente.",
                "Ocurrio un error al intentar modificar el registro.")
            },
            {
                EnumerationMessage.Message.MsjEliminacion,
                new TextResponse("Eliminacion de Registro",
                "El registro fue eliminado exitosamente.",
                "Ocurrio un error al intentar eliminar el registro.")
            },

            {
                EnumerationException.Message.ErrorGeneral,
                new TextResponse("Ha ocurrido un error general. ID: {0}")
            },
            {
                EnumerationException.Message.Unauthorized,
                new TextResponse("El usuario {0} no esta autorizado para ejecutar la acción solicitada.")
            }           
        };
    }
}
