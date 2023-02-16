using System.Runtime.Serialization;
using Utilities.Enumerations;
using Utilities.TextResponse;
using static Utilities.Enumerations.EnumerationException;

namespace Utilities.CustomModels
{
    public class CustomException : Exception
    {
        public string Username { get; private set; }

        public string ResourceName { get; private set; }

        public IList<string> ValidationErrors { get; private set; }

        public string ErrorMessageThrowBusinessValidation => new TextResponseApi().GetTextResponseMessage(this.ErrorBusiness).Message;

        public TypeCustomException TypeException { private set; get; }

        private EnumerationException.Message _errorBusiness = EnumerationMessage.Message.ErrorGeneral;
        public EnumerationException.Message ErrorBusiness { private set { _errorBusiness = value; } get { return _errorBusiness; } }

        public string[] TagTextBusiness { private set; get; }

        private void DefaultValues()
        {
            this.ErrorBusiness = EnumerationException.Message.ErrorGeneral;
            this.TypeException = TypeCustomException.Undefined;
            this.TagTextBusiness = null;
            this.ValidationErrors = null;
            this.Username = string.Empty;
            this.ResourceName = string.Empty;
        }

        public CustomException() { this.DefaultValues(); }

        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.DefaultValues();
            this.Username = info.GetString("Username");
            this.ResourceName = info.GetString("ResourceName");
            this.ValidationErrors = (IList<string>)info.GetValue("ValidationErrors", typeof(IList<string>));
        }

        public CustomException(string message) : base(message) { this.DefaultValues(); }

        public CustomException(string message, string resourceName, IList<string> validationErrors) : base(message)
        {
            this.DefaultValues();
            this.ResourceName = resourceName;
            this.ValidationErrors = validationErrors;
        }

        public CustomException(string message, string username, string resourceName, IList<string> validationErrors) : base(message)
        {
            this.DefaultValues();
            this.Username = username;
            this.ResourceName = resourceName;
            this.ValidationErrors = validationErrors;
        }

        public CustomException(string message, Exception innerException) : base(message, innerException) { this.DefaultValues(); }

        public CustomException(Exception ex) : base(ex?.Message, ex)
        {
            this.ErrorBusiness = EnumerationException.Message.ErrorGeneral;
            this.TypeException = TypeCustomException.Undefined;
            this.TagTextBusiness = null;
        }

        public CustomException(TypeCustomException typeException, EnumerationException.Message errorBusiness) : this()
        {
            this.ErrorBusiness = errorBusiness;
            this.TypeException = typeException;
        }

        public CustomException(TypeCustomException typeException, EnumerationException.Message errorBusiness, Exception ex) : this(ex)
        {
            this.ErrorBusiness = errorBusiness;
            this.TypeException = typeException;
        }

        public CustomException(TypeCustomException typeException, EnumerationException.Message errorBusiness, string[] tagTextBusiness) : this(typeException, errorBusiness)
        {
            this.TagTextBusiness = tagTextBusiness;
        }

        public CustomException(TypeCustomException typeException, EnumerationException.Message errorBusiness, string[] tagTextBusiness, Exception ex) : this(typeException, errorBusiness, ex)
        {
            this.TagTextBusiness = tagTextBusiness;
        }

        public CustomException(EnumerationException.Message errorBusiness) : this()
        {
            this.ErrorBusiness = errorBusiness;
        }

        public CustomException(EnumerationException.Message errorBusiness, Exception ex) : this(ex)
        {
            this.ErrorBusiness = errorBusiness;
        }

        public CustomException(EnumerationException.Message errorBusiness, string[] tagTextBusiness) : this(errorBusiness)
        {
            this.TagTextBusiness = tagTextBusiness;
        }

        public CustomException(EnumerationException.Message errorBusiness, string[] tagTextBusiness, Exception ex) : this(errorBusiness, ex)
        {
            this.TagTextBusiness = tagTextBusiness;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("Username", this.Username);
            info.AddValue("ResourceName", this.ResourceName);
            info.AddValue("ValidationErrors", this.ValidationErrors, typeof(IList<string>));

            base.GetObjectData(info, context);
        }
    }
}
