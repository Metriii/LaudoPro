using ApiWeb.Data;

public class LaudoRepository
{
    private readonly DatabaseContext _context = new DatabaseContext();

    public int Inserir(string numeroVara, string numeroPericia, int idTipo, int idEndereco, int idLocal)
    {
        using var conn = _context.GetConnection();
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO Laudo 
            (numero_Vara, numero_Pericia, id_tipo, id_endereco, id_local_de_trabalho)
            VALUES (@vara, @pericia, @tipo, @endereco, @local);
        ";

        cmd.Parameters.AddWithValue("@vara", numeroVara);
        cmd.Parameters.AddWithValue("@pericia", numeroPericia);
        cmd.Parameters.AddWithValue("@tipo", idTipo);
        cmd.Parameters.AddWithValue("@endereco", idEndereco);
        cmd.Parameters.AddWithValue("@local", idLocal);

        cmd.ExecuteNonQuery();
        var cmdId = conn.CreateCommand();
        cmdId.CommandText = "SELECT last_insert_rowid();";

        long id = (long)cmdId.ExecuteScalar();

        return (int)id;
    }
}