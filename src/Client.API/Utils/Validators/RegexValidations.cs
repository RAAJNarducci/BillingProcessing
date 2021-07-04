namespace Client.API.Utils.Validators
{
    public static class RegexValidations
    {
        public const string REGEX_CPF = @"([A-Z])\w+([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})";
    }
}
