namespace HardcoreGamesRanking.Business.Common.Exceptions
{
	public class BadRequestException : Exception
	{
		public BadRequestException(string message) : base(message) { }
	}
}