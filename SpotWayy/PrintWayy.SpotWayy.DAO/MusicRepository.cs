using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintWayy.SpotWayy.Entities;
using System.Data.SqlClient;

namespace PrintWayy.SpotWayy.DAO
{
    public class MusicRepository
    {
        private Connection connection;

        //Método de inserção
        public void Insert(MusicVO music)
        {            
            List<SqlParameter> parameters = new List<SqlParameter>();

            var strQuery = @"INSERT INTO TBMUSIC(Title,Genre,Duration,Id_Album) VALUES(@Title,@Genre,@Duration,@Id_Album)";
            var title = new SqlParameter("Title",music.Title);
            var genre = new SqlParameter("Genre",music.Genre);
            var duration = new SqlParameter("Duration",music.Duration);
            var idAlbum = new SqlParameter("Id_Album",music.IdAlbum);

            parameters.Add(title);
            parameters.Add(genre);
            parameters.Add(duration);
            parameters.Add(idAlbum);

            using(connection = new Connection()){
                connection.ExecuteQry(strQuery,parameters);
            }
        }

        //Método de alteração
        public void Update(MusicVO music)
        {           
            List<SqlParameter> parameters = new List<SqlParameter>();

            var strQuery = @"UPDATE TBMUSIC SET Title=@Title,Genre=@Genre,Duration=@Duration WHERE Id_Music=@Id_Music";
            var title = new SqlParameter("Title", music.Title);
            var genre = new SqlParameter("Genre", music.Genre);
            var duration = new SqlParameter("Duration", music.Duration);            
            var idMusic = new SqlParameter("Id_Music",music.Id);

            parameters.Add(title);
            parameters.Add(genre);
            parameters.Add(duration);
            parameters.Add(idMusic);


            using(connection = new Connection()){
                connection.ExecuteQry(strQuery,parameters);
            }           
        }

        //Método de exclusão
        public void Delete(int id)
        {
            using (connection = new Connection())
            {                
                List<SqlParameter> parameters = new List<SqlParameter>();

                var strQuery = @"DELETE FROM TBMUSIC WHERE Id_Music=@Id_Music";
                var id_Music = new SqlParameter("Id_Music",id);

                parameters.Add(id_Music);                
                
                connection.ExecuteQry(strQuery,parameters);
            }
        }

        //Selecionar todos os registros
        public List<MusicVO> GetAllMusic()
        {
            var listMusic = new List<MusicVO>();

            using (connection = new Connection())
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                var strQuery = @"SELECT * FROM TBMUSIC";
                SqlDataReader reader = connection.ExecuteSelect(strQuery,parameters);

                while (reader.Read())
                {
                    var music = new MusicVO
                    {
                        Id = int.Parse(reader["Id_Music"].ToString()),
                        Title = reader["Title"].ToString(),
                        Genre = reader["Genre"].ToString(),
                        Duration = reader["Duration"].ToString(),
                        IdAlbum = int.Parse(reader["Id_Album"].ToString())
                    };
                    listMusic.Add(music);
                }
                reader.Close();
            }
            return listMusic;
        }

        //Selecionar apenas por id
        public MusicVO GetForId(int id)
        {
            using (connection = new Connection())
            {
                var music = new MusicVO();
                List<SqlParameter> parameters = new List<SqlParameter>();
               
                var strQuery = @"SELECT * FROM TBMUSIC WHERE Id_Music=@Id_Music";
                var idMusic = new SqlParameter("Id_Music",id);

                parameters.Add(idMusic);

                SqlDataReader reader = connection.ExecuteSelect(strQuery,parameters);

                while (reader.Read())
                {
                    music.Id = int.Parse(reader["Id_Music"].ToString());
                    music.Title = reader["Title"].ToString();
                    music.Genre = reader["Genre"].ToString();
                    music.Duration = reader["Duration"].ToString();
                    music.IdAlbum = int.Parse(reader["Id_Album"].ToString());
                }

                return music;
            }
        }


        public List<MusicVO> GetForAlbum(int id)
        {
            var listMusic = new List<MusicVO>();
                       
            List<SqlParameter> parameters = new List<SqlParameter>();

            var strQuery = @"SELECT * FROM TBMUSIC WHERE Id_Album=@Id_Album";
            var idAlbum = new SqlParameter("Id_Album",id);

            parameters.Add(idAlbum);

            using (connection = new Connection())
            {
                SqlDataReader reader = connection.ExecuteSelect(strQuery,parameters);

                while (reader.Read())
                {
                    var music = new MusicVO
                    {
                        Id = int.Parse(reader["Id_Music"].ToString()),
                        Title = reader["Title"].ToString(),
                        Genre = reader["Genre"].ToString(),
                        Duration = reader["Duration"].ToString(),
                        IdAlbum = int.Parse(reader["Id_Album"].ToString())
                    };
                    listMusic.Add(music);
                }
                reader.Close();
            }

            return listMusic;
        }
    }
}
