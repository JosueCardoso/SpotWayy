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
            //lista de parametros para criar o SqlCommand para proteger os valores de sqlinjection
            List<SqlParameter> parameters = new List<SqlParameter>();
            
            var strQuery = @"INSERT INTO TBALBUM(Title,Id_Image) VALUES (@Title,@Id_Image)";
            var title = new SqlParameter("Title", album.Title);
            var id_Image = new SqlParameter("Id_Image", album.IdImage);

            parameters.Add(title);
            parameters.Add(id_Image);

            //Using para encerrar a conexão assim que executar a query
            using (connection = new Connection())
            {
                connection.ExecuteQry(strQuery,parameters);
            }
        }

        //Método de Alteração
        public void Update(Album album)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            var strQuery = @"UPDATE TBALBUM SET Title=@Title,Id_Image=@Id_Image WHERE Id_Album=@Id_Album";
            var title = new SqlParameter("Title", album.Title);
            var id_Image = new SqlParameter("Id_Image", album.IdImage);
            var id_Album = new SqlParameter("Id_Album",album.IdAlbum);

            parameters.Add(title);
            parameters.Add(id_Image);
            parameters.Add(id_Album);

            using (connection = new Connection())
            {
                connection.ExecuteQry(strQuery,parameters);
            }
        }

        //Método de exclusão
        public void Delete(int id)
        {
            using (connection = new Connection())
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                var strQuery = @"DELETE FROM TBALBUM WHERE Id_Album=@Id_Album";                
                var id_Album = new SqlParameter("Id_Album", id);

                parameters.Add(id_Album);

                connection.ExecuteQry(strQuery,parameters);
            }
            
        }

        //Selecionar todos os registros
        public List<Album> GetAllAlbum()
        {
            var listAlbum = new List<Album>();

            using (connection = new Connection())
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                var strQuery = @"SELECT * FROM TBALBUM";
                SqlDataReader reader = connection.ExecuteSelect(strQuery,parameters);

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
                List<SqlParameter> parameters = new List<SqlParameter>();

                var strQuery = @"SELECT * FROM TBALBUM WHERE Id_Album=@Id_Album";
                var idAlbum = new SqlParameter("Id_Album",id);

                parameters.Add(idAlbum);

                SqlDataReader reader = connection.ExecuteSelect(strQuery,parameters);

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

            List<SqlParameter> parameters = new List<SqlParameter>();
            var strQuery = @"SELECT MAX(Id_Album) FROM TBALBUM";

            using (connection = new Connection())
            {
                SqlDataReader reader = connection.ExecuteSelect(strQuery,parameters);

                reader.Read();

                lastId = int.Parse(reader[""].ToString());
            }
            return lastId;
        }


    }
}
