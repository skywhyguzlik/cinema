using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinema.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public string HallName { get; set; }
        public DateTime DateTime { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public decimal PricePaid { get; set; }
        public string Status { get; set; }
    }
}
