using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace PickAndGo.ViewModels
{
    public class OrderTransactionVM
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string OrderDate { get; set; }
        public decimal OrderValue { get; set; }
        public string? Currency { get; set; }
        public string? PaymentType { get; set; }
        public string? PaymentId { get; set; }
        public string PaymentDate { get; set; }
        public int? OrderCount { get; set; }
        public decimal? OrderTotalVal { get; set; }
    }
}
