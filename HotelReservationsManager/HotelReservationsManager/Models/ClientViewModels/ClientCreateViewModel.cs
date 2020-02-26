namespace HotelReservationsManager.Models.ClientViewModels
{
    public class ClientCreateViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsAdult { get; set; }
    }
}
