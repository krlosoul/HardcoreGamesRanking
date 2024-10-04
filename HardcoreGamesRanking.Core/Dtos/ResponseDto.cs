namespace HardcoreGamesRanking.Core.Dtos
{
	public class ResponseDto
	{
		public string? Message { get; set; }
	}

	public class ResponseDto<T>: ResponseDto
	{
		public T? Data { get; set; }
		public int? TotalRecords { get; set; }
	}
}