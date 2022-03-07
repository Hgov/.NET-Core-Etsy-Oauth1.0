using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Concrete.Domain
{
    public class StateCode
    {
        public int Code { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
    }
}
