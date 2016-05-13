using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using VentsMaterials;


namespace SwSpecification.DataTableClass
{
    internal class DataTableSql
    {
        private readonly ConnectSqlString _con = new ConnectSqlString();

        public DataTable DocTypeDt()
        {
            ToSQL.Conn = _con.Con;

            var docTypeTable = new DataTable();

            try
            {
                const string query = "select Code, Name from SWPlus.DocType";

                var sqlConnection = new SqlConnection(ToSQL.Conn);
                var sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();

                var sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(docTypeTable);

                sqlConnection.Close();

                sqlDataAdapter.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return docTypeTable;
        }

        public DataTable SectionDt()
        {
            ToSQL.Conn = _con.Con;

            var sectionTable = new DataTable();

            try
            {
                const string query = "select SectionsID, Name from SWPlus.Sections";

                var sqlConnection = new SqlConnection(ToSQL.Conn);
                var sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();

                var sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(sectionTable);

                sqlConnection.Close();

                sqlDataAdapter.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return sectionTable;
        }
    }
}
