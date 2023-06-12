using C969.Exceptions;
using C969.Models;
using C969.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Data
{
    public class CountryData : Database
    {
        public bool AddCountry(Country workingCountry)
        {
            var validCountry = ModelValidator.ValidateModel(workingCountry);

            if (!validCountry)
            {
                throw new InvalidObject("Country isn't valid");
            }

            return AddData(workingCountry);
        }
        public bool UpdateCountry(Country workingCountry)
        {
            var validCountry = ModelValidator.ValidateModel(workingCountry);

            if (!validCountry)
            {
                throw new InvalidObject("Country isn't valid");
            }

            return UpdateData(workingCountry, "countryId", workingCountry.countryId);
        }
        public bool DeleteCountryById(int id)
        {
            return DeleteData<Country>($"countryId = ${id}");
        }
    }
}
