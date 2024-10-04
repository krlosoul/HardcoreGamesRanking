namespace HardcoreGamesRanking.Core.Messages
{
    public static class ErrorMessage
    {
        public const string NullError = "El campo '{PropertyName}' no puede ser nulo";
        public const string LengthError = "La longitud del campo '{PropertyName}' debe estar entre '{MinLength}' y '{MaxLength}' caractéres. Se han encontrado '{TotalLength}' caractéres.";
        public const string EmptyError = "El campo '{PropertyName}' no puede estar vacío";
        public const string MatchError = "El formato del campo '{PropertyName}' no corresponde";
        public const string ValueError = "Valor de '{PropertyName}' inválido";
        public const string ValueWithValueError = "Valor de '{PropertyName}' inválido. Valor: '{PropertyValue}'";
        public const string CountError = "Debe haber al menos 1 '{PropertyName}'";
        public const string PasswordError = "Las contraseñas no coinciden";
        public const string TelError = "El '{PropertyName}' debe tener un máximo de 10 dígitos.";
        public const string DecimalError = "'{PropertyName}' no debe tener más de '{ExpectedPrecision}' dígitos en total, con margen para '{ExpectedScale}' decimales. Se encontraron '{Digits}' dígitos y '{ActualScale}' decimales '{PropertyValue}'.";
        public const string MaxLengthError = "La longitud de '{PropertyName}' debe tener '{MaxLength}' caractéres o menos. Total ingresados: '{TotalLength}'.";
        public const string UpdateDateError = "La fecha de actualización debe ser la fecha actual.";
        public const string NotFoundEntity = "La entidad '{entity}' no posee registros.";
        public const string NotFoundEntityById = "La entidad '{entity}' no posee registros para el id '{id}'.";
        public const string EmailError = "El campo '{PropertyName}' debe ser un formato de correo electrónico válido.";
        public const string UniqueLengthError = "La longitud de '{PropertyName}' debe tener '{MaxLength}' caractéres. Total ingresados: '{TotalLength}'.";
        public const string DateError = "El formato de la fecha es invalido, debe ser (YYYYMMDD)";
        public const string GreaterThanZero = "El campo '{PropertyName}' debe ser mayor a cero.";
    }
}