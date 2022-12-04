using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entites;

namespace Core.Specifications
{
    public class Productwith_Include_Condition : BaseSpecification<Product>
    {
        public Productwith_Include_Condition()
        {

        }

        public Productwith_Include_Condition(string id) : base(x => x.Id == id)
        {
            
        }
    }
}
