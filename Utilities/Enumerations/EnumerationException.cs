using System.ComponentModel;

namespace Utilities.Enumerations
{
    public class EnumerationException : EnumerationMessage
    {
        public enum TypeCustomException { Undefined, Validation, BusinessException, NoContent, Unauthorized }

        public enum CategoryException
        {
            [Description("Generales")]
            General,
            [Description("Base de Datos")]
            DataBase,
            [Description("Negocio")]
            BusinessException,
            [Description("Validaciones")]
            Validation
        }
    }
}
