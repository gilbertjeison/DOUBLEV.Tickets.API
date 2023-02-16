using System.ComponentModel;

namespace Utilities.Enumerations
{
    public abstract class EnumerationMessage
    {
        public enum Message
        {
            //Transversales 1 - 10X
            [Description("Error General Aplicación")]
            ErrorGeneral = 0,

            MsjCreacion = 1,
            MsjActualizacion = 2,
            MsjEliminacion = 3,
            MsjConfirmAutonomy = 4,

            ErrNoEncontrado = 10,

            Unauthorized = 99,

            //Comunes BD 10X - 100X
            ErrorGeneralDB = 100,

            ErrUniqueKey = 101,
            ErrRequiredField = 102,
            ErrForeingkey = 103,
            ErrMaxLength = 104,
            ErrMaxValue = 105,
            ExistField = 106,
            ObjectEmpty = 107,

            //Validaciones Negocio 100X - 10000X

            //Error de Negocio 10000X:
        }
    }
}
