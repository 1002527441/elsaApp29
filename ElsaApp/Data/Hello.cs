using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElsaApp.Data
{
    public class DTOHello
    {
        public string Id = Guid.NewGuid().ToString();
        public string Name { get; set; }
    }
}
