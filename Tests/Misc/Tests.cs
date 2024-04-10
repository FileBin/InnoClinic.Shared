using System.Linq;
using System.Net;
using System.Net.Http.Json;
using InnoClinic.Shared.Tests.Utility;
using InnoClinic.Shared.Tests.Utility.User;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace InnoClinic.Shared.Tests.Misc;

record ClaimDesc(string Type, string Value);

class Tests {
    TestWebApplicationFactory<TestWebApp.Program> _application;

    private readonly WebApplicationFactoryClientOptions DefaultOptions = new WebApplicationFactoryClientOptions() {
        AllowAutoRedirect = false
    };

    [SetUp]
    public void SetUp() {
        _application = new();
    }

    [Test]
    [CancelAfter(10000)]
    public async Task TestAuth() {
        var client = _application.CreateLoggedInClient<GeneralUser>(DefaultOptions);
        var resp = await client.GetAsync("/userinfo");
        Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var content = await resp.Content.ReadFromJsonAsync<IEnumerable<ClaimDesc>>();
        var claims = content?.ToDictionary(c => c.Type);
        Assert.Multiple(() => {
            Assert.That(claims!.TryGetValue("scope", out var desc));
            Assert.That(desc!.Value, Is.EqualTo("testAPI"));
        });

    }
}