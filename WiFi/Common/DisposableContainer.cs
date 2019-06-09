using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFiUtil
{
	internal class DisposableContainer<T> : IDisposable where T : IDisposable, new()
	{
		private readonly bool _isDefault;
		public T Content { get; }

		public DisposableContainer(T content)
		{
			_isDefault = EqualityComparer<T>.Default.Equals(content, default(T));
			this.Content = _isDefault ? new T() : content;
		}

		#region Dispose

		bool _disposed = false;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			if (disposing)
			{
				if (_isDefault)
					Content.Dispose();
			}

			_disposed = true;
		}

		#endregion
	}
}
