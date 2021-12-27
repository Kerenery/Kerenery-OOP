using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReportsApp.BLL.Interfaces;
using ReportsApp.DAL.Context;
using ReportsApp.DAL.Entities;

namespace ReportsApp.BLL.Authentication
{
    public class EmployeeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string SchemeName = "Employee";
        private readonly IEmployeeService _service;

        public EmployeeAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IEmployeeService service) : base(options,
            logger,
            encoder,
            clock)
        {
            _service = service;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = Context.GetEndpoint();

            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            if (!authHeader.Scheme.Equals(SchemeName, StringComparison.InvariantCultureIgnoreCase))
            {
                return AuthenticateResult.Fail("Invalid Authorization Header Scheme");
            }

            if (!Guid.TryParse(authHeader.Parameter, out var employeeId))
            {
                return AuthenticateResult.Fail("Invalid Authorization Header Parameter");
            }

            var employee = await _service.AsyncFindById(employeeId);

            if (employee == null)
            {
                return AuthenticateResult.Fail("Invalid Employee Identity");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Name, employee.FirstName),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}