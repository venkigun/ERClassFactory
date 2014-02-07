using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ERClassFactory
{
    internal static class Commands
    {
        

        private static SqlCommand _listDBCmd = null;

        internal static SqlCommand ListDBCmd
        {
            get 
            {
                if (Commands._listDBCmd == null)
                {
                    Commands._listDBCmd = new SqlCommand();
                }
                return _listDBCmd;
            }
            //set { CmdToolBox._listAllDB = value; }
        }

        private static SqlCommand _listTablesCmd = null;

        internal static SqlCommand ListTablesCmd
        {
            get
            {
                if (Commands._listTablesCmd == null)
                {
                    Commands._listTablesCmd = new SqlCommand();
                }
                return _listTablesCmd;
            }
        }

        private static SqlCommand _listColumnsCmd = null;

        internal static SqlCommand ListColumnsCmd
        {
            get
            {
                if (Commands._listColumnsCmd == null)
                {
                    Commands._listColumnsCmd = new SqlCommand();
                }
                return _listColumnsCmd;
            }
        }
    }
}
