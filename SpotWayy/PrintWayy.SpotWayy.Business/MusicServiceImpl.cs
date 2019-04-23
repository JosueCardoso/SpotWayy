using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintWayy.SpotWayy.DAO;
using PrintWayy.SpotWayy.Entities;

namespace PrintWayy.SpotWayy.Business
{
    public class MusicServiceImpl
    {
        private MusicRepository _musicRepository = new MusicRepository();

        public void Insert(MusicVO music)
        {
            _musicRepository.Insert(music);
        }

        public void Delete(int id)
        {
            _musicRepository.Delete(id);
        }

        public void Update(MusicVO music)
        {
            _musicRepository.Update(music);
        }

        public MusicVO GetForId(int id)
        {
            return _musicRepository.GetForId(id);
        }

        public List<MusicVO> GetAllMusic()
        {
            return _musicRepository.GetAllMusic();
        }

        public List<MusicVO> GetForAlbum(int id)
        {
            return _musicRepository.GetForAlbum(id);
        }
    }
}
