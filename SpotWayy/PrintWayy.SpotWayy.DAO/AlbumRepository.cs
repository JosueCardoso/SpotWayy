using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintWayy.SpotWayy.Entities;
using System.Data.SqlClient;

namespace PrintWayy.SpotWayy.DAO
{
    public class AlbumRepository
    {
        private Connection connection;

        //Método de Inserção
        public void Insert(Album album)
        {
            var strQuery = "";
            strQuery += "INSERT INTO TBALBUM(Title,Id_Image)";
            strQuery += string.Format(" VALUES ('{0}','{1}')",
                album.Title,album.IdImage);

            //Using para encerrar a conexão assim que executar a query
            using (connection = new Connection())
            {
                connection.ExecuteQry(strQuery);
            }
        }

        //Método de Alteração
        public void Update(Album album)
        {
            var strQuery = "";
            strQuery += "UPDATE TBALBUM SET ";
            strQuery += string.Format("Title='{0}',",album.Title);
            strQuery += string.Format("Id_Image='{0}' ", album.IdImage);
            strQuery += string.Format(" WHERE Id_Album={0}",album.IdAlbum);

            using (connection = new Connection())
            {
                connection.ExecuteQry(strQuery);
            }
        }

        //Método de exclusão
        public void Delete(int id)
        {
            using (connection = new Connection())
            {
                var strQuery = "";
                strQuery += "DELETE FROM TBALBUM ";
                strQuery += string.Format(" WHERE Id_Album={0}", id);
                connection.ExecuteQry(strQuery);
            }
            
        }

        //Selecionar todos os registros
        public List<Album> GetAllAlbum()
        {
            var listAlbum = new List<Album>();

            using (connection = new Connection())
            {
                var strQuery = "SELECT * FROM TBALBUM";
                SqlDataReader reader = connection.ExecuteSelect(strQuery);

                while (reader.Read())
                {
                    var album = new Album
                    {
                        IdAlbum=int.Parse(reader["Id_Album"].ToString()),
                        Title = reader["Title"].ToString(),
                        IdImage = reader["Id_Image"].ToString()
                    };
                    listAlbum.Add(album);
                }
                reader.Close();
            }
            return listAlbum;
        }

        //Selecionar os registros apenas por id
        public Album GetForId(int id)
        {
            using (connection = new Connection())
            {
                var album = new Album();
                var strQuery = string.Format("SELECT * FROM TBALBUM WHERE Id_Album={0}", id);
                SqlDataReader reader = connection.ExecuteSelect(strQuery);

                while (reader.Read())
                {
                    album.IdAlbum = int.Parse(reader["Id_Album"].ToString());
                    album.Title = reader["Title"].ToString();
                    album.IdImage = reader["Id_Image"].ToString();
                }

                return album;
            }            
        }

        public int GetLastId()
        {
            int lastId = 0;

            var strQuery = "SELECT MAX(Id_Album) FROM TBALBUM";

            using (connection = new Connection())
            {
                SqlDataReader reader = connection.ExecuteSelect(strQuery);

                reader.Read();

                lastId = int.Parse(reader[""].ToString());
            }
            return lastId;
        }


    }
}
