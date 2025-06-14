using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace CRUDAPP.Model
{
    public class MaintenanceExpenses
    {
        public int ID { get; set; }        
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        [DisplayName("Exp. Date")]
        public DateTime MaintenanceExpDate { get; set; }
        [DisplayName("Mode Of Payment")]
        public int ModeOfPayment { get;set; }
        [DisplayName("Created Date")]
        public DateTime createdDate { get; set; }
        public int? groupId { get; set; }        
    }
}
