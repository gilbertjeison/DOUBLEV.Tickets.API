namespace Utilities.TextResponse
{
    public struct TextResponse : IEquatable<TextResponse>
    {
        public string Message { get; }

        public string NegativeMessage { get; }

        public string Title { get; }

        public TextResponse(string title, string message, string negativeMessage)
        {
            Message = message;
            NegativeMessage = negativeMessage;
            Title = title;
        }

        public TextResponse(string message, string negativeMessage) : this(null, message, negativeMessage) { }

        public TextResponse(string message) : this(null, message, null) { }

        public bool Equals(TextResponse other)
        {
            return this.Message == other.Message &&
                    this.NegativeMessage == other.NegativeMessage &&
                    this.Title == other.Title;
        }
    }
}
