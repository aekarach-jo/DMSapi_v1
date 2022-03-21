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
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public ActionResult<List<Payment>> GetAllPayment() => _paymentService.GetAllPayment();

        [HttpGet("{id}")]
        public ActionResult<Payment> GetPaymentById(string id)
        {
            var payment = _paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return payment;
        }
        [HttpGet("{paymentNumber}")]
        public ActionResult<Payment> GetPaymentByNumber(string paymentNumber)
        {
            var payment = _paymentService.GetPaymentByPaymentNumber(paymentNumber);
            if (payment == null)
            {
                return NotFound();
            }
            return payment;
        }

        [HttpGet("{paymentStatus}")]
        public  ActionResult<List<Payment>>  GetPaymentByStatus(string paymentStatus)
        {
            var payment = _paymentService.GetPaymentByStatus(paymentStatus);
            if (payment == null)
            {
                return NotFound();
            }
            return payment;
        }

        [HttpPost]
        public Payment CreatePayment([FromBody] Payment payment)
        {
            var data = _paymentService.GetAllPaymentForApi();
            var count = data.Count();
            var id = "P00" + count.ToString();
            var num = "P-" + count.ToString();
            payment.PaymentId = id;
            payment.PaymentNumber = num;
            payment.Status = "Open";
            _paymentService.CreatePayment(payment);
            return payment;
        }

        [HttpPut("{id}")]
        public IActionResult EditPayment([FromBody] Payment payment, string id)
        {
            var payments = _paymentService.GetPaymentById(id);
            if (payments == null)
            {
                return NotFound();
            }
            payment.PaymentId = id;
            _paymentService.EditPayment(id, payment);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeletePayment(string id)
        {
            var payments = _paymentService.GetPaymentById(id);
            var statusChange = payments.Status;
            if (payments == null)
            {
                return NotFound();
            }
            if (statusChange == "Open")
            {
                statusChange = "Close";
            }
            payments.Status = statusChange;
            _paymentService.DeletePayment(id, payments);
            return NoContent();
        }
    }
}