namespace Utilities.CustomModels
{
    public class ParameterBusinessRules : EventArgs
    {
        public int CompanyId { get; }

        public ParameterBusinessRules(int companyId)
        {
            this.CompanyId = companyId;
        }
    }
}
