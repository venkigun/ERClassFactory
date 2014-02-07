using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ERClassFactory
{
    public static class Generator
    {
        #region Properties
        //private static string _propertyType = null;

        //internal static string PropertyType
        //{
        //    get { return Generator._propertyType; }
        //    set { Generator._propertyType = value; }
        //}

        //private static string _propertyName = null;

        //internal static string PropertyName
        //{
        //    get { return Generator._propertyName; }
        //    set { Generator._propertyName = value; }
        //}

        //private static int rowIndex = 0;
        private static string _rawClassText = null;

        public static string RawClassText
        {
            get { return Generator._rawClassText; }
            set { Generator._rawClassText = value; }
        }
        private static int _propTypeIndex = 2;

        private static int PropTypeIndex
        {
            get { return _propTypeIndex; }
            set { _propTypeIndex = value; }
        }
        private static int _propNameIndex = 3;

        private static int PropNameIndex
        {
            get { return _propNameIndex; }
            set { _propNameIndex = value; }
        }
        #endregion

        public static void BeginGeneration(string rawText)
        {
            PropTypeIndex = 2;
            PropNameIndex = 3;
            RawClassText = rawText;
        }

        public static void EndGeneration()
        {
            RawClassText = RawClassText.Replace("<column_name>", "");
            string dbType = System.Configuration.ConfigurationManager.AppSettings["dbType"];
            RawClassText = RawClassText.Replace("<dbType>", dbType);
        }

        public static void Reset()
        {
            PropTypeIndex = 2;
            PropNameIndex = 3;
            RawClassText = String.Empty;
        }

        public static void AddTableDBName(string tableName, string dbName)
        {
            tableName = tableName.Replace(" ", "");
            RawClassText = 
                RawClassText.Replace("<table_name>", tableName).Replace("<database_name>", dbName);
            switch (Args.Type)
            {
                case enmConnStrType.Trusted:
                    RawClassText = 
                        RawClassText.Replace("<server_name>", Args.DataSource);
                    break;
                case enmConnStrType.Authorized:
                    RawClassText = 
                        RawClassText.Replace("<server_name>", Args.Server);
                    break;
                case enmConnStrType.Default:
                default:
                    RawClassText =
                        RawClassText.Replace("<server_name>", ".");
                    break;
            }
        }

        public static void AddField(string dataType, string colName)
        {
            //First Property Type to be filled:
            string propertyType = null;
            string propertyName = null;
            switch (dataType)
            {
                case "int":
                case "bigint":
                    propertyType = "int";
                    break;
                case "nvarchar":
                case "varchar":
                case "text":
                case "ntext":
                    propertyType = "string";
                    break;
                case "datetime":
                    propertyType = "DateTime";
                    break;
                case "smalldatetime":
                    propertyType = "DateTime";
                    break;
                case "money":
                case "bigmoney":
                case "smallmoney":
                    propertyType = "double";
                    break;
                case "image":
                    propertyType = "byte[]";
                    break;
                default:
                    propertyType = "object";
                    break;
            }
            //Than propertyName is set:
            propertyName = colName.Replace(" ", "");
            //Now add fields to raw class text
            string fieldText =
                String.Format("public {0} {1}", propertyType, propertyName) + " { get; set; }\n\t\t\t<column_name>";
            //Now for the first time prevFieldText is e.g. "public int productId { get; set; }" 
            RawClassText = RawClassText.Replace("<column_name>", fieldText);
        }

        public static void AddParametersToSaveMethod(DataTable table)
        {
            string paramsText = string.Empty;
            string dbParamType = System.Configuration.ConfigurationManager.AppSettings["dbParamType"];
            foreach (DataRow row in table.Rows)
            {
                string size = row["CHARACTER_MAXIMUM_LENGTH"] == DBNull.Value ? "0" : row["CHARACTER_MAXIMUM_LENGTH"].ToString();
                paramsText += AddParamField(dbParamType, row["DATA_TYPE"].ToString(), row["COLUMN_NAME"].ToString(), size);
            }
            RawClassText = RawClassText.Replace("<save_params>", paramsText);
        }

        private static string AddParamField(string dbType, string dataType, string colName, string size)
        {
            //First Property Type to be filled:
            string paramType = null;
            string propertyName = null;
            string paramSize = size == null? "0": size;
            string paramText = Constants._sqlCmdPrmBeginCommon;
            switch (dataType)
            {
                case "int":
                    paramType = dbType+".Int";
                    paramText += Constants._sqlCmdPrmCommon;
                    break;
                case "bigint":
                    paramType = dbType+".BigInt";
                    paramText += Constants._sqlCmdPrmCommon;
                    break;
                case "nvarchar":
                    paramType = dbType + ".NVarChar";
                    paramText += Constants._sqlCmdPrmStr;
                    break;
                case "varchar":
                    paramType = dbType + ".VarChar";
                    paramText += Constants._sqlCmdPrmStr;
                    break;
                case "text":
                    paramType = dbType + ".Text";
                    paramText += Constants._sqlCmdPrmStr;
                    break;
                case "ntext":
                    paramType = dbType + ".NText";
                    paramText += Constants._sqlCmdPrmStr;
                    break;
                case "datetime":
                    paramType = dbType + ".DateTime";
                    paramText += Constants._sqlCmdPrmDate;
                    break;
                case "smalldatetime":
                    paramType = dbType + ".SmallDateTime";
                    paramText += Constants._sqlCmdPrmDate;
                    break;
                case "money":
                    paramType = dbType + ".Money";
                    paramText += Constants._sqlCmdPrmCommon;
                    break;
                case "smallmoney":
                    paramType = dbType + ".SmallMoney";
                    paramText += Constants._sqlCmdPrmCommon;
                    break;
                case "image":
                    paramType = ".Binary";
                    paramText += Constants._sqlCmdPrmCommon;
                    break;
                default:
                    paramType = "NULL";
                     paramText += Constants._sqlCmdPrmCommon;
                   break;
            }
            paramText += Constants._sqlCmdPrmEndCommon;
            //Than propertyName is set:
            propertyName = colName.Replace(" ", "");
            paramText = paramText.Replace("<param>", propertyName+"Param");
            paramText = paramText.Replace("<propertyName>", propertyName);
            paramText = paramText.Replace("<type>", paramType);
            paramText = paramText.Replace("<size>", size);
            return paramText;
            //Now add parameter to method
        }

        public static void AddGetEntityFields(DataTable table)
        {
            string paramsText = string.Empty;
            string dbParamType = System.Configuration.ConfigurationManager.AppSettings["dbParamType"];
            foreach (DataRow row in table.Rows)
            {
                string size = row["CHARACTER_MAXIMUM_LENGTH"] == DBNull.Value ? "0" : row["CHARACTER_MAXIMUM_LENGTH"].ToString();
                paramsText += AddGetPropertyField(row["DATA_TYPE"].ToString(), row["COLUMN_NAME"].ToString());
            }
            RawClassText = RawClassText.Replace("<get_properties>", paramsText);
        }

        private static string AddGetPropertyField(string dataType, string colName)
        {
            //First Property Type to be filled:
            string dataConverter = null;
            string propertyName = null;
            string default_value = string.Empty;
            string paramText = Constants._sqlGetParams;
            switch (dataType)
            {
                case "int":
                    dataConverter = "Convert.ToInt32";
                    default_value = "\"0\"";
                    break;
                case "bigint":
                    dataConverter = "Convert.ToInt64";
                    default_value = "\"0\"";
                    break;
                case "nvarchar":
                case "varchar":
                case "text":
                case "ntext":
                    dataConverter = "Convert.ToString";
                    default_value = "string.Empty";
                    break;
                case "datetime":
                case "smalldatetime":
                    dataConverter = "Convert.ToDateTime";
                    default_value = "new DateTime()";
                    break;
            }
            //Than propertyName is set:
            propertyName = colName.Replace(" ", "");
            paramText = paramText.Replace("<propertyName>", propertyName);
            paramText = paramText.Replace("<data_converter>", dataConverter);
            paramText = paramText.Replace("<default_value>", default_value);
            return paramText;
            //Now add parameter to method
        }

    }
}
