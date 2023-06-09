// Taken from this gist https://gist.github.com/krzys-h/9062552e33dd7bd7fe4a6c12db109a1a

using System;
using UnityEngine.Networking;

namespace OpenAi.Api.V1
{
	/// <summary>
	/// Provides a get waiter extension to Unity web requests to allow it to be awaited
	/// </summary>
	public static partial class ExtensionMethods
	{
		public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
		{
			return new UnityWebRequestAwaiter(asyncOp);
		}
    }
}