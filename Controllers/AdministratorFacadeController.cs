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
    public class AdministratorFacadeController : ApiController
    {
        private AuthenticationDetails _authen;

        public AdministratorFacadeController()
        {
            _authen = new AuthenticationDetails();
        }

        //PostAirlineComapny: api/AdministratorFacade/CreateNewAirline
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/administratorFacade/CreateNewAirline")]
        [HttpPost]
        public IHttpActionResult CreateNewAirline([FromBody] AirlineCompany airline)
        {
            GetAdminTokenAndFacade();
            if (airline == null)
                return Content(HttpStatusCode.NotAcceptable, "You didn't send airline to post");
            try
            {
                _authen.adminFacade.CreateNewAirLine(_authen.admin, airline);
                return Ok($"{airline} Added by {_authen.admin.User.UserName}");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, $"{e.Message}");
            }

        }

        //UpdateAirlineCompany: api/AdministratorFacade/UpdateExistAirline
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/administratorFacade/UpdateExistAirline")]
        [HttpPut]
        public IHttpActionResult UpdateAirlineDetails([FromBody]AirlineCompany airline)
        {
            GetAdminTokenAndFacade();
            if (airline == null || airline.Id == 0)
                return Content(HttpStatusCode.NotAcceptable, $"{airline} details have not been completed properly");
            try
            {
                _authen.adminFacade.UpdateAirlineDetails(_authen.admin, airline);
                return Ok($"{airline} updated by {_authen.admin.User.UserName}");
            }

            catch (Exception e)
            {
                return Content(HttpStatusCode.NotFound, $"ID {airline.Id} was not found");
            }

        }

        // DeleteAirlineCompany: api/AdministratorFacade/DeleteAirlineCompany
        [ResponseType(typeof(AirlineCompany))]
        [Route("api/administratorFacade/DeleteAirline/{id}")]
        [HttpDelete]
        public IHttpActionResult RemoveAirline(int id)
        {
            GetAdminTokenAndFacade();
            if (id <= 0)
                return Content(HttpStatusCode.NotAcceptable, "Id is not valid");
            AirlineCompany airline = _authen.adminFacade.GetAllAirlineCompanies().ToList().Find(a => a.Id == id);
            if (airline == null)
                return Content(HttpStatusCode.NotFound, $"{id} was not found");
            _authen.adminFacade.RemoveAirline(_authen.admin, airline);
            return Ok($"{airline} deleted by {_authen.admin.User.UserName}");

        }

        // PostCustomer: api/AdministratorFacade/CreateCustomer
        [ResponseType(typeof(Customer))]
        [Route("api/administratorFacade/CreateNewCustomer")]
        [HttpPost]
        public IHttpActionResult CreateNewCustomer([FromBody] Customer customer)
        {
            GetAdminTokenAndFacade();
            if (customer == null)
                return Content(HttpStatusCode.NotAcceptable, "You didn't send customer to post");
            try
            {
                _authen.adminFacade.CreateNewCustomer(_authen.admin, customer);
                return Ok($"{customer} Added by {_authen.admin.User.UserName}");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, $"{e.Message}");
            }


        }

        // UpdateCustomer: api/AdministratorFacade/UpdateExistACustomer
        [ResponseType(typeof(Customer))]
        [Route("api/administratorFacade/UpdateExistCustomer")]
        [HttpPut]
        public IHttpActionResult UpdateCustomerDetails([FromBody]Customer customer)
        {
            GetAdminTokenAndFacade();
            if (customer == null || customer.Id <= 0)
                return Content(HttpStatusCode.NotAcceptable, $"{customer} details have not been completed properly");
            try
            {
                _authen.adminFacade.UpdateCustomerDetails(_authen.admin, customer);
                return Ok($"{customer} updated by {_authen.admin.User.UserName}");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotFound, $"ID {customer.Id} was not found");
            }

        }

        // DeleteCustomer: api/AdministratorFacade/DeleteCustomer
        [ResponseType(typeof(Customer))]
        [Route("api/administratorFacade/DeleteCustomer/{id}")]
        [HttpDelete]
        public IHttpActionResult RemoveCustomer(int id)
        {
            GetAdminTokenAndFacade();
            if (id <= 0)
                return Content(HttpStatusCode.NotAcceptable, "Id is not valid");
            Customer customer = _authen.adminFacade.GetAllCustomers(_authen.admin).ToList().Find(c => c.Id == id);
            if (customer == null)
                return Content(HttpStatusCode.NotFound, $"{id} was not found");
            _authen.adminFacade.RemoveCustomer(_authen.admin, customer);
            return Ok($"{customer} Deleted by {_authen.admin.User.UserName}");
        }
        private AuthenticationDetails GetAdminTokenAndFacade()
        {
            Request.Properties.TryGetValue("AdminUser", out object token);
            Request.Properties.TryGetValue("AdminFacade", out object facade);
            LoginToken<Administrator> adminToken = (LoginToken<Administrator>)token;
            LoggedInAdministratorFacade adminFacade = (LoggedInAdministratorFacade)facade;
            _authen.admin = adminToken;
            _authen.adminFacade = adminFacade;
            return _authen;
        }

    }
}
