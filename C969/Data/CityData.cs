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
    public class CityData : Database
    {
        public bool AddCity(City workingCity)
        {
            var validCity = ModelValidator.ValidateModel(workingCity);

            if (!validCity)
            {
                throw new InvalidObject("City isn't valid");
            }

            return AddData(workingCity);
        }
        public bool UpdateCity(City workingCity)
        {
            var validCity = ModelValidator.ValidateModel(workingCity);

            if (!validCity)
            {
                throw new InvalidObject("City isn't valid");
            }

            return UpdateData(workingCity, "cityId", workingCity.cityId);
        }
        public bool DeleteCityById(int id)
        {
            return DeleteData<City>($"cityId = {id}");
        }
    }
}
