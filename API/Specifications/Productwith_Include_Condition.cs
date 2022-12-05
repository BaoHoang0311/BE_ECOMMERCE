using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Entites;
using API.Specifications;

namespace API.Specifications
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
