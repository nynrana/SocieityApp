using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDAPP.Model
{
    public class Fund
    {
        public int ID { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [Precision(18, 2)]
        public decimal PersonsForFood { get; set; } = 0;
        [Precision(18, 2)]
        public decimal FoodAmount { get; set; } = 0;
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; } = 0;
        public DateTime FundDate { get; set; }
        public int? givenTo { get; set; }
        public int? givenBy { get; set; }
        public string? givenToName { get; set; }
        public string? givenByName { get; set; }
        public string description { get; set; } = "";
        public int? groupId { get; set; }
        public int? festivalId { get; set; }
        public int fundForYear { get; set; }
        public virtual Member? FundGivenTo { get; set; }        
        public virtual Member? FundGivenBy { get; set; }

    }
}
