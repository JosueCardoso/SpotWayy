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
        public void Insert(Music music)
        {
            var strQuery = "";
            strQuery += "INSERT INTO TBMUSIC(Title,Genre,Duration,Id_Album) ";
            strQuery += string.Format("VALUES ('{0}','{1}','{2}',{3})",
                music.Title,music.Genre,music.Duration,music.IdAlbum);

            using(connection = new Connection()){
                connection.ExecuteQry(strQuery);
            }
        }

        //Método de alteração
        public void Update(Music music)
        {
            var strQuery = "";
            strQuery += "UPDATE TBMUSIC SET ";
            strQuery += string.Format("Title='{0}',",music.Title);
            strQuery += string.Format("Genre='{0}',",music.Genre);
            strQuery += string.Format("Duration='{0}' ", music.Duration);
            strQuery += string.Format(" WHERE Id_Music={0}", music.IdMusic);

            using(connection = new Connection()){
                connection.ExecuteQry(strQuery);
            }           
        }

        //Método de exclusão
        public void Delete(int id)
        {
            using (connection = new Connection())
            {
                var strQuery = "";
                strQuery += "DELETE FROM TBMUSIC ";
                strQuery += string.Format("WHERE Id_Music={0}", id);
                connection.ExecuteQry(strQuery);
            }
        }

        //Selecionar todos os registros
        public List<Music> GetAllMusic()
        {
            var listMusic = new List<Music>();

            using (connection = new Connection())
            {
                var strQuery = "SELECT * FROM TBMUSIC";
                SqlDataReader reader = connection.ExecuteSelect(strQuery);

                while (reader.Read())
                {
                    var music = new Music
                    {
                        IdMusic = int.Parse(reader["Id_Music"].ToString()),
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
        public Music GetForId(int id)
        {
            using (connection = new Connection())
            {
                var music = new Music();
                var strQuery = string.Format("SELECT * FROM TBMUSIC WHERE Id_Music={0}",id);
                SqlDataReader reader = connection.ExecuteSelect(strQuery);

                while (reader.Read())
                {
                    music.IdMusic = int.Parse(reader["Id_Music"].ToString());
                    music.Title = reader["Title"].ToString();
                    music.Genre = reader["Genre"].ToString();
                    music.Duration = reader["Duration"].ToString();
                    music.IdAlbum = int.Parse(reader["Id_Album"].ToString());
                }

                return music;
            }
        }


        public List<Music> GetForAlbum(int id)
        {
            var listMusic = new List<Music>();

            var strQuery = "";
            strQuery += string.Format("SELECT * FROM TBMUSIC WHERE Id_Album={0}",id);

            using (connection = new Connection())
            {
                SqlDataReader reader = connection.ExecuteSelect(strQuery);

                while (reader.Read())
                {
                    var music = new Music
                    {
                        IdMusic = int.Parse(reader["Id_Music"].ToString()),
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
