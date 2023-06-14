using C969.Exceptions;
using C969.Models;
using C969.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Data
{
    public class CityData : Database
    {
        /// <summary
        /// Gets a city from the db by id
        /// </summary>
        /// <param name="id">ID of the city</param>
        /// <returns>City if success, exception if fail</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        /// <exception cref="DataNotFound">Thrown when city object is not found with provided id</exception>
        public City GetCityById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            City emptyCity = new City();
            DataRow cityRow = RetrieveSingleRow(emptyCity, "cityId", id);
            City resultCity = DataTableConverter.ConvertDataRowToModel<City>(cityRow) 
                ?? throw new DataNotFound("No city found with provided ID");
            return resultCity;
        }
        /// <summary>
        /// Gets a city from the db by name
        /// </summary>
        /// <param name="cityname">Name of the city</param>
        /// <returns>City if success, exception if fail</returns>
        /// <exception cref="ArgumentException">Thrown when city name is null or whitespace</exception>
        /// <exception cref="DataNotFound">Thrown when city object is not found by provided name</exception>
        public City GetCityByName(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ArgumentException($"'{nameof(cityName)}' cannot be null or whitespace", nameof(cityName));
            }

            City emptyCity = new City();
            DataRow cityRow = RetrieveSingleRow(emptyCity, "city", cityName);
            City resultCity = DataTableConverter.ConvertDataRowToModel<City>(cityRow)
                ?? throw new DataNotFound("No city found with provided name");
            return resultCity;
        }
        /// <summary>
        /// Checks if a city exists in the database based on name and id
        /// </summary>
        /// <param name="workingCity">The city instance to check</param>
        /// <returns>true if the city exists, false otherwise</returns>
        public bool DoesCityExist(City workingCity)
        {
            try
            {
                City cityByNameSearch = GetCityByName(workingCity.city);
                City cityByIdSearch = GetCityById(workingCity.cityId);
                return true;
            }
            catch (DataNotFound)
            {
                return false;
            }
        }
        /// <summary>
        /// Add City to db
        /// </summary>
        /// <param name="workingCity">City object to add</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="DuplicateData">Thrown when the city already exists</exception>
        /// <exception cref="InvalidObject">Thrown when the city object is not valid</exception>
        public bool AddCity(City workingCity)
        {
            bool validCity = ModelValidator.ValidateModel(workingCity);

            if (!validCity)
            {
                throw new InvalidObject("City isn't valid");
            }

            bool existingCity = DoesCityExist(workingCity);

            if (existingCity)
            {
                throw new DuplicateData("City already exists");
            }

            return AddData(workingCity);
        }
        /// <summary>
        /// Update customer in db
        /// </summary>
        /// <param name="workingCity">City object to update</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="DuplicateData">Thrown when the city already exists</exception>
        /// <exception cref="InvalidObject">Thrown when the city object is not valid</exception>
        public bool UpdateCity(City workingCity)
        {
            bool validCity = ModelValidator.ValidateModel(workingCity);

            if (!validCity)
            {
                throw new InvalidObject("City isn't valid");
            }

            bool existingCity = DoesCityExist(workingCity);

            if (existingCity)
            {
                throw new DuplicateData("City already exists");
            }

            return UpdateData(workingCity, "cityId", workingCity.cityId);
        }
        // TODO: add checks for foreign keys like in AddressData
        public bool DeleteCityById(int id)
        {
            return DeleteData<City>($"cityId = {id}");
        }
    }
}
