namespace CRUDAPP.Model
{
    public class Expenses
    {
        public int ID { get; set; }
        public string ExpenseName { get; set; }
        public decimal Amount { get; set;}
        public DateTime ExpenseDate { get; set;}

        public int? groupId { get; set; }
        public int? festivalId { get; set; }
        public int fundForYear { get; set; }
    }
}
