using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adimax.Domain
{
    public class CategoryName
    {
        private readonly Category _category;
        public CategoryName(Category category) 
        {
            _category = category;
        }
    }
}
