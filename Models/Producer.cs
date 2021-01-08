using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manarca_Proiect.Models
{
    public class Producer
    {
        public int ID { get; set; }
        public string ProducerName { get; set; }
        public ICollection<Movie> Movies { get; set; } //navigation propriety
    }
}
