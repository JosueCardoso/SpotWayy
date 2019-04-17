using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrintWayy.SpotWayy.Business;
using PrintWayy.SpotWayy.SpotWayyApp.Models;
using PrintWayy.SpotWayy.Entities;
using AutoMapper;
using System.IO;



namespace PrintWayy.SpotWayy.SpotWayyApp.Controllers
{
    public class HomeController : Controller
    {
        //lista de musicas estatica(carregar no cache até salvar o album)
        private static List<MusicModel> listMusicAdd = new List<MusicModel>();
        private static List<MusicModel> listMusicUpdate = new List<MusicModel>();

        //lista de album estatica(carregar no cache durante a busca)
        private static List<AlbumModel> listAlbumCache = new List<AlbumModel>();               

        //business
        private AlbumBusiness albumBusiness = new AlbumBusiness();
        private MusicBusiness musicBusiness = new MusicBusiness();

        private static string nameImage;

        //*******Partial Views********//

        //Abre o PartialView de Inserção/Alteração e pega o id do álbum
        public ActionResult ShowInsertMusic(int id)
        {
            var musicModel = new MusicModel()
            {
                IdAlbum=id
            };
            return PartialView("_InsertMusic",musicModel);
        }        
        
        //Abre o PartialView de UpdateMusic
        public ActionResult ShowUpdateMusic(int idMusic, int idAlbum, string indicatorFlag)
        {
            TempData["IdMusicUpdate"] = idMusic;
            TempData["IndicatorFlag"] = indicatorFlag;

            var musicModel = new MusicModel();

            if (idAlbum == 0)
            {
                musicModel = listMusicAdd[idMusic - 1];
            }
            else
            {
                if (indicatorFlag.Equals("new"))
                {
                    musicModel = listMusicUpdate[idMusic - 1];
                }
                else
                {
                    var music = musicBusiness.GetForId(idMusic);
                    musicModel = Mapper.Map<Music, MusicModel>(music);
                }
            }              

            return PartialView("_UpdateMusic",musicModel);
        }

        //Abre o PartialView de exclusão e pega o id do álbum
        public ActionResult ShowDeleteMusic(int idMusic, int idAlbum, string indicatorFlag)
        {
            TempData["IdMusicDeleteMusic"] = idMusic;
            TempData["indicatorFlag"] = indicatorFlag;
            TempData["IdAlbumDeleteMusic"] = idAlbum;

            var musicModel = new MusicModel()
            {
                IdAlbum = idAlbum,
                IdMusic = idMusic,
            };
            
            return PartialView("_DeleteMusic",musicModel);
        }

        //Popula o PartialView com as listas de musicas de cache
        public ActionResult ShowMusic(int id)
        {
            List<MusicModel> listMusicView = ConcatenateList(id);           
            
            return PartialView("_Music", listMusicView);
        }

        //Mostrar todos os álbuns no index
        public ActionResult ShowAllAlbum()
        {
            //Buscando todos os albuns do banco e mapeando para AlbumModel
            var listAlbum = albumBusiness.GetAllAlbum();
            var listAlbumView = listAlbum.Select(Mapper.Map<Album, AlbumModel>);

            listAlbumCache.Clear();

            foreach (var item in listAlbumView)
            {
                listAlbumCache.Add(item);
            }

            //Limpando as listas de cache
            listMusicAdd.Clear();
            listMusicUpdate.Clear();

            return PartialView("_Album", listAlbumView);
        }

        //Abre o PartialView de excluir album
        public ActionResult ShowDeleteAlbum(int Id)
        {
            TempData["IdDeleteAlbum"] = Id;
            return PartialView("_DeleteAlbum");
        }



        //*******Actions********//

        // GET: Home
        public ActionResult Index()
        {  
            return View();
        }

        //Muda de página para insert
        public ActionResult Insert()
        {
            var album = new Album();
            var albumView = Mapper.Map<Album, AlbumModel>(album);
           
            return View(albumView);
        }

