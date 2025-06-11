using Microsoft.EntityFrameworkCore;

namespace CRUDAPP.Model
{
    public class Maintenance
    {
        public int ID { get; set; }
        public int? memberId { get; set; }
        public virtual Member? MaintenanceGivenBy { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public int ModeOfPayment { get;set; }
        public DateTime createdDate { get; set; }
        public int? groupId { get; set; }

        public int fundForYear { get; set; }
        public int fundForMonth { get; set; }
    }
}
