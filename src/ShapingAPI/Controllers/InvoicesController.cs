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
    public class InvoicesController : Controller
    {
        #region Properties
        private readonly IInvoiceRepository _invoiceRepository;
        private List<string> _properties = new List<string>();
        private const int maxSize = 50;
        #endregion

        #region Constructor
        public InvoicesController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;

            _properties = new List<string>();
        }
        #endregion

        #region Actions
        public ActionResult Get(string props = null, int page = 1, int pageSize = maxSize)
        {
            try
            {
                var _invoices = _invoiceRepository.LoadAll().Skip(page).Take(pageSize);

                var _invoicesVM = Mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceViewModel>>(_invoices);

                string _serializedInvoices = JsonConvert.SerializeObject(_invoicesVM, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                JToken _jtoken = JToken.Parse(_serializedInvoices);
                if (!string.IsNullOrEmpty(props))
                    Utils.FilterProperties(_jtoken, props.ToLower().Split(',').ToList());

                return Ok(_jtoken);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
        }

        [Route("{invoiceId}")]
        public ActionResult Get(int invoiceId, string props = null)
        {
            try
            {
                var _invoice = _invoiceRepository.Load(invoiceId);

                if (_invoice == null)
                {
                    return HttpNotFound();
                }

                var _invoiceVM = Mapper.Map<Invoice, InvoiceViewModel>(_invoice);

                string _serializedInvoice = JsonConvert.SerializeObject(_invoiceVM, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                JToken _jtoken = JToken.Parse(_serializedInvoice);
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
