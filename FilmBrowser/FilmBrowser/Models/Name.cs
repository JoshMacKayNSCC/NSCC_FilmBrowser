using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public partial class Name
    {
        public string? GetDeathYear 
        {
            get 
            {
                if (this.DeathYear == null) return "N/A";

                else return this.DeathYear.ToString();
            }
        }
    }
}
