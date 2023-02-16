using Utilities.Enumerations;

namespace Utilities.TextResponse
{
    public class TextResponseApi : TextResponseMessage
    {
        internal TextResponse GetText(EnumerationException.Message cusMessage)
        {
            return GetTextResponseMessage(cusMessage);
        }
    }
}
