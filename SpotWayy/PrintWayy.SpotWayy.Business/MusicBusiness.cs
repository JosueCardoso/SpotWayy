using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintWayy.SpotWayy.DAO;
using PrintWayy.SpotWayy.Entities;

namespace PrintWayy.SpotWayy.Business
{
    public class MusicBusiness
    {
        private MusicRepository _musicRepository = new MusicRepository();

        public void Insert(Music music)
        {
            _musicRepository.Insert(music);
        }

        public void Delete(int id)
        {
            _musicRepository.Delete(id);
        }

        public void Update(Music music)
        {
            _musicRepository.Update(music);
        }

        public Music GetForId(int id)
        {
            return _musicRepository.GetForId(id);
        }

        public List<Music> GetAllMusic()
        {
            return _musicRepository.GetAllMusic();
        }

        public List<Music> GetForAlbum(int id)
        {
            return _musicRepository.GetForAlbum(id);
        }
    }
}
