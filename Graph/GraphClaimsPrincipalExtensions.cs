using Microsoft.Graph;
using System.Security.Claims;

namespace AltruCode.Graph;

public static class GraphClaimTypes
{
    public const string DisplayName = "graph_name";
    public const string GivenName = "graph_givenname";
    public const string Surname = "graph_surname";
    public const string Email = "graph_email";
    public const string Photo = "graph_photo";
}


public static class GraphClaimsPrincipalExtensions
{
    public static string? GetUserGraphDisplayName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(GraphClaimTypes.DisplayName);
    }

    public static string? GetUserGraphGivenName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(GraphClaimTypes.GivenName);
    }

    public static string? GetUserGraphSurname(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(GraphClaimTypes.Surname);
    }

    public static string? GetUserGraphEmail(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(GraphClaimTypes.Email);
    }

    public static string? GetUserGraphPhoto(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(GraphClaimTypes.Photo);
    }

    public static void AddUserGraphInfo(this ClaimsPrincipal claimsPrincipal, User user)
    {
        var identity = claimsPrincipal.Identity as ClaimsIdentity;
        if (identity == null) throw new Exception("bah");

        identity.AddClaim(
            new Claim(GraphClaimTypes.DisplayName, user.DisplayName));
        identity.AddClaim(
            new Claim(GraphClaimTypes.GivenName, user.GivenName));
        identity.AddClaim(
            new Claim(GraphClaimTypes.Surname, user.Surname));
        identity.AddClaim(
            new Claim(GraphClaimTypes.Email,
                user.Mail ?? user.UserPrincipalName));
    }
}