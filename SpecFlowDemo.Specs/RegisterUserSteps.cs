using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpecFlowDemo.Controllers;
using TechTalk.SpecFlow;
using System.Web.Mvc;

namespace SpecFlowDemo.Specs
{
    [Binding]
    public class RegisterUserSteps
    {
        ActionResult _result;
        AccountController _controller; 
        
        [When(@"the user goes to the register user screen")]
        public void WhenTheUserGoesToTheRegisterUserScreen()
        {
            _controller = new AccountController();
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
    }
}
