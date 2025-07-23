using System.Data;
using System.Diagnostics;
using System.IO;

namespace ImageTool
{
    public static class Utilities
    {
        /// <summary>
        /// 指定されたDataTableのFileName列を元に、SortingFolder内の該当ファイルを削除します。
        /// </summary>
        /// <param name="dt">削除対象ファイル情報を含むDataTable</param>
        /// <param name="sortingFolderPath">ファイル削除対象のフォルダパス</param>
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
