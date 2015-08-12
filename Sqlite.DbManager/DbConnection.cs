using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace Sqlite.DbManager
{
    public sealed class DbConnection : DbManagerBase
    {
        #region FIELDS
        private static string _connectionString = string.Empty;
        internal SQLiteConnection Connection = null;
        private bool _disposed = false;
        private readonly bool _explicitlyOpenConnection;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isExplicit">bool</param>
        public DbConnection(bool isExplicit)
        {
            _explicitlyOpenConnection = isExplicit;
        }
        #endregion

        // #################### M E T H O D S ####################

        #region Sets the database connection string
        /// <summary>
        /// Static function to set the database connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to set.</param>
        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;

        }
        #endregion

        #region Opens a connection
        /// <summary>
        /// Opens the the database connection.
        /// </summary>
        /// <returns>Whether the connection has been successfully opened or not.</returns>
        public bool Open()
        {
            return Open(_connectionString);
        }
        /// <summary>
        /// Opens the the database connection with a given connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>Whether the connection has been successfully opened or not.</returns>
        public bool Open(string connectionString)
        {
            _isOpen = false;

            Connection = new SQLiteConnection(connectionString);

            if (_explicitlyOpenConnection == true)
            {
                Connection.
                _isOpen = true;
            }


            return _isOpen;
        }
        #endregion

        #region Closes a connection
        /// <summary>
        /// Closes the connection (if open).
        /// </summary>
        public void Close()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
                Connection = null;
                _isOpen = false;
            }
        }
        #endregion

        #region Transaction handling related methods
        /// <summary>
        /// Starts a Transaction session on the current connection.
        /// </summary>
        public void BeginTran()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Transaction = Connection.BeginTransaction();
                _isActiveTransaction = true;
            }
        }
        /// <summary>
        /// Commits the currently active Transaction (if any).
        /// </summary>
        public void CommitTran()
        {
            if ((Connection.State == ConnectionState.Open) && _isActiveTransaction)
            {
                Transaction.Commit();
                _isActiveTransaction = false;
            }
        }
        /// <summary>
        /// Rools back the currently active Transaction (if any).
        /// </summary>
        public void RollbackTran()
        {
            if ((Connection.State == ConnectionState.Open) && _isActiveTransaction)
            {
                Transaction.Rollback();
                _isActiveTransaction = false;
            }
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Overwrite the Base class Derived method for[ Releases the resources. ]
        /// </summary>
        /// <param name="disposing">bool</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        // Release the managed resources you added in
                        // this derived class here.
                        if (Connection != null)
                        {
                            Connection.Dispose();
                        }


                        if (Transaction != null)
                        {
                            Transaction.Dispose();
                        }
                    }
                    // Release the native unmanaged resources you added
                    // in this derived class here.

                    _disposed = true;
                }
            }
            finally
            {
                // Call Dispose of base class.
                base.Dispose(disposing);
            }
        }
        #endregion

    }
}
