using ApiWeb.Data;

public class ReclamanteRepository
{
    private readonly DatabaseContext _context = new DatabaseContext();

    public void Inserir(string nome, int idLaudo)
    {
        using var conn = _context.GetConnection();
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO Reclamantes (nome, id_laudo)
            VALUES (@nome, @id_laudo);
        ";

        cmd.Parameters.AddWithValue("@nome", nome);
        cmd.Parameters.AddWithValue("@id_laudo", idLaudo);

        cmd.ExecuteNonQuery();
    }
}