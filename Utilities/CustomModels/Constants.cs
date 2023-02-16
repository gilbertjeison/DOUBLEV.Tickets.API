namespace Utilities.CustomModels
{
    public static class Constants
    {
        public const string DefaultUriWebApi = "api/v1/";
        public static readonly int DefaultNumeroDeRegistros = 10;

        #region Codigo Errores SQL

        public struct CodeSql
        {
            /// <summary>
            /// Código de Error SQL InsertarValoresNulos
            /// No se puede insertar el valor NULL en la columna '%1!', tabla '%2!'. La columna no admite valores NULL. Error de %3!.
            /// </summary>
            public static readonly int CodeInsertValuessNulll = 515;

            /// <summary>
            /// Código de Error SQL IdentityInsert
            /// No se puede insertar un valor explícito en la columna de identidad de la tabla '%1!' cuando IDENTITY_INSERT es OFF.
            /// </summary>
            public static readonly int CodeIdentityInsert = 544;

            /// <summary>
            /// Código de Error SQL ConflictoRestriccion
            /// Instrucción %1! en conflicto con la restricción %2! "%3!". El conflicto ha aparecido en la base de datos "%4!", tabla "%5!"%6!%7!%8!.
            /// </summary>
            public static readonly int CodeConflictRestriccion = 547;
            /// <summary>
            /// Código de Error SQL ClaveDuplicada
            /// No se puede insertar una fila de clave duplicada en el objeto '%1!' con índice único '%2!'. El valor de la clave duplicada es %3!.
            /// </summary>
            public static readonly int CodeUniquekey = 2601;
            /// <summary>
            /// Código de Error SQL RestriccionClaveDuplicada
            /// Infracción de la restricción %1! '%2!'. No se puede insertar una clave duplicada en el objeto '%3!'. El valor de la clave duplicada es %4!.
            /// </summary>
            public static readonly int CodeDuplicateKeyRestriction = 2627;
            /// <summary>
            /// Código de Error SQL ViolationOfMaxLengthstaticraint
            /// Los datos de cadena o binarios se truncarían.
            /// </summary>
            public static readonly int CodeDataTruncated = 8152;
            /// <summary>
            /// Código de Error SQL ViolationOfMaxValuestaticraint
            /// </summary>
            public static readonly int CodeViolationOfMaxValueConstraint = 8115;
        }

        #endregion
    }
}
