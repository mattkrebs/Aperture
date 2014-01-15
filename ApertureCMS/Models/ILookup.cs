using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureCMS.Models
{
    public interface ILookup
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
