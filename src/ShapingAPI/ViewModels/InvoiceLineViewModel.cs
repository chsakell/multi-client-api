using System;
using System.Collections.Generic;

namespace ShapingAPI.ViewModels
{
    public class InvoiceLineViewModel
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public int Quantity { get; set; }
        public int TrackId { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
