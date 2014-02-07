using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERClassFactory.Exceptions;

namespace ERClassFactory
{
    public static class Args
    {
        //internal static Args(enmConnStrType type, params string[] args)
        //{
        //    switch (type)
        //    {
        //        case enmConnStrType.Trusted:
        //            if (args.Length == 2)
        //            {
        //                DataSource = args[0];
        //                InitialCatalog = args[1];
        //                Type = type;
        //            }                   
        //            else
        //                throw new ConnStrBuilderException();
        //            break;
        //        case enmConnStrType.Authorized:
        //            if (args.Length == 4)
        //            {
        //                Server = args[0];
        //                Database = args[1];
        //                UserID = args[2];
        //                Password = args[3];
        //                Type = type;
        //            }
        //            else
        //                throw new ConnStrBuilderException();
        //            break;
        //        case enmConnStrType.Default:
        //            DataSource = null;
        //            InitialCatalog = null;
        //            Server = null;
        //            Database = null;
        //            UserID = null;
        //            Password = null;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public static void Reset()
        {
            Database = 
                DataSource = 
                InitialCatalog = 
                Password = 
                Server = 
                UserID = null;
            Type = enmConnStrType.Default;
        }

        private static enmConnStrType _type = enmConnStrType.Default;

        public static enmConnStrType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private static string _dataSource = null;

        public static string DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }
        private static string _initialCatalog = null;

        public static string InitialCatalog
        {
            get { return _initialCatalog; }
            set { _initialCatalog = value; }
        }
        private static string _server = null;

        public static string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        private static string _database = null;

        public static string Database
        {
            get { return _database; }
            set { _database = value; }
        }
        private static string _userID = null;

        public static string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        private static string _password = null;

        public static string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private static string _wheredTable = null;

        public static string WheredTable
        {
            get { return _wheredTable; }
            set { _wheredTable = value; }
        }

        private static string _wheredDB = null;

        public static string WheredDB
        {
            get { return _wheredDB; }
            set { _wheredDB = value; }
        }
    }
}
