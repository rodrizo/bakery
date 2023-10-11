using System.Data;
using Oracle.ManagedDataAccess.Client;

public class DataConnection
{
    private OracleConnection Conexion;
    private string ConnectionString;

    public void AbrirConexion(string strConexion)
    {
        try
        {
            ConnectionString = strConexion;
            Conexion = new OracleConnection(strConexion);
            Conexion.Open();
        }
        catch
        {
            Conexion = null;
            throw;
        }
    }

    public void CerrarConexion()
    {
        try
        {
            Conexion.Close();
        }
        catch (Exception ex)
        {
            Conexion.Close();
        }
    }



    public object DBExecuteNonQueryReturnValue(OracleCommand objOracleCommand, string pstrReturnValue)
    {
        OracleCommand ComandoOracle;
        object lobjReturnValueObject = null;

        try
        {
            ComandoOracle = objOracleCommand;
            ComandoOracle.Connection = Conexion;
            ComandoOracle.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        if (!string.IsNullOrEmpty(pstrReturnValue))
            lobjReturnValueObject = null;//ComandoOracle.Parameters(pstrReturnValue).Value;

        return lobjReturnValueObject;
    }



    public bool GetOracleDbType(ref OracleParameter pobjOracleParameter, object valor)
    {
        bool repspuesta = true;
        OracleDbType lobjOracleDbType = OracleDbType.Varchar2;

        try
        {
            if (valor is DateTime)
                lobjOracleDbType = OracleDbType.Date;

            if (valor is Int64)
                lobjOracleDbType = OracleDbType.Int64;

            if (valor is Int32)
                lobjOracleDbType = OracleDbType.Int32;

            if (valor is Int16)
                lobjOracleDbType = OracleDbType.Int16;

            if (valor is sbyte)
                lobjOracleDbType = OracleDbType.Byte;

            if (valor is byte)
                lobjOracleDbType = OracleDbType.Int16;

            if (valor is decimal)
                lobjOracleDbType = OracleDbType.Decimal;

            if (valor is float)
                lobjOracleDbType = OracleDbType.Single;

            if (valor is double)
                lobjOracleDbType = OracleDbType.Double;

            if (valor is byte[])
                lobjOracleDbType = OracleDbType.Blob;

            pobjOracleParameter.OracleDbType = lobjOracleDbType;
            pobjOracleParameter.Value = valor;
        }
        catch
        {
            repspuesta = false;
        }

        return repspuesta;
    }


    public DataTable LlenarTabla(OracleCommand pobjOracleCommand)
    {
        OracleCommand lobjOracleCommand;
        DataTable Tabla = new DataTable();
        OracleDataAdapter lobjDataAdapter;

        try
        {
            lobjOracleCommand = pobjOracleCommand;
            lobjOracleCommand.Connection = Conexion;
            lobjDataAdapter = new OracleDataAdapter(pobjOracleCommand);
            lobjDataAdapter.Fill(Tabla);
            return Tabla;
        }
        catch
        {
            throw;
        }
    }


    public DataSet LlenarDataSet(OracleCommand pobjOracleCommand)
    {
        OracleCommand lobjOracleCommand;
        DataSet lobjDataSet = new DataSet();
        OracleDataAdapter lobjDataAdapter;

        try
        {
            lobjOracleCommand = pobjOracleCommand;
            lobjOracleCommand.Connection = Conexion;
            lobjDataAdapter = new OracleDataAdapter(pobjOracleCommand);
            lobjDataAdapter.Fill(lobjDataSet);
            return lobjDataSet;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
