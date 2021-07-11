// Taken from this gist https://gist.github.com/krzys-h/9062552e33dd7bd7fe4a6c12db109a1a

using System;
using System.Runtime.CompilerServices;

using UnityEngine;
using UnityEngine.Networking;

namespace OpenAi.Api.V1
{
	/// <summary>
	/// Allows the UnityWebRequest object to be awaited
	/// </summary>
	public class UnityWebRequestAwaiter : INotifyCompletion
	{
		private UnityWebRequestAsyncOperation asyncOp;
		private Action continuation;

		public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
		{
			this.asyncOp = asyncOp;
			asyncOp.completed += OnRequestCompleted;
		}

		public bool IsCompleted { get { return asyncOp.isDone; } }

		public void GetResult() { }

		public void OnCompleted(Action continuation)
		{
			this.continuation = continuation;
		}

		private void OnRequestCompleted(AsyncOperation obj)
		{
			continuation();
		}
	}
}