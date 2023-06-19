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
    public class CountryData : Database
    {
        /// <summary>
        /// Gets a country from the db by id
        /// </summary>
        /// <param name="id">ID of the country</param>
        /// <returns>Country if success, exception if fail</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        /// <exception cref="DataNotFound">Thrown when country object is not found</exception>
        public Country GetCountryById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            Country emptyCountry = new Country();
            DataRow countryRow = RetrieveSingleRow(emptyCountry, "countryId", id) ?? throw new DataNotFound("No  country found with the provided ID");
            Country resultCountry = DataTableConverter.ConvertDataRowToModel<Country>(countryRow);
            return resultCountry;
        }
        /// <summary>
        /// Gets an country from the db by name
        /// </summary>
        /// <param name="countryName">Name of the Address (Address.address)</param>
        /// <exception cref="ArgumentException">Thrown when country name is null or whitespace</exception>
        /// <exception cref="DataNotFound">Thrown when country object is not found</exception>
        public Country GetCountryByName(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
            {
                throw new ArgumentException($"'{nameof(countryName)}' cannot be null or whitespace", nameof(countryName));
            }
            Country emptyCountry = new Country();
            DataRow countryRow = RetrieveSingleRow(emptyCountry, "country", countryName) ?? throw new DataNotFound("No country found with provided name");
            Country resultCountry = DataTableConverter.ConvertDataRowToModel<Country>(countryRow);
            return resultCountry;
        }
        /// <summary>
        /// Determines whether a country is attached to any cities
        /// </summary>
        /// <param name="id">ID of the country</param>
        /// <returns>True if the country is attached to at least one city, otherwise false</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        /// <exception cref="DataNotFound">Thrown when country object is not found with provided id</exception>
        public bool CountryBeingUsedByCity(int id)
        {
            bool countryBeingUsed = false;
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            try
            {
                var claimedCountry = GetCountryById(id);
                var cityDataAccess = new CityData();
                var cityCountryLookup = cityDataAccess.GetCityByCountryId(id);
                if (cityCountryLookup.Count < 0)
                {
                    countryBeingUsed = true;
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
                    throw new Exception("Something went wrong checking if the country is attached to any cities");
                }
            }
            return countryBeingUsed;
        }
        /// <summary>
        /// Checks if a country exists in the database based on name and id
        /// </summary>
        /// <param name="workingCountry">The country instance to check</param>
        /// <returns>true if the country exists, false otherwise</returns>
        public bool DoesCountryExist(Country workingCountry)
        {
            try
            {
                Country countryByNameSearch = GetCountryByName(workingCountry.country);
                Country countryByIdSearch = GetCountryById(workingCountry.countryId);
                return true;
            } 
            catch (DataNotFound)
            {
                return false;
            }
        }
        /// <summary>
        /// Add country to db
        /// </summary>
        /// <param name="workingAddress">Country object to add</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="DuplicateData">Thrown when the country already exists</exception>
        /// <exception cref="InvalidObject">Thrown when the country object is not valid</exception>
        public bool AddCountry(Country workingCountry)
        {
            var validCountry = ModelValidator.ValidateModel(workingCountry);

            if (!validCountry)
            {
                throw new InvalidObject("Country isn't valid");
            }

            bool existingCountry = DoesCountryExist(workingCountry);

            if (existingCountry)
            {
                throw new DuplicateData("Country already exists");
            }

            return AddData(workingCountry);
        }
        /// <summary>
        /// Update country in db
        /// </summary>
        /// <param name="workingCountry">Country object to update</param>
        /// <returns>Boolean of success</returns>
        /// <exception cref="DuplicateData">Thrown when the country already exists</exception>
        /// <exception cref="InvalidObject">Thrown when the country object is not valid</exception>
        public bool UpdateCountry(Country workingCountry)
        {
            var validCountry = ModelValidator.ValidateModel(workingCountry);

            if (!validCountry)
            {
                throw new InvalidObject("Country isn't valid");
            }

            bool existingCountry = DoesCountryExist(workingCountry);

            if (existingCountry)
            {
                throw new DuplicateData("Country already exists");
            }

            return UpdateData(workingCountry, "countryId", workingCountry.countryId);
        }
        /// <summary>
        /// Deletes a country from the database by id
        /// </summary>
        /// <param name="id">id of the country to delete</param>
        /// <returns>True if the coutnry was successfully deleted, False otherwise</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        /// <exception cref="ChangeNotPermitted">Thrown when country is still attached to cities (forein key constraint)</exception>
        /// <exception cref="DataNotFound">Thrown when country object cannot be found by provided id</exception>
        public bool DeleteCountryById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }

            bool countryBeingUsed = CountryBeingUsedByCity(id);
            if (countryBeingUsed)
            {
                throw new ChangeNotPermitted("Country attached to city");
            }

            Country claimedCountry = GetCountryById(id);
            bool existingCountry = DoesCountryExist(claimedCountry);

            if (!existingCountry)
            {
                throw new DataNotFound("Country does not exist");
            }

            return DeleteData<Country>($"countryId = ${id}");
        }
    }
}
