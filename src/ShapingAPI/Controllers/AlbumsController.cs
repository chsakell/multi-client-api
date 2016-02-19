using AutoMapper;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShapingAPI.Entities;
using ShapingAPI.Infrastructure.Core;
using ShapingAPI.Infrastructure.Data.Repositories;
using ShapingAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShapingAPI.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : Controller
    {
        #region Properties
        private readonly IAlbumRepository _albumRepository;
        private List<string> _properties = new List<string>();
        private Expression<Func<Album, object>>[] includeProperties;
        private const int maxSize = 50;
        #endregion

        #region Constructor
        public AlbumsController(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;

            _properties = new List<string>();
            includeProperties = Expressions.LoadAlbumNavigations();
        }
        #endregion

        #region Actions
        public ActionResult Get(string props = null, int page = 1, int pageSize = maxSize)
        {
            try
            {
                var _albums = _albumRepository.GetAll(includeProperties).Skip((page - 1) * pageSize).Take(pageSize);

                var _albumsVM = Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumViewModel>>(_albums);

                JToken _jtoken = TokenService.CreateJToken(_albumsVM, props);

                return Ok(_jtoken);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
        }

        [Route("{albumId}")]
        public ActionResult Get(int albumId, string props = null)
        {
            try
            {
                var _album = _albumRepository.Get(t => t.AlbumId == albumId, includeProperties);

                if (_album == null)
                {
                    return HttpNotFound();
                }

                var _albumVM = Mapper.Map<Album, AlbumViewModel>(_album);

                JToken _jtoken = TokenService.CreateJToken(_albumVM, props);

                return Ok(_jtoken);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
        }
        #endregion
    }
}
