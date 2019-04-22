using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintWayy.SpotWayy.Entities
{
   public class MusicVO
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Genre { get; set; }
       
        public string Duration { get; set; }
        public int IdAlbum { get; set; }
    }
}
