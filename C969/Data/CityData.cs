﻿using C969.Exceptions;
using C969.Models;
using C969.Utilities;
using MySqlX.XDevAPI.Relational;
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
            DataRow cityRow = RetrieveSingleRow(emptyCity, "cityId", id) ?? throw new DataNotFound("No city found with provided ID");
            City resultCity = DataTableConverter.ConvertDataRowToModel<City>(cityRow);
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
            DataRow cityRow = RetrieveSingleRow(emptyCity, "city", cityName) ?? throw new DataNotFound("No city found with provided name");
            City resultCity = DataTableConverter.ConvertDataRowToModel<City>(cityRow);
            return resultCity;
        }
        /// <summary>
        /// Determines whether a city is attached to any addresses.
        /// </summary>
        /// <param name="id">ID of the city</param>
        /// <returns>True if the city is attached to at least one address, otherwise false</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        /// <exception cref="DataNotFound">Thrown when city object is not found with provided id</exception>
        /// <exception cref="Exception"></exception>
        public bool CityAttachedToAddresses(int id)
        {
            bool cityBeingUsed = false;
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            try
            {
                var claimedCity = GetCityById(id);
                var addressDataAccess = new AddressData();
                var addressCityLookUp = addressDataAccess.GetAddressByCityId(id);
                if (addressCityLookUp.Count < 0)
                {
                    cityBeingUsed = true;
                }
            }
            catch (Exception ex)
            {
                if (ex is DataNotFound)
                {
                    throw ex;
                }
                else
                {
                    throw new Exception("Something went wrong checking if the city is attached to any addresses");
                }
            }
            return cityBeingUsed;
        }
        /// <summary>
        /// Checks if a city exists in the database based on id
        /// </summary>
        /// <param name="workingCity">The city instance to check</param>
        /// <returns>true if the city exists, false otherwise</returns>
        public bool DoesCityExist(City workingCity)
        {
            try
            {
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
        /// <returns>ID of newly added city</returns>
        /// <exception cref="DuplicateData">Thrown when the city already exists</exception>
        /// <exception cref="InvalidObject">Thrown when the city object is not valid</exception>
        public int AddCity(City workingCity)
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
        /// Retrieves a list of cities associated with a country id
        /// </summary>
        /// <param name="id">ID of the country</param>
        /// <returns>A list of cities associated with the country ID</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        public List<City> GetCityByCountryId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            var emptyCity = new City();
            var cityDataTable = RetrieveData(emptyCity, "countryId", id);
            return DataTableConverter.ConvertDataTableToList<City>(cityDataTable);
        }
        /// <summary>
        /// Method to check if a duplicate city exists in the database
        /// </summary>
        /// <param name="workingCity">The city to check for duplicates</param>
        /// <returns>CityId if duplicate exists, 0 otherwise</returns>
        public int DoesDuplicateExist(City workingCity)
        {
            try
            {
                City cityByNameSearch = GetCityByName(workingCity.city);
                return cityByNameSearch.cityId;
            }
            catch (DataNotFound)
            {
                return 0;
            }
        }
        /// <summary>
        /// Update customer in db
        /// </summary>
        /// <param name="workingCity">City object to update</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="DataNotFound">Thrown when the city cannot be found by it's ID</exception>
        /// <exception cref="InvalidObject">Thrown when the city object is not valid</exception>
        public bool UpdateCity(City workingCity)
        {
            bool validCity = ModelValidator.ValidateModel(workingCity);

            if (!validCity)
            {
                throw new InvalidObject("City isn't valid");
            }

            bool existingCity = DoesCityExist(workingCity);

            if (!existingCity)
            {
                throw new DataNotFound("City does not exist");
            }

            int duplicateCity = DoesDuplicateExist(workingCity);

            if (duplicateCity != 0 && workingCity.cityId != duplicateCity)
            {
                throw new DuplicateData($"City already exists by name - please use {duplicateCity}.", duplicateCity);
            }

            return UpdateData(workingCity, "cityId", workingCity.cityId);
        }
        /// <summary>
        /// Deletes a city from the database by id
        /// </summary>
        /// <param name="id">id of the city to delete</param>
        /// <returns>True if the city was successfully deleted, False otherwise</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        /// <exception cref="ChangeNotPermitted">Thrown when city is still attached to addresses (foreign key constraint)</exception>
        /// <exception cref="DataNotFound">Thrown when city object cannot be found by provided id</exception>
        public bool DeleteCityById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }

            bool cityBeingUsed = CityAttachedToAddresses(id);
            if (cityBeingUsed)
            {
                throw new ChangeNotPermitted("City attached to addresses");
            }

            City claimedCity = GetCityById(id);
            bool existingCity = DoesCityExist(claimedCity);

            if (!existingCity)
            {
                throw new DataNotFound("City does not exist");
            }
            return DeleteData<City>($"cityId = {id}");
        }
    }
}
