using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<GymClassViewModel> GymClasses { get; set; } = Enumerable.Empty<GymClassViewModel>();
        public bool ShowHistory { get; set; }
    }
}
