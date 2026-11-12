using System;
using LibraryApi.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace LibraryApi.Services.Interfaces;

public interface IAuthService
{
Task<IdentityResult> RegisterAsync(
          RegisterRequestDto request,
          CancellationToken cancellationToken
);
    

    
};
