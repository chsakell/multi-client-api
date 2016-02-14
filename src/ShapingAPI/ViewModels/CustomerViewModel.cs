using System;
using System.Collections.Generic;

namespace ShapingAPI.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel() { }

        public int CustomerId { get; set; }
        public string Company { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? SupportRepId { get; set; }
        public int TotalInvoices { get; set; }
        public ICollection<InvoiceViewModel> Invoice { get; set; }
        public AddressViewModel Address { get; set; }
        public ContactViewModel Contact { get; set; }
    }
}
