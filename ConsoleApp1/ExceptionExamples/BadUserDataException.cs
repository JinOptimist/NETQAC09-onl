namespace ExceptionExamples;

internal class BadUserDataException : Exception
{
    private string userName;

    public BadUserDataException(string userName, string errorMessage)
        : base(errorMessage)
    {
        this.userName = userName;
    }
}
