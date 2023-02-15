using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Entities
{
    public class Response
    {
        public object Result { get; set; }
        public bool IsSucessed { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
