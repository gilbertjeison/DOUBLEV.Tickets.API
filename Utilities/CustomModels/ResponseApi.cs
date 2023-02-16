using System.Globalization;
using Utilities.Enumerations;
using Utilities.ExtensionMethods;
using Utilities.TextResponse;

namespace Utilities.CustomModels
{
    public class ResponseApi
    {
        public EnumerationException.Message ErrorCode { get; }

        public string Title { get; }

        public string TitleTag { get; }

        public string Message { get; }

        public string Detail { get; }

        public string MessageTag { get; }

        public string[] Tags { get; }

        public EnumerationApplication.CategoryMessage CategoryMessage { get; }

        public string CategoryMessageTag { get; }

        public ResponseApi(EnumerationException.Message message,
                              string[] tags,
                              EnumerationApplication.CategoryMessage categoryMessage)
        {
            ErrorCode = message;
            MessageTag = message.ToString();

            CategoryMessage = categoryMessage;
            CategoryMessageTag = categoryMessage.ToString();

            var approvedMessage = new TextResponseApi().GetText(message);
            Message = approvedMessage.Message;
            if (categoryMessage != EnumerationApplication.CategoryMessage.Success && approvedMessage.NegativeMessage.IsValid())
            {
                Message = approvedMessage.NegativeMessage;
            }

            Title = approvedMessage.Title;
            if (Title.IsValid())
            {
                TitleTag = $"Title{message}";
            }

            if (tags.IsNotNull())
            {
                Tags = tags;
            }

            if (Message.IsValid() && Tags.IsNotNull())
            {
                Message = string.Format(CultureInfo.CurrentCulture, Message, Tags);
            }
        }

        public ResponseApi(EnumerationException.Message message,
                              string[] tags,
                              string detail,
                              EnumerationApplication.CategoryMessage categoryMessage) :
            this(message, tags, categoryMessage)
        {
            this.Detail = detail;
        }
    }
}
