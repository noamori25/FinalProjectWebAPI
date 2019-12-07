using ProjectManagmentSystem.Facade;
using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPI.Controllers
{
    public class AnonymousFacadeController : ApiController
    {
        private AnonymousUserFacade _anonymous;

        public AnonymousFacadeController()
        {
            _anonymous = new AnonymousUserFacade();
        }

        //GetAllFlights: api/AnonymousFacade/GetAllFligts
        [ResponseType(typeof(List<Flight>))]
        [Route("api/anonymousFacade/GetAllFlights")]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
            List<Flight> allFlights = _anonymous.GetAllFlights().ToList();
            if (allFlights.Count == 0)
                return NotFound();
            return Ok(allFlights);
        }

        //GetAllAirlineCompanies: api/AnonymousFacade/GetAllAirlineComapanies
        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/anonymousFacade/GetAllAirlineCompanies")]
        [HttpGet]
        public IHttpActionResult GetAllAirlineComapanies()
        {
            List<AirlineCompany> airlineCompanies = _anonymous.GetAllAirlineCompanies().ToList();
            if (airlineCompanies.Count == 0)
                return NotFound();
            return Ok(airlineCompanies);
        }

        //GetAllFlightsVacancy: api/AnonymousFacade/GetAllFlightsVacancy
        [ResponseType(typeof(List<Flight>))]
        [Route("api/anonymousFacade/GetAllFlightsVacancy")]
        [HttpGet]
        public IHttpActionResult GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flightsVacancy = _anonymous.GetAllVacancyFlights();
            if (flightsVacancy.Count == 0)
                return NotFound();
            return Ok(flightsVacancy.ToList());
        }

        //GetFlightsById: api/AnonymousFacade/GetFlightById
        [ResponseType(typeof(Flight))]
        [Route("api/anonymousFacade/GetFlightById/{id}")]
        [HttpGet]
        public IHttpActionResult GetFlightById(int id)
        {
            if (id <= 0)
                return StatusCode(HttpStatusCode.NoContent);
            Flight flight = _anonymous.GetFlightById(id);
            if (flight == null)
                return NotFound();
            return Ok(flight);
        }

        //GetFlightsByOriginCountry: api/AnonymousFacade/FlightsByOriginCountry
        [ResponseType(typeof(List<Flight>))]
        [Route("api/anonymousFacade/GetFlightsByOriginCountry/{countryCode}")]
        [HttpGet]
        public IHttpActionResult GetFlightsByOriginCountry(int countryCode)
        {
            List<Flight> flightsByOriginCountry = _anonymous.GetFlightsByOriginCountry(countryCode).ToList();
            if (flightsByOriginCountry.Count == 0)
                return NotFound();
            return Ok(flightsByOriginCountry);
        }

        //GetFlightsByDestinationCountry: api/AnonymousFacade/FlightsByDestinationCountry
        [ResponseType(typeof(List<Flight>))]
        [Route("api/anonymousFacade/GetFlightsByDestinationCountry/{countryCode}")]
        [HttpGet]
        public IHttpActionResult GetFlightsByDestinationCountry(int countryCode)
        {
            List<Flight> flightsByDestinationCountry = _anonymous.GetFlightsByDestinationCountry(countryCode).ToList();
            if (flightsByDestinationCountry.Count == 0)
                return NotFound();
            return Ok(flightsByDestinationCountry);
        }

        //Query parameters
        //GetFlightsByDepatrureDate: api/AnonymousFacade/FlightsByDepatrureDate?year=..&month=..&day=..
        [ResponseType(typeof(List<Flight>))]
        [Route("api/anonymousFacade/GetFlightsByDepartrureDate")]
        [HttpGet]
        public IHttpActionResult GetFlightsByDepartureDate(int year = 0, int month = 0, int day = 0)
        {
            DateTime departureDate = new DateTime(year, month, day);
            List<Flight> flightsByDepartureDate = _anonymous.GetFlightsByDepartureDate(departureDate).ToList();
            if (flightsByDepartureDate.Count == 0)
                return NotFound();
            return Ok(flightsByDepartureDate);
        }

        //Query parameters
        //GetFlightsByLandingDate: api/AnonymousFacade/FlightsByLandingDate
        [ResponseType(typeof(List<Flight>))]
        [Route("api/anonymousFacade/GetFligtsByLandingDate")]
        [HttpGet]
        public IHttpActionResult GetFlightsByLandingDate(int year = 0, int month = 0, int day = 0)
        {
            DateTime landingDate = new DateTime(year, month, day);
            List<Flight> flightsByLandingDate = _anonymous.GetFlightsByLandingDate(landingDate).ToList();
            if (flightsByLandingDate.Count == 0)
                return NotFound();
            return Ok(flightsByLandingDate);
        }



    }
}
