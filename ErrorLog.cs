using System;
using System.Data.SqlClient;
using System.Data;


namespace Convergence.TimerServiceEngine
{
    public class ErrorLog
    {
        public static void SaveErrorLog(string connectionString, ExceptionSeverityLevel errorType, string errorSource,
            int errorCode, string errorMessage, string errorDetail, string contextKeyValuePairs)
        {

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("ErrorLog_Insert", cnn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@ErrorType", SqlDbType = SqlDbType.VarChar, Value = errorType.ToString() });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@ErrorSource", SqlDbType = SqlDbType.VarChar, Value = errorSource });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@ErrorCode", SqlDbType = SqlDbType.Int, Value = errorCode });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@ErrorMessage", SqlDbType = SqlDbType.VarChar, Value = errorMessage });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@ErrorDetail", SqlDbType = SqlDbType.VarChar, Value = errorDetail });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@ContextKeyValuePairs", SqlDbType = SqlDbType.VarChar, Value = contextKeyValuePairs });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@CreatedBy", SqlDbType = SqlDbType.UniqueIdentifier, Value = Guid.Empty });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@CreatedOn", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now });

                cmd.ExecuteNonQuery();

                cmd.Dispose();
            }

        }
    }

    public enum ExceptionSeverityLevel
    {
        Debug = 1,
        Informational = 2,
        Warning = 3,
        Critical = 4,
        Fatal = 5
    }
}
