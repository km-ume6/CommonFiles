using System.Data;
using System.Diagnostics;
using System.IO;

namespace ImageTool
{
    public static class Utilities
    {
        /// <summary>
        /// �w�肳�ꂽDataTable��FileName������ɁASortingFolder���̊Y���t�@�C�����폜���܂��B
        /// </summary>
        /// <param name="dt">�폜�Ώۃt�@�C�������܂�DataTable</param>
        /// <param name="sortingFolderPath">�t�@�C���폜�Ώۂ̃t�H���_�p�X</param>
        public static void DeleteFilesFromDataTable(DataTable dt, string sortingFolderPath)
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string fileName = row["FileName"].ToString() ?? "Unknown";
                    string filePath = Path.Combine(sortingFolderPath, fileName);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        Debug.WriteLine($"Deleted: {filePath}");
                    }
                    else
                    {
                        Debug.WriteLine($"File not found: {filePath}");
                    }
                }
            }
            else
            {
                Debug.WriteLine("No records found for file deletion.");
            }
        }
    }
}
