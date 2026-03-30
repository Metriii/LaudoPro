using ApiWeb.Data;
using Microsoft.Data.Sqlite;

public class EnderecoRepository
{
    private readonly DatabaseContext _context = new DatabaseContext();

    public int Inserir(string rua, string bairro, string numero, string cidade)
    {
        using var conn = _context.GetConnection();
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO Endereco (rua, bairro, numero, cidade)
            VALUES (@rua, @bairro, @numero, @cidade);
        ";

        cmd.Parameters.AddWithValue("@rua", rua);
        cmd.Parameters.AddWithValue("@bairro", bairro);
        cmd.Parameters.AddWithValue("@numero", numero);
        cmd.Parameters.AddWithValue("@cidade", cidade);

        cmd.ExecuteNonQuery();

        // 🔥 pega o ID correto
        var cmdId = conn.CreateCommand();
        cmdId.CommandText = "SELECT last_insert_rowid();";

        long id = (long)cmdId.ExecuteScalar();

        return (int)id;
    }
}