using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace StoreCenter.E2ETests
{
    [TestFixture]
    public class CorsE2ETests : PageTest
    {
        private const string BackendUrl = "https://localhost:5001";
        private const string FrontendUrl = "http://localhost:3000";

        [Test]
        public async Task CorsPolicy_ShouldAllowFrontendApiCalls()
        {
            // This test would require a running frontend application
            // For now, we'll test direct API calls with CORS headers

            // Navigate to a frontend origin
            await Page.GotoAsync(FrontendUrl);

            // Test if API call succeeds from frontend origin
            var response = await Page.EvaluateAsync<bool>($@"
                fetch('{BackendUrl}/api/categories', {{
                    method: 'GET',
                    headers: {{
                        'Origin': '{FrontendUrl}'
                    }}
                }})
                .then(response => response.ok)
                .catch(error => false)
            ");

            Assert.IsTrue(response, "Frontend should be able to call backend API");
        }

        [Test]
        public async Task CorsPolicy_ShouldBlockUnauthorizedOrigins()
        {
            // Navigate to an unauthorized origin
            await Page.GotoAsync("http://unauthorized-site.com");

            // Test that API call is blocked from unauthorized origin
            var response = await Page.EvaluateAsync<bool>($@"
                fetch('{BackendUrl}/api/categories', {{
                    method: 'GET',
                    headers: {{
                        'Origin': 'http://unauthorized-site.com'
                    }}
                }})
                .then(response => response.ok)
                .catch(error => false)
            ");

            Assert.IsFalse(response, "Unauthorized origins should be blocked");
        }
    }
}