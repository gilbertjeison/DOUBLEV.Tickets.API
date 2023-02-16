using static Utilities.Enumerations.EnumerationApplication;

namespace Utilities.CustomModels
{
    public class ItemsFilter
    {
        public string Name { set; get; }

        public object[] Values { set; get; }

        public OperationExpression Operator { set; get; }
    }    
}
