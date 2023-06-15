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
            DataRow countryRow = RetrieveSingleRow(emptyCountry, "countryId", id);
            Country resultCountry = DataTableConverter.ConvertDataRowToModel<Country>(countryRow)
                ?? throw new DataNotFound("No  country found with the provided ID");
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
            DataRow countryRow = RetrieveSingleRow(emptyCountry, "country", countryName);
            Country resultCountry = DataTableConverter.ConvertDataRowToModel<Country>(countryRow)
                ?? throw new DataNotFound("No country found with provided name");
            return resultCountry;
        }
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
