using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DMSapi_v2.Models;
using DMSapi_v2.Services;

namespace DMSapi_v2.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;
        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public ActionResult<List<Invoice>> GetAllInvoice() => _invoiceService.GetAllInvoice();

        [HttpGet("{id}")]
        public ActionResult<Invoice> GetInvoiceById(string id)
        {
            var invoice = _invoiceService.GetInvoiceById(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return invoice;
        }
        [HttpGet("{invoiceNumber}")]
        public ActionResult<Invoice> GetInvoiceByNumber(string invoiceNumber)
        {
            var invoice = _invoiceService.GetInvoiceByInvoiceNumber(invoiceNumber);
            if (invoice == null)
            {
                return NotFound();
            }
            return invoice;
        }

        [HttpGet("{invoiceStatus}")]
        public  ActionResult<List<Invoice>>  GetInvoiceByStatus(string invoiceStatus)
        {
            var invoice = _invoiceService.GetInvoiceByStatus(invoiceStatus);
            if (invoice == null)
            {
                return NotFound();
            }
            return invoice;
        }

        [HttpPost]
        public Invoice CreateInvoice([FromBody] Invoice invoice)
        {
            var data = _invoiceService.GetAllInvoiceForApi();
            var count = data.Count();
            var id = "I00" + count.ToString();
            invoice.InvoiceId = id;
            invoice.Status = "Open";
            _invoiceService.CreateInvoice(invoice);
            return invoice;
        }

        [HttpPut("{id}")]
        public IActionResult EditInvoice([FromBody] Invoice invoice, string id)
        {
            var invoices = _invoiceService.GetInvoiceById(id);
            if (invoices == null)
            {
                return NotFound();
            }
            invoice.InvoiceId = id;
            _invoiceService.EditInvoice(id, invoice);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeleteInvoice(string id)
        {
            var invoices = _invoiceService.GetInvoiceById(id);
            var statusChange = invoices.Status;
            if (invoices == null)
            {
                return NotFound();
            }
            if (statusChange == "Open")
            {
                statusChange = "Close";
            }
            invoices.Status = statusChange;
            _invoiceService.DeleteInvoice(id, invoices);
            return NoContent();
        }
    }
}