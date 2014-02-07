using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ERClassFactory
{
    public static class Adapters
    {
        private static SqlDataAdapter _serverDA = null;

        public static SqlDataAdapter ServerDA
        {
            get 
            {
                if (Adapters._serverDA == null)
                {
                    Commands.ListDBCmd.CommandText = Constants.ListDBStr;
                    Commands.ListDBCmd.Connection = Connections.GetConnection();
                    Adapters._serverDA =
                                new SqlDataAdapter(Commands.ListDBCmd); 
                }
                return Adapters._serverDA; 
            }
            //set { Adapters._serverDataAdapter = value; }
        }
        private static SqlDataAdapter _lstTablesDA = null;

        public static SqlDataAdapter LstTablesDA 
        {
            get
            {
                Args.InitialCatalog = Args.WheredDB;
                //Here Args.WheredDB must be set
                Commands.ListTablesCmd.CommandText = Constants.ListTablesStr;
                Commands.ListTablesCmd.Connection = Connections.GetConnection();
                Adapters._lstTablesDA = new SqlDataAdapter(Commands.ListTablesCmd);
                return Adapters._lstTablesDA;
            }
            //set { Adapters._lstTablesDataAdapter = value; }
        }

        private static SqlDataAdapter _lstColumnsDA = null;

        public static SqlDataAdapter LstColumnsDA
        {
            get 
            {
                //Here Args.WheredTable must be set
                Commands.ListColumnsCmd.CommandText =
                        String.Format(Constants.ListColumnsStr, Args.WheredTable);
                Commands.ListColumnsCmd.Connection = Connections.GetConnection();
                Adapters._lstColumnsDA = new SqlDataAdapter(Commands.ListColumnsCmd);
                return Adapters._lstColumnsDA; 
            }
            //set { Adapters._lstColumnsDA = value; }
        }
    }
}
