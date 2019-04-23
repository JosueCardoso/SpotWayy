using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintWayy.SpotWayy.DAO;
using PrintWayy.SpotWayy.Entities;

namespace PrintWayy.SpotWayy.Business
{
    public class AlbumBusiness
    {
        private AlbumRepository _albumRepository = new AlbumRepository();

        public void Insert(AlbumVO album)
        {
            _albumRepository.Insert(album);
        }

        public void Delete(int id)
        {
            _albumRepository.Delete(id);
        }

        public void Update(AlbumVO album)
        {
            _albumRepository.Update(album);
        }

        public AlbumVO GetForId(int id)
        {
            return _albumRepository.GetForId(id);
        }

        public List<AlbumVO> GetAllAlbum()
        {
            return _albumRepository.GetAllAlbum();
        }

        public int GetLastId()
        {
            return _albumRepository.GetLastId();
        }
    }
}
