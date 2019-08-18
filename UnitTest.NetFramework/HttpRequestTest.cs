using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Autin.Services;

namespace UnitTest.NetFramework
{
    [TestClass]
    public class HttpRequestTest
    {
        [TestMethod]
        public async void PostRequestTestAsync()
        {
            var url = "http://59.110.173.242/api/login";
            var request = new Autin.Services.HttpRequest();
            await request.PostRequest(url, new Dictionary<string, string>() { {"email", "wldevau@gmail.com" }, { "password", "Mba287xd!" } });
        }
    }
}
