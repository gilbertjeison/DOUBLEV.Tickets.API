using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Utilities.CustomModels;
using Utilities.Enumerations;

namespace BusinessRules.Common
{
    public static class ExceptionProcessorHelper
    {
        public static void HandleException(Exception ex)
        {
            if (ex.GetType() == typeof(DbUpdateException))
            {
                if (ex.GetBaseException() is SqlException sqlEx)
                {
                    throw new CustomException(EnumerationException.TypeCustomException.Validation,
                                           GetCusEnumMessageBd(sqlEx), ex);
                }

                throw new CustomException(EnumerationException.TypeCustomException.Validation,
                    EnumerationException.Message.ErrorGeneral, ex);
            }
            if (ex.GetType() == typeof(DbUpdateConcurrencyException))
            {
                throw new CustomException(EnumerationException.TypeCustomException.Validation,
                    EnumerationException.Message.ErrorGeneral, ex);
            }

            throw new CustomException(EnumerationException.Message.ErrorGeneral,
                                      new[] { Convert.ToString(Guid.NewGuid(), CultureInfo.CurrentCulture) },
                                      ex);
        }

        public static EnumerationException.CategoryException CategoryException(Exception ex)
        {
            var enumCategory = EnumerationException.CategoryException.General;

            if (ex.GetType() == typeof(DbUpdateException))
            {
                enumCategory = EnumerationException.CategoryException.DataBase;
            }
            if (ex.GetType() == typeof(CustomException))
            {
                enumCategory = ((CustomException)ex).TypeException == EnumerationException.TypeCustomException.Validation ?
                                        EnumerationException.CategoryException.Validation :
                                        EnumerationException.CategoryException.BusinessException;
            }

            return enumCategory;
        }

        public static string ErrorMessage(Exception ex)
        {
            string message = string.Empty;
            if (ex != null)
            {
                const string defaultText = "Source: {0} - Message: {1}";
                message = string.Format(defaultText, ex?.Source, string.IsNullOrEmpty(ex?.InnerException?.ToString()) ?
                                                                    ex?.Message + ex?.ToString() :
                                                                    ex?.InnerException?.Message);
            }

            return message;
        }

        private static EnumerationException.Message GetCusEnumMessageBd(SqlException sqlEx)
        {
            if (sqlEx.Number == Constants.CodeSql.CodeUniquekey || sqlEx.Number == Constants.CodeSql.CodeDuplicateKeyRestriction)
            {
                return EnumerationException.Message.ErrUniqueKey;
            }

            if (sqlEx.Number == Constants.CodeSql.CodeInsertValuessNulll)
            {
                return EnumerationException.Message.ErrRequiredField;
            }
            if (sqlEx.Number == Constants.CodeSql.CodeConflictRestriccion)
            {
                return EnumerationException.Message.ErrForeingkey;
            }
            if (sqlEx.Number == Constants.CodeSql.CodeDataTruncated)
            {
                return EnumerationException.Message.ErrMaxLength;
            }

            if (sqlEx.Number == Constants.CodeSql.CodeViolationOfMaxValueConstraint)
            {
                return EnumerationException.Message.ErrMaxValue;
            }

            return EnumerationException.Message.ErrorGeneralDB;
        }
    }
}
