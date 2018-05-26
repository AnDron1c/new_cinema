using System.Collections.Generic;

namespace viacinema.Models.ViewModels
{
    public class ReservationsViewModel
    {
        public List<Payment> Payments{ get; set; }

        public ReservationsViewModel(List<Payment> payments)
        {
            Payments = payments;
        }
    }
}