        //Action que insere no banco
        [HttpPost]
        public ActionResult InsertAlbum(AlbumModel albumModelView, HttpPostedFileBase file)
        {
            string fileName;

            //faz upload da imagem e gerencia o valor do input(que não pode ser recuperado por questões de segurança)
            //Verifica se há valor no input file
            if (file != null && file.ContentLength > 0)
            {
                fileName = string.Format(albumModelView.Title+"_"+file.FileName);

                //Se o nome da imagem ultrapassar de 30 digitos, é colocado apenas (titulo do album)_cover.(extensão) como nome da imagem
                if (fileName.Length > 30)
                {
                    fileName = string.Format("_cover"+file.FileName.Substring((file.FileName.Length-4),4));                    
                }
                string path = Path.Combine(Server.MapPath("~/Content/upload"), Path.GetFileName(fileName));
                file.SaveAs(path);
            }
            else
            {
                //caso não tenha valor mas o album existe no banco, utiliza o nome que está salvo no banco
                if (albumModelView.IdAlbum != 0)
                {
                    fileName = nameImage;
                }
                //senão atribui o nome padrão
                else
                {
                    fileName = "noImage.jpg";
                }
            }        
            
            var albumModel = new AlbumModel
            {
                Title = albumModelView.Title,
                IdImage = fileName,    
                IdAlbum = albumModelView.IdAlbum
            };
                     
            var album = Mapper.Map<AlbumModel, Album>(albumModel);

            //Objeto Hidden que pega o Id do album está causando ModelState Invalid
            ModelState.Remove("IdAlbum");
            if (ModelState.IsValid)
            {
                //Se idAlbum == 0 deve dar insert, senão update
                if (albumModelView.IdAlbum == 0)
                {
                    albumBusiness.Insert(album);
                    InsertMusicBD(listMusicAdd,album.IdAlbum);
                    listMusicAdd.Clear();
                }
                else
                {
                    albumBusiness.Update(album);
                    InsertMusicBD(listMusicUpdate,album.IdAlbum);
                    listMusicUpdate.Clear();
                }
            }
            else
            {
                return View("Insert", albumModelView);
            }   
            return RedirectToAction("Index");
        }

        //Muda de página para o insert(update)
        public ActionResult UpdateAlbum(int id)
        {
            var albumForId = albumBusiness.GetForId(id);

            nameImage = albumForId.IdImage;
            var albumForUpdate = Mapper.Map<Album,AlbumModel>(albumForId);            

            return View("Insert",albumForUpdate);
        }     

        //Confirma a exclusão do album no banco
        public ActionResult DeleteConfirmedAlbum()
        {
            var typedText = Request["Delete"].ToString().ToLower();
            int idDeleteAlbum = Convert.ToInt32(TempData["IdDeleteAlbum"]);

            //Verifica se foi digitado excluir no campo de texto e se o id do álbum é maior que 0(Se ele existe)
            if ("excluir".Equals(typedText) && (idDeleteAlbum > 0))
            {
                albumBusiness.Delete(idDeleteAlbum);
            }            
            
            return RedirectToAction("Index");
        }

        //Adiciona as musicas em listas de cache
        public ActionResult InsertMusic(MusicModel musicModel)
        {
            if (ModelState.IsValid)
            {
                if (musicModel.IdAlbum == 0)
                {
                    listMusicAdd.Add(musicModel);
                }
                else
                {
                    listMusicUpdate.Add(musicModel);
                }
            }

            return RedirectToAction("UpdateAlbum", new { id = musicModel.IdAlbum });
        }        

