﻿namespace movementProj.Server.Interfaces
{
    public interface IAuthService
    {
        string Authenticate(string username, string password);
        bool ValidateToken(string token);
    }
}
