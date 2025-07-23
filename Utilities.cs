using System.Data;
using System.Diagnostics;

namespace CommonFiles
{
    public static class Utilities
    {
        public static void OutputDataTable(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                // �񖼂��o��
                string columnNames = string.Join(", ", dataTable.Columns.Cast<DataColumn>().Select(col => col.ColumnName));
                Debug.WriteLine(columnNames);

                // �e�s�̒l���o��
                foreach (DataRow row in dataTable.Rows)
                {
                    string rowValues = string.Join(", ", row.ItemArray.Select(item => item?.ToString() ?? ""));
                    Debug.WriteLine(rowValues);
                }
            }
            else
            {
                Debug.WriteLine("No records found");
            }
        }
    }
}
