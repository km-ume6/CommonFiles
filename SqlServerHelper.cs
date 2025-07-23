using Microsoft.Data.SqlClient;
using System.Data;

namespace CommonFiles
{
    /// <summary>
    /// SQL Server�ւ̔ėp�A�N�Z�X�w���p�[�N���X�B
    /// �ڑ��Ǘ��A�N�G�����s�A�p�����[�^�Ή��A���\�[�X������T�|�[�g���܂��B
    /// </summary>
    /// <example>
    /// <code>
    /// using var db = new SqlServerHelper("Server=...;Database=...;User Id=...;Password=...;");
    /// var dt = db.ExecuteQuery("SELECT * FROM Users WHERE Id = @id", new SqlParameter("@id", 1));
    /// int count = db.ExecuteNonQuery("UPDATE Users SET Name = @name WHERE Id = @id", new SqlParameter("@name", "�V�������O"), new SqlParameter("@id", 1));
    /// object? value = db.ExecuteScalar("SELECT COUNT(*) FROM Users");
    /// </code>
    /// </example>
    public class SqlServerHelper : IDisposable
    {
        private readonly SqlConnection _connection;
        static public string ConnectionString { get; set; } = string.Empty;

        public SqlServerHelper()
        {
            if (string.IsNullOrEmpty(ConnectionString))
                throw new InvalidOperationException("ConnectionString is not set. Please set it before using SqlServerHelper.");

            // �C��: �R���X�g���N�^�̌Ăяo���ł͂Ȃ��A�t�B�[���h�̏��������s��
            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
        }

        /// <summary>
        /// �ڑ��������SQL Server�֐ڑ����܂��B
        /// </summary>
        /// <param name="connectionString">SQL Server�̐ڑ�������</param>
        public SqlServerHelper(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        /// <summary>
        /// SELECT���Ȃǂ̃N�G�������s���A���ʂ�DataTable�Ŏ擾���܂��B
        /// </summary>
        /// <param name="sql">SQL��</param>
        /// <param name="parameters">SQL�p�����[�^�i�ȗ��j</param>
        /// <returns>�N�G�����ʂ�DataTable</returns>
        public DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            using var cmd = new SqlCommand(sql, _connection);
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            using var adapter = new SqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        /// <summary>
        /// INSERT/UPDATE/DELETE�Ȃǂ̔�N�G��SQL�����s���܂��B
        /// </summary>
        /// <param name="sql">SQL��</param>
        /// <param name="parameters">SQL�p�����[�^�i�ȗ��j</param>
        /// <returns>�e�����󂯂��s��</returns>
        public int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using var cmd = new SqlCommand(sql, _connection);
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// �P��l�i�W�v�l�Ȃǁj���擾���܂��B
        /// </summary>
        /// <param name="sql">SQL��</param>
        /// <param name="parameters">SQL�p�����[�^�i�ȗ��j</param>
        /// <returns>�擾�����l�iobject�^�Anull�j</returns>
        public object? ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using var cmd = new SqlCommand(sql, _connection);
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// �ڑ���j�����A���\�[�X��������܂��B
        /// </summary>
        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}