using DbCore.DbAdmin;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAccessIntegration
{
    public class DbAccess : DbData
    {
        public string connectionString { get; set; }
        private static DataProviderType AccessDataProvider { get { return DataProviderType.OleDb; } }

        /// <summary>
        /// Konstruktori qe krijon lidhjen me databazen e Access sipas Connection String
        /// </summary>
        /// <param name="dbPath">Pathi i db Access i cili duhet te jete share nese eshte ne rrjet</param>
        public DbAccess(string dbPath)
            : base(AccessDataProvider, GetConnectionString(dbPath))
        {
            connectionString = GetConnectionString(dbPath);
        }

        public DbAccess(transactionCache trans, string connectionString)
            : base(trans, AccessDataProvider, connectionString)
        {
        }

        public DbAccess(DbData db) : base(db) { }

        public void CompleteTransaction(ref DbAccess dbAccess)
        {
            var trans = new transactionCache();
            trans = this.TransCache.ShallowCopy();
            this.commitTransaksion();
            dbAccess = new DbAccess(trans, connectionString);
            dbAccess.connectionString = connectionString;
        }
        public void RollBackTransaction(ref DbAccess dbAccess)
        {
            var trans = new transactionCache();
            trans = this.TransCache.ShallowCopy();
            this.rollbackTransaksion();
            //ARSEN Pse e vendos dy here kete per tu siguruar:P
            dbAccess = new DbAccess(trans, connectionString);
            dbAccess.connectionString = connectionString;
        }

        /// <summary>
        /// Funksioni qe krijon Connection String duke perdorur pathin e databazes 
        /// </summary>
        /// <returns>Kthen Connection String si string</returns>
        private static string GetConnectionString(string dbPath)
        {
            return $"Provider=Microsoft.ACE.OLEDB.12.0; OLE DB Services=-4; Data Source={dbPath}";
        }

        /// <summary>
        /// Funksion Generic qe krijon query per perdorim ne databazen e access, sipas elementeve te tipit qe i kalohet si typeParam
        /// </summary>
        /// <typeparam name="T">Tipi i objektit qe i kalohet si parameter</typeparam>
        /// <param name="command">Enumeracion qe permban llojet e veprimeve Select,Insert,Update,Delete</param>
        /// <param name="classObject">Objekti i cili do te perdoret per veprimet ne databaze</param>
        /// <param name="table">Tabela ne te cilen do te behen veprimet</param>
        /// <param name="condition">Kushte qe mund te vendosen per ekzekutimi e query</param>
        /// <param name="primaryKey">Emrat i fushes Primary Key</param>
        /// <returns>Kthen nje query string</returns>
        public string QueryBuilder<T>(EnumSqlCommand command, T classObject, string table, string condition, params string[] primaryKey)
        {
            Type type = classObject.GetType();
            PropertyInfo[] classObjectAttributes = type.GetProperties().Where(x => !x.Name.EqualsAnyIgnoreCase(primaryKey)).ToArray();
            string[] propertiesNames = classObjectAttributes.Select(x => Convert.ToString(x.Name)).ToArray();
            string[] values = getValuesFromObject<T>(classObject, classObjectAttributes);
            bool hasConditions = !string.IsNullOrEmpty(condition);
            switch (command)
            {
                case EnumSqlCommand.Select:
                    return $"SELECT {string.Join(",", propertiesNames)} FROM {table} WHERE {(hasConditions ? condition : "1 = 1")} ";
                case EnumSqlCommand.Insert:
                    return $"INSERT INTO {table} ({string.Join(",", propertiesNames)}) VALUES ({string.Join(",", values)})";
                case EnumSqlCommand.Update:
                    string[] updateSetStrings = new string[propertiesNames.Length];
                    for (var i = 0; i < propertiesNames.Length; i++)
                        updateSetStrings[i] += $"{propertiesNames[i]} = {values[i]}";
                    return $"UPDATE {table} SET {string.Join(",", updateSetStrings)} WHERE {(hasConditions ? condition : "1 = 1")}";
                case EnumSqlCommand.Delete:
                    return $"DELETE FROM {table} WHERE {(hasConditions ? condition : "1 = 1")}";
            }
            return string.Empty;
        }

        /// <summary>
        /// Funksion qe krijon nje array me vlerat qe do te shkruhen ne databaze
        /// </summary>
        /// <typeparam name="T">Tipi i objektit qe i kalohet si parameter</typeparam>
        /// <param name="classObject">Objekti i cili do te perdoret per veprimet ne databaze</param>
        /// <param name="classObjectProperties">Atributet qe permban objekti i tipit T</param>
        /// <returns>Kthen nje array me gjithe vlerat ne objekt sipas rradhes</returns>
        private string[] getValuesFromObject<T>(T classObject, PropertyInfo[] classObjectAttributes)
        {
            int propertiesCount = classObjectAttributes.Count();
            string[] values = new string[propertiesCount];
            string value = string.Empty;
            for (int i = 0; i < propertiesCount; i++)
            {
                value = Convert.ToString(classObjectAttributes[i].GetValue(classObject));
                switch (classObjectAttributes[i].PropertyType.Name)
                {
                    case "Int32":
                    case "Int64":
                    case "Decimal":
                        values[i] = !string.IsNullOrEmpty(value) ? value : "0";
                        break;
                    case "String":
                        values[i] = !string.IsNullOrEmpty(value) ? "'" + value + "'" : "''";
                        break;
                    case "DateTime":
                        DateTime dt = new DateTime();
                        DateTime.TryParse(value, out dt);
                        values[i] = !string.IsNullOrEmpty(value) ? "'" + dt.ToString("MM/dd/yyyy HH:mm:ss") + "'" : "''";
                        break;
                }
            }
            return values;
        }

        /// <summary>
        /// Funksion qe ekzekuton ne databazen e access komande select
        /// </summary>
        /// <typeparam name="T">Tipi i objektit qe i kalohet si parameter</typeparam>
        /// <param name="classObject">Objekti i cili do te perdoret per veprimet ne databaze</param>
        /// <param name="table">Tabela ne te cilen do te behen veprimet</param>
        /// <param name="condition">Kushte qe mund te vendosen per ekzekutimi e query</param>
        /// <returns>Kthen nje DataTable me rreshtat e selektuar ne databaze</returns>
        public DataTable SelectObject<T>(T classObject, string table, string condition)
        {
            DataSet ds;
            dbManager.Open();
            ds = dbManager.ExecuteDataSet(CommandType.Text, QueryBuilder<T>(EnumSqlCommand.Select, classObject, table, condition, string.Empty));
            return ds.Tables[0];
        }

        /// <summary>
        /// Funksion qe ekzekuton ne databazen e access komande insert
        /// </summary>
        /// <typeparam name="T">Tipi i objektit qe i kalohet si parameter</typeparam>
        /// <param name="classObject">Objekti i cili do te perdoret per veprimet ne databaze</param>
        /// <param name="table">Tabela ne te cilen do te behen veprimet</param>
        /// <param name="primaryKey">Emrat i fushes Primary Key</param>
        /// <returns>Kthen mesazh gabimi nese deshton inserti, mesazh suksesi ne te kundert</returns>
        public clsMesazh InsertObject<T>(T classObject, out int primaryKeyValue, string table, params string[] primaryKey)
        {
            primaryKeyValue = 0;
            dbManager.Open();
            dbManager.ExecuteNonQuery(CommandType.Text, QueryBuilder<T>(EnumSqlCommand.Insert, classObject, table, string.Empty, primaryKey));
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Funksion qe ekzekuton ne databazen e access komande delete
        /// </summary>
        /// <typeparam name="T">Tipi i objektit qe i kalohet si parameter</typeparam>
        /// <param name="classObject">Objekti i cili do te perdoret per veprimet ne databaze</param>
        /// <param name="table">Tabela ne te cilen do te behen veprimet</param>
        /// <param name="condition">Kushte qe mund te vendosen per ekzekutimi e query</param>
        /// <returns>Kthen nje DataTable me rreshtat e selektuar ne databaze</returns>
        public clsMesazh DeleteObject<T>(T classObject, string table, string condition)
        {
            dbManager.Open();
            dbManager.ExecuteNonQuery(CommandType.Text, QueryBuilder<T>(EnumSqlCommand.Delete, classObject, table, condition, string.Empty));
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Funksion qe ekzekuton ne databazen e access komande insert per nje liste objektesh
        /// </summary>
        /// <typeparam name="T">Tipi i objektit qe i kalohet si parameter</typeparam>
        /// <param name="objectCollection">Lista me objekte te tipit T</param>
        /// <param name="table">Tabela ne te cilen do te behen veprimet</param>
        /// <param name="primaryKey">Emrat i fushes Primary Key</param>
        /// <returns>Kthen mesazh gabimi nese deshton inserti, mesazh suksesi ne te kundert</returns>
        public clsMesazh InsertObjectCollection<T>(List<T> objectCollection, string table, params string[] primaryKey)
        {
            foreach (T item in objectCollection)
            {
                dbManager.Open();
                dbManager.ExecuteNonQuery(CommandType.Text, QueryBuilder<T>(EnumSqlCommand.Insert, item, table, string.Empty, primaryKey));
            }
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Funksion qe ekzekuton ne databazen e access komande update
        /// </summary>
        /// <param name="queryString">Query</param>
        /// <returns>Kthen mesazh gabimi nese deshton inserti, mesazh suksesi ne te kundert</returns>
        public clsMesazh UpdateByQueryString(string queryString)
        {
            dbManager.Open();
            dbManager.ExecuteNonQuery(CommandType.Text, queryString);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Funksion qe ekzekuton ne databazen e access komande select
        /// </summary>
        /// <param name="queryString">Query</param>
        /// <returns>Kthen mesazh gabimi nese deshton inserti, mesazh suksesi ne te kundert</returns>
        public DataTable SelectByQueryString(string queryString)
        {
            DataSet ds;
            dbManager.Open();
            ds = dbManager.ExecuteDataSet(CommandType.Text, queryString);
            return ds.Tables[0];
        }


        /// <summary>
        /// Funksion qe ekzekuton ne databazen e access komande select
        /// </summary>
        /// <param name="queryString">Query</param>
        /// <returns>Kthen mesazh gabimi nese deshton inserti, mesazh suksesi ne te kundert</returns>
        public object SelectScalarByQueryString(string queryString)
        {
            dbManager.Open();
            return dbManager.ExecuteScalar(CommandType.Text, queryString);
        }
    }
}
