using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sqlite.DbManager
{
    public abstract class DbManagerBase : IDisposable
    {
        #region Private Properties
        // Track whether Dispose has been called.
        private bool _isDisposed;
        // The resource _handle
        private IntPtr _handle = IntPtr.Zero;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value indicating whether this instance is _disposed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is _disposed; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisposed
        {
            get
            {
                return _isDisposed;
            }
        }
        #endregion Public Properties

        #region IDisposable Members
        private event System.EventHandler _disposed;

        /// <summary>
        /// Occurs when this instance is _disposed.
        /// </summary>
        public event System.EventHandler Disposed
        {
            add
            {
                _disposed += value;
            }
            remove
            {
                _disposed -= value;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                // Make this routine thread-safe
                lock (this)
                {
                    // Check to see if Dispose has already been called.
                    if (!_isDisposed)
                    {
                        if (disposing)
                        {
                            //Free managed resources
                            EventHandler handler = _disposed;
                            if (handler != null)
                            {
                                handler(this, EventArgs.Empty);
                                handler = null;
                            }
                        }

                        //Free unmanaged resources
                        if (_handle != IntPtr.Zero)
                        {
                            //CloseHandle();
                            _handle = IntPtr.Zero;
                        }
                    }
                }
            }
            finally
            {
                _isDisposed = true;
            }
        }
        #endregion IDisposable Members

        #region CheckDisposed
        /// <summary>
        ///    <para>
        ///        Checks if the instance has been _disposed of, and if it has, throws an <see cref="ObjectDisposedException"/>; otherwise, does nothing.
        ///    </para>
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///    The instance has been _disposed of.
        ///    </exception>
        ///    <remarks>
        ///    <para>
        ///        Derived classes should call this method at the start of all methods and properties that should not be accessed after a call to <see cref="Dispose()"/>.
        ///    </para>
        /// </remarks>
        protected void CheckDisposed()
        {
            if (_isDisposed)
            {
                string typeName = GetType().FullName;

                // TODO: You might want to move the message string into a resource file
                throw new ObjectDisposedException(
                    typeName,
                    String.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "Cannot access a _disposed {0}.",
                        typeName));
            }
        }
        #endregion
    }
}
