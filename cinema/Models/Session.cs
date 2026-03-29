using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace cinema.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public string HallName { get; set; }
        public string HallQuality { get; set; }
        public int Capacity { get; set; }
        public int HallId { get; set; }
    }
}
