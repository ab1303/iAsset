using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iAsset.Services.Results;

namespace iAsset.Services.Results
{
    public class CountryCityResult:ServiceResult
    {
        public IEnumerable<string> Cities { get; set; }

    }
}
