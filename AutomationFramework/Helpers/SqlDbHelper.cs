using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using TechTalk.SpecFlow;

namespace AutomationFramework.Helpers
{
    public static class SqlDbHelper
    {
        static StringBuilder sbLog = new StringBuilder();

        /// <summary>
        /// Executes a SQL command and reads the text of the outcome to determine pass or fail
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="query"></param>
        /// <returns>Bool</returns>
        public static bool AssertDatabaseResults(this SqlConnection sqlConnection, string query)
        {
            var spCommand = new SqlCommand();
            spCommand.Connection = sqlConnection;
            spCommand.CommandText = query;
            spCommand.CommandType = CommandType.Text;
            if (sqlConnection == null || ((sqlConnection != null && (sqlConnection.State == ConnectionState.Closed || sqlConnection.State == ConnectionState.Broken))))
                sqlConnection.Open();
            sqlConnection.InfoMessage += new SqlInfoMessageEventHandler(sqlConnection_InfoMessage);
            spCommand.ExecuteScalar();
            sqlConnection.Close();
            var result = sbLog.ToString();
            if (result.Contains("Pass"))
                return true;
            return false;
        }

        /// <summary>
        /// Delegate to handle building a string log from SQL notifications
        /// </summary>
        /// <param name="sender">Delegate requesting</param>
        /// <param name="e">Event messages</param>
        internal static void sqlConnection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            sbLog.AppendLine(e.Message);
        }
    }
}
