using System.Collections;
using System.Text;
using ChatRoomWithBot.Domain.Interfaces;

namespace ChatRoomWithBot.Domain.OperationResults
{
	public class OperationResult<T> : IOperationResult<T>
	{
		public T Data { get; set; }
		public bool Success { get; set; }

		public bool Error => !Success;
		public List<string> Errors { get; set; } = [];
		public int Code { get; set; } = 1;

		public string Message => GetMessagesError();

		private string GetMessagesError()
		{
			try
			{
				var sb = new StringBuilder();
				foreach (var e in Errors)
				{
					sb.AppendLine(e);
				}

				return sb.ToString()
					.Replace("\r","")
					.Replace("\n","");
			}
			catch (Exception e)
			{
				return string.Empty;
			}
		}

		public static OperationResult<T> CreateSuccessResult(T result)
		{
			if (!typeof(IEnumerable).IsAssignableFrom(typeof(T)))
				return new OperationResult<T>
				{
					Success = true,
					Data = result
				};
			var totalRecords = 0;

			if (result == null)
				return new OperationResultIEnumerable<T>
				{
					Success = true,
					Errors = [],
					Data = result,
					TotalRecords = totalRecords
				};

			var enumerable = (IEnumerable)result;
			totalRecords = enumerable.Cast<object>()
				.Count(); // Usando Cast<object>() para lidar com IEnumerable não genérico




			return new OperationResultIEnumerable<T>
			{
				Success = true,
				Errors = [],
				Data = result,
				TotalRecords = totalRecords
			};
		}

		public static OperationResult<T> CreateFailureResult(string error, T obj)
		{
			if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
			{

				return new OperationResultIEnumerable<T>
				{
					Success = false,
					Errors = [error],
					TotalRecords = 0,
					Data = obj,
				};
			}

			return new OperationResult<T>
			{
				Success = false,
				Errors = [error],
				Data = obj,
			};
		}

		public static OperationResult<T> CreateFailureResult(IEnumerable<string> errors, T obj)
		{

			if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
			{
				return new OperationResultIEnumerable<T>
				{
					Success = false,
					Errors = [..errors],
					TotalRecords = 0,
					Data = obj,
				};
			}

			return new OperationResult<T>
			{
				Success = false,
				Errors = [..errors],
				Data = obj,
			};
		}


		public static OperationResult<T> CreateFailureResult(IList<string> errors, T obj)
		{

			if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
			{
				return new OperationResultIEnumerable<T>
				{
					Success = false,
					Errors = [..errors],
					TotalRecords = 0,
					Data = obj,
				};
			}

			return new OperationResult<T>
			{
				Success = false,
				Errors = [..errors],
				Data = obj,
			};
		}


	}
}
