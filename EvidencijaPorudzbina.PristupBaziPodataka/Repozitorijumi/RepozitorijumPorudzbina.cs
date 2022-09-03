using EvidencijaPorudzbina.PristupBaziPodataka.Modeli;
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
                    sqlCommand.CommandText = "SELECT p.id, p.dostavljac, p.proizvod, p.cena, p.adresaKupca, p.telefonKupca, p.datumPorucivanja, s.stanje FROM Porudzbina p inner join StanjaPorudzbine s on p.idStanja = s.id WHERE id = @id";
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            porudzbina.Id = (int)reader["id"];
                            porudzbina.Dostavljac = reader["dostavljac"] as string;
                            porudzbina.Proizvod = reader["proizvod"] as string;
                            porudzbina.Cena = (decimal)reader["cena"];
                            porudzbina.AdresaKupca = reader["adresaKupca"] as string;
                            porudzbina.TelefonKupca = reader["telefonKupca"] as string;
                            porudzbina.DatumPorucivanja = (DateTime)reader["datumPorucivanja"];
                            porudzbina.StanjePorudzbine = reader["stanje"] as string;
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
                    sqlCommand.CommandText = "SELECT p.id, p.dostavljac, p.proizvod, p.cena, p.adresaKupca, p.telefonKupca, p.datumPorucivanja, s.stanje FROM Porudzbina p inner join StanjaPorudzbine s on p.idStanja = s.id";

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaPorudzbina.Add(new Modeli.Porudzbina
                            {
                                Id = (int)reader["id"],
                                Dostavljac = reader["dostavljac"] as string,
                                Proizvod = reader["proizvod"] as string,
                                Cena = (decimal)reader["cena"],
                                AdresaKupca = reader["adresaKupca"] as string,
                                TelefonKupca = reader["telefonKupca"] as string,
                                DatumPorucivanja = (DateTime)reader["datumPorucivanja"],
								StanjePorudzbine = reader["stanje"] as string
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
                    sqlCommand.CommandText = "SELECT p.id, p.dostavljac, p.proizvod, p.cena, p.adresaKupca, p.telefonKupca, p.datumPorucivanja, s.stanje FROM Porudzbina p inner join StanjaPorudzbine s on p.idStanja = s.id WHERE Dostavljac LIKE '%' + @pretraga + '%'";
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
                                Cena = (decimal)reader["cena"],
                                AdresaKupca = reader["adresaKupca"] as string,
                                TelefonKupca = reader["telefonKupca"] as string,
                                DatumPorucivanja = (DateTime)reader["datumPorucivanja"],
								StanjePorudzbine = reader["stanje"] as string
							});
                        }
                    }
                }
            }

            return porudzbine;
        }

        public void ObrisiPorudzbinuPoId(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "DELETE FROM Porudzbina WHERE Id = @id";
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.ExecuteScalar();
                }
            }
        }

        public int DodajPorudzbinu(Modeli.Porudzbina porudzbina)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO Porudzbina(Dostavljac, Proizvod, Cena, AdresaKupca, TelefonKupca, DatumPorucivanja, idStanja) OUTPUT Inserted.Id " +
                        "VALUES(@Dostavljac, @Proizvod, @Cena, @AdresaKupca, @TelefonKupca, @DatumPorucivanja, @idStanja)"; ;
                    sqlCommand.Parameters.AddWithValue("@Dostavljac", porudzbina.Dostavljac);
                    sqlCommand.Parameters.AddWithValue("@Proizvod", porudzbina.Proizvod);
                    sqlCommand.Parameters.AddWithValue("@Cena", porudzbina.Cena);
                    sqlCommand.Parameters.AddWithValue("@AdresaKupca", porudzbina.AdresaKupca);
                    sqlCommand.Parameters.AddWithValue("@TelefonKupca", porudzbina.TelefonKupca);
                    sqlCommand.Parameters.AddWithValue("@DatumPorucivanja", porudzbina.DatumPorucivanja);
                    sqlCommand.Parameters.AddWithValue("@idStanja", 1);

                    return (int)sqlCommand.ExecuteScalar();
                }
            }
        }

        public void IzmeniPorudzbinu(Modeli.Porudzbina porudzbina)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "UPDATE Porudzbina SET Id=@Id, Dostavljac=@Dostavljac, Proizvod=@Proizvod, Cena=@Cena, AdresaKupca=@AdresaKupca, TelefonKupca=@TelefonKupca,  OUTPUT Inserted.Id WHERE Id=@id"; ;
                    sqlCommand.Parameters.AddWithValue("@Id", porudzbina.Id);
                    sqlCommand.Parameters.AddWithValue("@Dostavljac", porudzbina.Dostavljac);
                    sqlCommand.Parameters.AddWithValue("@Proizvod", porudzbina.Proizvod);
                    sqlCommand.Parameters.AddWithValue("@Cena", porudzbina.Cena);
                    sqlCommand.Parameters.AddWithValue("@AdresaKupca", porudzbina.AdresaKupca);
                    sqlCommand.Parameters.AddWithValue("@TelefonKupca", porudzbina.TelefonKupca);

                    sqlCommand.ExecuteScalar();
                }
            }
        }

        public void IzmeniStanjePorudzbine(int id, int idStanja)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "UPDATE Porudzbina SET idStanja=@idStanja WHERE Id=@Id"; ;
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    sqlCommand.Parameters.AddWithValue("@idStanja", idStanja);

                    sqlCommand.ExecuteScalar();
                }
            }
        }

        public List<StanjePorudzbine> UzmiStanjaPorudzbine()
        {
			List<StanjePorudzbine> stanjaPorudzbine = new List<StanjePorudzbine>();

			using (SqlConnection sqlConnection = new SqlConnection(_konekcioniString))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = "SELECT * FROM StanjaPorudzbine";

					using (SqlDataReader reader = sqlCommand.ExecuteReader())
					{
						while (reader.Read())
						{
							stanjaPorudzbine.Add(new StanjePorudzbine
							{
								Id = (int)reader["id"],
								Stanje = reader["stanje"] as string,
							});
						}
					}
				}
			}
			return stanjaPorudzbine;
		}
    }
}
