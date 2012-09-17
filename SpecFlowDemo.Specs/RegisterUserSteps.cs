using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpecFlowDemo.Controllers;
using SpecFlowDemo.Models;
using TechTalk.SpecFlow;
using System.Web.Mvc;
using Moq;

namespace SpecFlowDemo.Specs
{
    [Binding]
    public class RegisterUserSteps
    {
        ActionResult _result;
        AccountController _controller;
        RegisterModel _registerModel;

        readonly Mock<IMembershipService> _memberService = new Mock<IMembershipService>();
        readonly Mock<IFormsAuthenticationService> _formsService = new Mock<IFormsAuthenticationService>();

        [When(@"the user goes to the register user screen")]
        public void WhenTheUserGoesToTheRegisterUserScreen()
        {
            _controller = new AccountController(_formsService.Object, _memberService.Object);
            _result = _controller.Register();
        }

        [Then(@"the register user view should be displayed")]
        public void ThenTheRegisterUserViewShouldBeDisplayed()
        {
            Assert.IsInstanceOf<ViewResult>(_result);
            Assert.IsEmpty(((ViewResult)_result).ViewName);
            Assert.AreEqual("Register",
                   _controller.ViewData["Title"],
                   "Page title is wrong");
        }

        [Given(@"The user has entered all the information")]
        public void GivenTheUserHasEnteredAllTheInformation()
        {
            _registerModel = new RegisterModel
            {
                UserName = "user" + new Random(1000).NextDouble().ToString(),
                Email = "test@dummy.com",
                Password = "test123",
                ConfirmPassword = "test123"
            };
            _controller = new AccountController(_formsService.Object, _memberService.Object);
        }

        [When(@"He Clicks on Register button")]
        public void WhenHeClicksOnRegisterButton()
        {
            _result = _controller.Register(_registerModel);
        }

        [Then(@"He should be redirected to the home page")]
        public void ThenHeShouldBeRedirectedToTheHomePage()
        {
            const string expected = "Index";
            Assert.IsNotNull(_result);
            Assert.IsInstanceOf<RedirectToRouteResult>(_result);

            var tresults = _result as RedirectToRouteResult;

            if (tresults != null) Assert.AreEqual(expected, tresults.RouteValues["action"]);
        }

    }
}
