using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEntityID
    {
        string Id { get; set; }
        string FullName { get; set; }
    }
}
