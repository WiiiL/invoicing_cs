using System;

namespace Magni.APIClient.V2.Models.Interfaces
{
    public interface IAuthentication
    {
        string TokenField { get; set; }
        string Email { get; set; }
    }
}
