using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    public class NamefromTextBox
    {
        private string namedata;
        private string yearsdata;
        private string countrydata;
        public string NameData
        {
            get
            {
                return namedata;
            }
             set
            {
                namedata = value;
            }
        }
        public string YearsData
        {
            get
            {
                return yearsdata;
            }
            set
            {
                yearsdata = value;
            }
        }
        public string CountryData
        {
            get
            {
                return countrydata;
            }
            set
            {
                countrydata = value;
            }
        }
    }
}
