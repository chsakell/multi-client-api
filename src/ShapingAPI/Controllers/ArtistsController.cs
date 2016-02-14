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
using System.Threading.Tasks;

namespace ShapingAPI.Controllers
{
    [Route("api/[controller]")]
    public class ArtistsController : Controller
    {
        #region Properties
        private readonly IArtistRepository _artistRepository;
        private List<string> _properties = new List<string>();
        private const int maxSize = 50;
        #endregion

        #region Constructor
        public ArtistsController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;

            _properties = new List<string>();
        }
        #endregion

        #region Actions
        public ActionResult Get(string props = null, int page = 1, int pageSize = maxSize)
        {
            try
            {
                var _artists = _artistRepository.LoadAll().Skip(page).Take(pageSize);

                var _artistsVM = Mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistViewModel>>(_artists);

                string _serializedArtists = JsonConvert.SerializeObject(_artistsVM, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                JToken _jtoken = JToken.Parse(_serializedArtists);
                if (!string.IsNullOrEmpty(props))
                    Utils.FilterProperties(_jtoken, props.ToLower().Split(',').ToList());

                return Ok(_jtoken);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
        }

        [Route("{artistId}")]
        public ActionResult Get(int artistId, string props = null)
        {
            try
            {
                var _artist = _artistRepository.Load(artistId);

                if (_artist == null)
                {
                    return HttpNotFound();
                }

                var _artistVM = Mapper.Map<Artist, ArtistViewModel>(_artist);

                string _serializedArtist = JsonConvert.SerializeObject(_artistVM, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                JToken _jtoken = JToken.Parse(_serializedArtist);
                if (!string.IsNullOrEmpty(props))
                    Utils.FilterProperties(_jtoken, props.ToLower().Split(',').ToList());
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
