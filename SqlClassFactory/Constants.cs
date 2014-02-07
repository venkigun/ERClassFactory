using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERClassFactory
{
    internal static class Constants
    {
        private static string _listDBStr =
                @"select * from sys.databases";

        public static string _sqlCmdPrmBeginCommon = System.Environment.NewLine + String.Format("\t\t\t\t\t\t")+ @"<dbType>Parameter <param> = new <dbType>Parameter();" 
            + System.Environment.NewLine + String.Format("\t\t\t\t\t\t") + "<param>.ParameterName = \"@<propertyName>\";" 
            + System.Environment.NewLine + String.Format("\t\t\t\t\t\t") + "<param>.<dbType>DbType = <type>;" 
            + System.Environment.NewLine + String.Format("\t\t\t\t\t\t") + "<param>.Direction = ParameterDirection.Input;"
            + System.Environment.NewLine + String.Format("\t\t\t\t\t\t") + "<param>.Size = <size>;";
        public static string _sqlCmdPrmStr = System.Environment.NewLine + String.Format("\t\t\t\t\t\t") + @"if (string.IsNullOrEmpty(this.<propertyName>))"
            + System.Environment.NewLine + String.Format("\t\t\t\t\t\t\t") + "<param>.Value = DBNull.Value;"
            + System.Environment.NewLine + String.Format("\t\t\t\t\t\t") + "else"
            + System.Environment.NewLine + String.Format("\t\t\t\t\t\t\t") + "<param>.Value = this.<propertyName>;";
        public static string _sqlCmdPrmDate = System.Environment.NewLine + String.Format("\t\t\t\t\t\t") + @"if (this.<propertyName> == null || this.<propertyName> == new DateTime())"
            + System.Environment.NewLine + String.Format("\t\t\t\t\t\t\t") + "<param>.Value = DBNull.Value;"
            + System.Environment.NewLine + String.Format("\t\t\t\t\t\t") + "else"
            + System.Environment.NewLine + String.Format("\t\t\t\t\t\t\t") + "<param>.Value = this.<propertyName>;";
        public static string _sqlCmdPrmCommon = System.Environment.NewLine + String.Format("\t\t\t\t\t\t") + @"<param>.Value = this.<propertyName>;";
        public static string _sqlCmdPrmEndCommon = System.Environment.NewLine + String.Format("\t\t\t\t\t\t") + @"cmd.Parameters.Add(<param>);" + System.Environment.NewLine;
        public static string _sqlGetParams = System.Environment.NewLine + String.Format("\t\t\t\t\t\t") + @"this.<propertyName> = <data_converter>(reader[""<propertyName>""] == DBNull.Value ? <default_value> : reader[""<propertyName>""]);";
        internal static string ListDBStr
        {
            get { return Constants._listDBStr; }
            set { Constants._listDBStr = value; }
        }
        private static string _listTablesStr =
                @"select * from INFORMATION_SCHEMA.Tables
                where TABLE_TYPE = 'BASE TABLE'";

        internal static string ListTablesStr
        {
            get { return Constants._listTablesStr; }
            set { Constants._listTablesStr = value; }
        }

        
        private static string _listColumnsStr =
            @"select * from INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME='{0}'";

        internal static string ListColumnsStr
        {
            get { return Constants._listColumnsStr; }
            set { Constants._listColumnsStr = value; }
        }

        private static string _connStrTrusted =
            @"Data Source={0};
            Initial Catalog={1};
            Integrated Security=SSPI;";

        internal static string ConnStrTrusted
        {
            get { return Constants._connStrTrusted; }
            set { Constants._connStrTrusted = value; }
        }

        private static string _connStrAuth =
            @"Server={0};
            Database={1};
            User ID={2};
            Password={3};
            Trusted_Connection=False;";

        internal static string ConnStrAuth
        {
            get { return Constants._connStrAuth; }
            set { Constants._connStrAuth = value; }
        }
        //public static string SQLCommandString
        //{
        //    get { return Constants._sqlCmdStr; }
        //}
    }
}
