using BeestjeOpJeFeestje.Domain;

namespace BeestjeOpJeFeestje.ASP.Models
{
    public class AccessoiresSelectViewModel
    {
        public List<int> SelectedAccessoiresIds { get; set; }

        public List<Accessories>? AvailableAccessoires { get; set; }

        public BookingStatusModel BookingStatus { get; set; }
    }
}
