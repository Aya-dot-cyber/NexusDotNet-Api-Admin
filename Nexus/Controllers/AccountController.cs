using Microsoft.AspNetCore.Mvc;
using Nexus.Application.Interfaces;
using Nexus.Common.Enumeration;
using Nexus.Common.Models;
using Nexus.Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace Nexus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IResponseModel _responseModel;
        private readonly IUserService _service;
        private readonly ITokenService _tokenService;
        private readonly IUserOTPService _userOtpService;


        public AccountController(IResponseModel responseModel, IUserService service,
            ITokenService tokenService, IUserOTPService userOtpService)
        {
            _responseModel = responseModel;
            _service = service;
            _tokenService = tokenService;
            _userOtpService = userOtpService;

        }

        /// <summary>
        /// HttpPost to Register new user 
        /// </summary>
        /// <remarks>
        /// sample RegisterDto{
        ///    "Name": "May",
        ///     "email": "userTest@example.com",
        ///     "password": "P@$$w0rd",
        ///     "phoneNumber": "01085514785",
        ///      "type": 2
        ///  }
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">retturn Message</response>
        /// <response code="400">server error</response>
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var item in ModelState.Values)
                    {
                        if (item.Errors.Select(a => a.ErrorMessage == "Invalid Email").FirstOrDefault())
                            return BadRequest(_responseModel.Response((int)EStatusCodes.BadRequest, "EmailChangeValidation", null));
                        else
                            return BadRequest(_responseModel.Response((int)EStatusCodes.BadRequest, "", null));
                    }

                }


                //if (await _service.IsValidPhoneToCountry(model.PhoneNumber, model.LoginCountryId.GetValueOrDefault()) == false)
                //    return BadRequest(_responseModel.Response((int)EStatusCodes.BadRequest, Helper.GetLocalization("PhoneNumberValidation", Request.GetLangIdFromHeader()), null));


                //bool EmailExist = await _service.IsEmailExist(model.Email);
                //bool PhoneNumberExist = await _service.IsPhoneExist(model.PhoneNumber);
                //if (EmailExist)
                //{
                //    return Conflict(_responseModel.Response((int)EStatusCodes.alreadyExist, Helper.GetLocalization("EmailAlreadyExist", Request.GetLangIdFromHeader()), null));

                //}

                //if (PhoneNumberExist)
                //{
                //    return Conflict(_responseModel.Response((int)EStatusCodes.alreadyExist, Helper.GetLocalization("PhoneNumberExist", Request.GetLangIdFromHeader()), null));

                //}
                var res = await _service.Register(model);
                if (!string.IsNullOrEmpty(res))
                {

                    return Ok(_responseModel.Response((int)EStatusCodes.Ok, string.Empty, new { Key = res }));
                }
                else
                    return BadRequest(_responseModel.Response((int)EStatusCodes.BadRequest, "UserRegisteredBefore", null));
            }
            catch (Exception ex)
            {
                return BadRequest(_responseModel.Response((int)EStatusCodes.BadRequest, ex.Message, null));
            }

        }








    }
}
