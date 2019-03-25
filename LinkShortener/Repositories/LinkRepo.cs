using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LinkShortener.Entities;

namespace LinkShortener.Repositories
{
    public class LinkRepo
    {
       string ConnectionString = $"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog = ShortLink; Data Source = LENOVO - PC";             

        public string CreateShortLink()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[5];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        public void convertInShortURL(string link)
        {            
            string ShortLink = CreateShortLink();

            string sqlCommand =
                $"INSERT INTO [dbo].[Link2] (ShortLink, FullLink, CountOpen, ExpirationDate) " +
                $"VALUES (@shortLink, @fullLink, @countOpen, @expirationDate)";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, sqlConnection))
                {
                    SqlParameter shortLink = new SqlParameter("@shortLink", ShortLink);
                    SqlParameter fullLink = new SqlParameter("@fullLink", link);
                    SqlParameter countOpen = new SqlParameter("@countOpen", 5);
                    SqlParameter expirationDate = new SqlParameter("@expirationDate", DateTime.Now);


                    command.Parameters.Add(shortLink);
                    command.Parameters.Add(fullLink);
                    command.Parameters.Add(countOpen);

                    command.ExecuteNonQuery();
                }
            }         
        }

        public string getFullUrl(string ShortUrl)
        {
            LinkDto Link = new LinkDto();
            string sql = $"SELECT * FROM Link2 WHERE ShortLink = {ShortUrl}";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Link.FullLink = reader["FullLink"].ToString();
                        }
                    }
                    else throw new Exception("No data found!");
                }
            }

            return Link.FullLink;
        }

    }
}
