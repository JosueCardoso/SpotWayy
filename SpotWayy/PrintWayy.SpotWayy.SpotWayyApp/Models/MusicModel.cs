using PrintWayy.SpotWayy.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PrintWayy.SpotWayy.SpotWayyApp.Models
{
    public class MusicModel    {
      
        public int IdMusic { get; set; }

        [DisplayName("Título")]
        public string Title { get; set; }

        [DisplayName("Gênero")]
        public string Genre { get; set; }

        [DisplayName("Duração")]
        public string Duration { get; set; }
        public int IdAlbum { get; set; }
        
    }
}