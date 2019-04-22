using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PrintWayy.SpotWayy.SpotWayyApp.Models
{
    public class AlbumModel
    {
        public int Id { get; set; }

        [DisplayName("Título do álbum:")]
        public string Title { get; set; }

        [DisplayName("Url da imagem:")]
        public string IdImage { get; set; }
    }
}