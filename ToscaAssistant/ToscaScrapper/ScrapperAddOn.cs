using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToscaScrapper.Tasks;
using Tricentis.TCAddOns;

namespace ToscaScrapper
{
    public class ScrapperAddOn : TCAddOn
    {
        public override string UniqueName => "Scrapper";

        public override Dictionary<Type, string> TaskToIconDefinition
        {
            get
            {
                var entry = new Dictionary<Type, string> { { typeof(ScrapperTask), "GUI.Images.engineering.png" } };
                return entry;
            }
        }
    }
}
