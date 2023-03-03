namespace PickAndGo.ViewModels
{
    public class OrderHeaderVM
    {
        public int Outstanding { get; set; }
        public int Completed { get; set; }
        public decimal OutstandingVal { get; set; }
        public decimal CompletedVal { get; set; }
        public string Date { get; set; }
    }
}
