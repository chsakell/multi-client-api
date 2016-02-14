using System;
using System.Collections.Generic;

namespace ShapingAPI.ViewModels
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {
            InvoiceLine = new HashSet<InvoiceLineViewModel>();
        }

        public int InvoiceId { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public string BillingState { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Total { get; set; }
        public ICollection<InvoiceLineViewModel> InvoiceLine { get; set; }
    }
}
