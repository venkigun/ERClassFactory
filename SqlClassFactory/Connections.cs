using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ERClassFactory.Exceptions;

namespace ERClassFactory
{
    public enum enmConnStrType
    {
        Trusted,
        Authorized,
        Default
    }

    internal static class Connections
    {
        #region private fields

        private static SqlConnection _connection = null;

        #endregion

        /// <summary>
        /// First To Call Before Extracting Connection
        /// </summary>
        /// <param name="type">enmConnStrType for the type of connection</param>
        /// <param name="args">Enter parameters like userid ect.</param>
        internal static SqlConnection GetConnection()
        {
            switch (Args.Type)
	        {
		        case enmConnStrType.Trusted:
                    return _connection = 
                        new SqlConnection(
                        String.Format(Constants.ConnStrTrusted, Args.DataSource, Args.InitialCatalog));
                case enmConnStrType.Authorized:
                     return _connection = 
                         new SqlConnection(
                             String.Format(
                             Constants.ConnStrAuth, 
                             Args.Server, Args.Database, Args.UserID, Args.Password));
                case enmConnStrType.Default:
                default:
                 return null;
	        }
        }
    }
}
