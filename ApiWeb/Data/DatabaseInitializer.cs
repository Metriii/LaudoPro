using System;
using Microsoft.Data.Sqlite;

namespace Laudo.Data
{
    public class DatabaseInitializer
    {
        public static void Inicializar()
        {
            string connString = "Data Source=LaudoPro.db";

            using (var conn = new SqliteConnection(connString))
            {
                conn.Open();

                var cmd = conn.CreateCommand();

                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Tipo_de_Laudo(
                        id_tipo INTEGER PRIMARY KEY AUTOINCREMENT,
                        nome TEXT
                    );

                    INSERT OR IGNORE INTO Tipo_de_Laudo (id_tipo, nome)
                    VALUES
                    (1,'Periculosidade'),
                    (2,'Insalubridade'),
                    (3,'Insalubridade/Periculosidade');

                    CREATE TABLE IF NOT EXISTS Estruturas(
                        id_estrutura INTEGER PRIMARY KEY AUTOINCREMENT,
                        id_tipo INTEGER,
                        FOREIGN KEY(id_tipo) REFERENCES Tipo_de_Laudo(id_tipo)
                    );

                    CREATE TABLE IF NOT EXISTS Anexos_Periculosidade(
                        id_Anexos_Periculosidade INTEGER PRIMARY KEY AUTOINCREMENT,
                        id_estrutura INTEGER,
                        anexos TEXT NOT NULL,
                        FOREIGN KEY(id_estrutura) REFERENCES Estruturas(id_estrutura)
                    );

                    CREATE TABLE IF NOT EXISTS Anexos_Insalubridade(
                        id_Anexos_Insalubridade INTEGER PRIMARY KEY AUTOINCREMENT,
                        id_estrutura INTEGER,
                        anexos TEXT NOT NULL,
                        FOREIGN KEY(id_estrutura) REFERENCES Estruturas(id_estrutura)
                    );

                    CREATE TABLE IF NOT EXISTS Anexos_PericInsa(
                        id_Anexos_PericInsa INTEGER PRIMARY KEY AUTOINCREMENT,
                        id_estrutura INTEGER,
                        anexos TEXT NOT NULL,
                        FOREIGN KEY(id_estrutura) REFERENCES Estruturas(id_estrutura)
                    );

                    CREATE TABLE IF NOT EXISTS Local_De_Trabalho(
                        id_local_de_trabalho INTEGER PRIMARY KEY AUTOINCREMENT,
                        local_Atividade TEXT NOT NULL,
                        parede TEXT NOT NULL,
                        piso TEXT NOT NULL,
                        iluminacao TEXT NOT NULL,
                        ventilacao TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Endereco(
                        id_endereco INTEGER PRIMARY KEY AUTOINCREMENT,
                        rua TEXT NOT NULL,
                        bairro TEXT NOT NULL,
                        numero TEXT NOT NULL,
                        cidade TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Laudo(
                        id_laudo INTEGER PRIMARY KEY AUTOINCREMENT,
                        numero_Vara TEXT NOT NULL,
                        numero_Pericia TEXT,
                        id_tipo INTEGER,
                        id_endereco INTEGER,
                        id_local_de_trabalho INTEGER,
                        FOREIGN KEY(id_tipo) REFERENCES Tipo_de_Laudo(id_tipo),
                        FOREIGN KEY(id_endereco) REFERENCES Endereco(id_endereco),
                        FOREIGN KEY(id_local_de_trabalho) REFERENCES Local_De_Trabalho(id_local_de_trabalho)
                    );

                    CREATE TABLE IF NOT EXISTS Reclamadas(
                        id_reclamadas INTEGER PRIMARY KEY AUTOINCREMENT,
                        nome TEXT NOT NULL,
                        id_laudo INTEGER,
                        FOREIGN KEY(id_laudo) REFERENCES Laudo(id_laudo)
                    );

                    CREATE TABLE IF NOT EXISTS Reclamantes(
                        id_reclamantes INTEGER PRIMARY KEY AUTOINCREMENT,
                        nome TEXT NOT NULL,
                        id_laudo INTEGER,
                        FOREIGN KEY(id_laudo) REFERENCES Laudo(id_laudo)
                    );
                ";

                cmd.ExecuteNonQuery();
            }
        }
    }
}