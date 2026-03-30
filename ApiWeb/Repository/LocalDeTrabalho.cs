 using ApiWeb.Data;

public class LocalDeTrabalhoRepository
{
    private readonly DatabaseContext _context = new DatabaseContext();

    public int Inserir(string atividade, string parede, string piso, string iluminacao, string ventilacao)
    {
        using var conn = _context.GetConnection();
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO Local_De_Trabalho 
            (local_Atividade, parede, piso, iluminacao, ventilacao)
            VALUES (@atividade, @parede, @piso, @iluminacao, @ventilacao);
        ";

        cmd.Parameters.AddWithValue("@atividade", atividade);
        cmd.Parameters.AddWithValue("@parede", parede);
        cmd.Parameters.AddWithValue("@piso", piso);
        cmd.Parameters.AddWithValue("@iluminacao", iluminacao);
        cmd.Parameters.AddWithValue("@ventilacao", ventilacao);

        cmd.ExecuteNonQuery();

        var cmdId = conn.CreateCommand();
        cmdId.CommandText = "SELECT last_insert_rowid();";

        long id = (long)cmdId.ExecuteScalar();

        return (int)id;
    }
}
  