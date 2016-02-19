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
    public class CustomersController : Controller
    {
        #region Properties
        private readonly ICustomerRepository _customerRepository;
        private List<string> _properties = new List<string>();
        private const int maxSize = 50;
        #endregion

        #region Constructor
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            _properties = new List<string>();
        }
        #endregion

        #region Actions
        public ActionResult Get(string props = null, int page = 1, int pageSize = maxSize)
        {
            try
            {
                var _customers = _customerRepository.LoadAll().Skip((page - 1) * pageSize).Take(pageSize);

                var _customersVM = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(_customers);

                JToken _jtoken = TokenService.CreateJToken(_customersVM, props);

                return Ok(_jtoken);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500);
            }
        }

        [Route("{customerId}")]
        public ActionResult Get(int customerId, string props = null)
        {
            try
            {
                var _customer = _customerRepository.Load(customerId);

                if (_customer == null)
                {
                    return HttpNotFound();
                }

                var _customerVM = Mapper.Map<Customer, CustomerViewModel>(_customer);

                JToken _jtoken = TokenService.CreateJToken(_customerVM, props);

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
