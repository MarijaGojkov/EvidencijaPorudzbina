using System.Data.SqlClient;

namespace EvidencijaPorudzbina.PristupBaziPodataka.Repozitorijumi
{
    public class RepozitorijumPorudzbina : IRepozitorijumPorudzbina
    {
        private const string _konekcioniString = @"Data Source = .\SQLEXPRESS;Initial Catalog=EvidencijaPorudzbina;Integrated Security=True";

        public Modeli.Porudzbina UzmiPorudzbinuPoId(int id)
        {
            Modeli.Porudzbina porudzbina = new Modeli.Porudzbina();


            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Porudzbina WHERE id = @id";
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            porudzbina.Id = (int)reader["id"];
                            porudzbina.Dostavljac = reader["dostavljac"] as string;
                            porudzbina.Proizvod = reader["proizvod"] as string;
                            porudzbina.Cena = (double)reader["cena"];
                            porudzbina.AdresaKupca = reader["adresaKupca"] as string;
                            porudzbina.TelefonKupca = reader["telefonKupca"] as string;
                            porudzbina.DatumPorucivanja = (DateTime)reader["datumPorucivanja"];
                        }
                    }
                }

            }
            return porudzbina;
        }

        public List<Modeli.Porudzbina> GetAllPorudzbine()
        {
            List<Modeli.Porudzbina> listaPorudzbina = new List<Modeli.Porudzbina>();

            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Porudzbina";

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaPorudzbina.Add(new Modeli.Porudzbina
                            {
                                Id = (int)reader["id"],
                                Dostavljac = reader["dostavljac"] as string,
                                Proizvod = reader["proizvod"] as string,
                                Cena = (double)reader["cena"],
                                AdresaKupca = reader["adresaKupca"] as string,
                                TelefonKupca = reader["telefonKupca"] as string,
                                DatumPorucivanja = (DateTime)reader["datumPorucivanja"]
                            });
                        }
                    }
                }
            }
            return listaPorudzbina;
        }

        public List<Modeli.Porudzbina> PretragaPorudzbina(string pretraga)
        {
            List<Modeli.Porudzbina> porudzbine = new List<Modeli.Porudzbina>();

            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Porudzbina WHERE Dostavljac LIKE '%' + @pretraga + '%'";
                    sqlCommand.Parameters.AddWithValue("@pretraga", pretraga);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            porudzbine.Add(new Modeli.Porudzbina
                            {
                                Id = (int)reader["id"],
                                Dostavljac = reader["dostavljac"] as string,
                                Proizvod = reader["proizvod"] as string,
                                Cena = (double)reader["cena"],
                                AdresaKupca = reader["adresaKupca"] as string,
                                TelefonKupca = reader["telefonKupca"] as string,
                                DatumPorucivanja = (DateTime)reader["datumPorucivanja"]
                            });
                        }
                    }
                }
            }

            return porudzbine;
        }

        public Modeli.Porudzbina Delete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "DELETE FROM Porudzbina WHERE Id = @id";
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlCommand.ExecuteNonQuery();

                    return sqlCommand.ExecuteScalar() as Modeli.Porudzbina;
                }
            }
        }

        public int Add(Modeli.Porudzbina porudzbina)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO Porudzbina(Id, Dostavljac, Proizvod, Cena, AdresaKupca, TelefonKupca, DatumPorucivanja) OUTPUT Inserted.Id " +
                        "VALUES(@Id, @Dostavljac, @Proizvod, @Cena, @AdresaKupca, @TelefonKupca, @DatumPorucivanja)"; ;
                    sqlCommand.Parameters.AddWithValue("@Id", porudzbina.Id);
                    sqlCommand.Parameters.AddWithValue("@Dostavljac", porudzbina.Dostavljac);
                    sqlCommand.Parameters.AddWithValue("@Proizvod", porudzbina.Proizvod);
                    sqlCommand.Parameters.AddWithValue("@Cena", porudzbina.Cena);
                    sqlCommand.Parameters.AddWithValue("@AdresaKupca", porudzbina.AdresaKupca);
                    sqlCommand.Parameters.AddWithValue("@TelefonKupca", porudzbina.TelefonKupca);
                    sqlCommand.Parameters.AddWithValue("@DatumPorucivanja", porudzbina.DatumPorucivanja);

                    return (int)sqlCommand.ExecuteScalar();
                }
            }
        }

        public int Update(Modeli.Porudzbina porudzbina)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "UPDATE Porudzbina SET Id=@Id, Dostavljac=@Dostavljac, Proizvod=@Proizvod, Cena=@Cena, AdresaKupca=@AdresaKupca, TelefonKupca=@TelefonKupca, DatumPorucivanja=@DatumPorucivanja,  OUTPUT Inserted.Id WHERE Id=@id"; ;
                    sqlCommand.Parameters.AddWithValue("@Id", porudzbina.Id);
                    sqlCommand.Parameters.AddWithValue("@Dostavljac", porudzbina.Dostavljac);
                    sqlCommand.Parameters.AddWithValue("@Proizvod", porudzbina.Proizvod);
                    sqlCommand.Parameters.AddWithValue("@Cena", porudzbina.Cena);
                    sqlCommand.Parameters.AddWithValue("@AdresaKupca", porudzbina.AdresaKupca);
                    sqlCommand.Parameters.AddWithValue("@TelefonKupca", porudzbina.TelefonKupca);
                    sqlCommand.Parameters.AddWithValue("@DatumPorucivanja", porudzbina.DatumPorucivanja);

                    return (int)sqlCommand.ExecuteScalar();
                }
            }
        }
    }
}
