using ProjectManagmentSystem.BLL;
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
    [BasicAuthentication]
    public class CustomerController : ApiController
    {
        AuthenticationDetails _authen { get; set; }
        public CustomerController()
        {
            _authen = new AuthenticationDetails();
        }

        // GetAllMyFlights: api/Customer/AllMyFlights
        [ResponseType(typeof(List<Flight>))]
        [Route("api/customer/AllMyFLights")]
        [HttpGet]
        public IHttpActionResult GetAllMyFlights()
        {
            getCustomerLoginToken();
            List<Flight> myFLights = _authen.customerFacade.GetAllMyFlights(_authen.customer).ToList();
            if (myFLights.Count > 0)
                return Ok(myFLights);
            else
                return StatusCode(HttpStatusCode.NotFound);
        }

        // PurchaseTicket: api/Customer/PurchaseTicket
        [Route("api/customer/PurchaseTicket/{flightId}")]
        [HttpPost]
        public IHttpActionResult PurchaseTicket(int flightId)
        {
            getCustomerLoginToken();
            if (flightId > 0)
            {
                Flight flight = _authen.customerFacade.GetFlightById(flightId);
                if (flight != null)
                {
                    try
                    {
                        _authen.customerFacade.PurchaseTicket(_authen.customer, flight);
                        return Ok($"{_authen.customer.User.UserName} purchased ticket for this flight details:{flight}");
                    }

                    catch (Exception e)
                    {
                        return Content(HttpStatusCode.NotAcceptable, $"{e.Message}");
                    }

                }
            }
            return Content(HttpStatusCode.NotAcceptable, $"Id {flightId} is not valid");
        }

        // CancelTicket: api/Customer/CancelTicket
        [Route("api/customer/CancelTicket/{id}")]
        [HttpDelete]
        public IHttpActionResult CancelTicket(int id)
        {
            getCustomerLoginToken();
            if (id > 0)
            {
                Ticket ticket = _authen.customerFacade.GetAllMyTickets(_authen.customer, _authen.customer.User).ToList().Find(t => t.Id == id);
                if (ticket != null)
                {
                    _authen.customerFacade.CancelTicket(_authen.customer, ticket);
                    return Ok($"{ticket} caneles by {_authen.customer.User.UserName}");
                }
                else
                {
                    return NotFound();
                }
            }
            return Content(HttpStatusCode.NotAcceptable, $"{id} is not valid");


        }

        private AuthenticationDetails getCustomerLoginToken()
        {
            Request.Properties.TryGetValue("CustomerUser", out object token);
            Request.Properties.TryGetValue("CustomerFacade", out object facade);
            LoggedInCustomerFacade customerFacade = (LoggedInCustomerFacade)facade;
            LoginToken<Customer> customerToken = (LoginToken<Customer>)token;
            _authen.customerFacade = customerFacade;
            _authen.customer = customerToken;
            return _authen;
        }
    }
}
