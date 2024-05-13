namespace BeestjeOpJeFeestje.ASP.Models
{
    public class BevestigingViewModel
    {
        public decimal TotalPrice { get; set; }

        public List<DiscountInfo> Discounts { get; set; }

        public BookingStatusModel BookingStatus { get; set; }
    }
}
