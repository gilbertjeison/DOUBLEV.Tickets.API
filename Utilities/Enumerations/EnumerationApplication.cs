namespace Utilities.Enumerations
{
    public class EnumerationApplication
    {
        public enum Orden
        {
            Asc,
            Desc
        }

        public enum OperationExpression
        {
            Equals,
            NotEquals,
            Minor,
            MinorEquals,
            Mayor,
            MayorEquals,
            Like,
            NotLike,
            StartsWith,
            NotStartsWith,
            EndsWith,
            NotEndsWith,
            Contains,
            Any
        }

        public enum CategoryMessage { Success = 200, Error = 500, Warning = 400, Alert = 100 }

        public enum Validations
        {
            Entity = 1,
            Button = 2
        }
    }
}