        //Confirma a exclusão da música
        public ActionResult DeleteConfirmedMusic(MusicModel musicView)
        {           
            int idMusicDeleteMusic = Convert.ToInt32(TempData["idMusicDeleteMusic"]);
            var typedText = Request["Delete"].ToString().ToLower();
            var idAlbumDeleteMusic = musicView.IdAlbum;
            var indicatorFlag = TempData["indicatorFlag"];

            //Verifica se foi digitado excluir no campo de texto
            if ("excluir".Equals(typedText))
            {
                //se o ID do álbum for 0, está excluindo a música de um novo álbum, senão a exclusão é durante a edição de um álbum já existente            
                if(idAlbumDeleteMusic==0){    
                    //remove a música da lista de cache de álbum novo
                    listMusicAdd.RemoveAt(idMusicDeleteMusic-1);
                }
                else
                {
                    //verifica se o flag de indicação da música é nova música(id da música é um contador) ou se é uma música do banco(idMusic)
                    if (indicatorFlag.Equals("new"))
                    {
                        //remove a música da lista de cache de um álbum antigo
                        listMusicUpdate.RemoveAt(idMusicDeleteMusic - 1);
                    }
                    else
                    {
                        //remove a música do banco
                        musicBusiness.Delete(idMusicDeleteMusic);
                    }
                    
                }                
            }
            
            return RedirectToAction("UpdateAlbum", new { id= idAlbumDeleteMusic});
        }        

        //Realiza o update da música nas listas cache ou no banco
        public ActionResult UpdateConfirmedMusic(MusicModel musicView)
        {
            int idMusicUpdate = Convert.ToInt32(TempData["IdMusicUpdate"]);
            var idAlbumUpdateMusic = musicView.IdAlbum;
            var indicatorFlag = TempData["IndicatorFlag"];
            

            if (idAlbumUpdateMusic == 0)
            {
                
                listMusicAdd.RemoveAt(idMusicUpdate - 1);
                listMusicAdd.Insert(idMusicUpdate - 1, musicView);
            }
            else
            {
                if (indicatorFlag.Equals("new"))
                {                    
                    listMusicUpdate.RemoveAt(idMusicUpdate - 1);
                    listMusicUpdate.Insert(idMusicUpdate - 1, musicView);
                }
                else
                {
                    var music = Mapper.Map<MusicModel, Music>(musicView);
                    music.IdMusic = idMusicUpdate;
                    musicBusiness.Update(music);                    
                }
            }

            return RedirectToAction("UpdateAlbum", new { id = idAlbumUpdateMusic });
        }


        //*******Métodos Auxiliares********//

        //Inserir as músicas que estão nas listas cache no banco
        private void InsertMusicBD(List<MusicModel> list,int idAlbum)
        { 
            if (idAlbum == 0)
            {
                //Busca o ultimo ID salvo
                idAlbum = albumBusiness.GetLastId();
            }

            //Percorre a lista de musica de cache para inserir no banco
            foreach (var item in list)
            {
                var music = new Music()
                {
                    IdMusic = item.IdMusic,
                    IdAlbum = idAlbum,
                    Title = item.Title,
                    Duration = item.Duration,
                    Genre = item.Genre
                };

                musicBusiness.Insert(music);
            }            
        }

        //Concatenar todas as listas de cache numa lista só
        private List<MusicModel> ConcatenateList(int id)
        {
            List<MusicModel> list = new List<MusicModel>();

            //Busca no banco músicas relacionadas com o id do album e mapeia para MusicModel
            var listMusicBD = new MusicBusiness().GetForAlbum(id);
            var listMusicModel = listMusicBD.Select(Mapper.Map<Music, MusicModel>);

            //Adiciona na lista da view as músicas em álbuns novos
            foreach (var item in listMusicAdd)
            {
                list.Add(item);
            }

            //Adiciona na lista da view as músicas que estavam no banco
            foreach (var item in listMusicModel)
            {
                list.Add(item);
            }

            //Adiciona na lista da view as músicas que estão na lista de cache de update
            foreach (var item in listMusicUpdate)
            {
                list.Add(item);
            }

            return list;
        }

        //Busca os álbuns no banco
        public ActionResult IndexSearch(string filter)
        {
            List<AlbumModel> teste = new List<AlbumModel>();
            var filterLower=filter.ToLower();
           
            teste=(listAlbumCache.FindAll(x => x.Title.ToLower().Contains(filterLower)));    

            return PartialView("_Album",teste);
        }
               
        
    }
}