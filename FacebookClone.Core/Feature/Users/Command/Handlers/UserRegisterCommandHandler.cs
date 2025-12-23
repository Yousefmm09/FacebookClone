using FacebookClone.Core.Feature.Authentication.Command.Models;
using FacebookClone.Core.Feature.Users.Command.Models;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Infrastructure.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class UserRegisterCommandHandler
    : IRequestHandler<UserRegisterModel, string>
{
    private readonly UserManager<User> _userManager;
    private readonly IFile _file;
    private readonly IMediator _mediator;

    public UserRegisterCommandHandler(
        UserManager<User> userManager,
        IFile file,
        IMediator mediator)
    {
        _userManager = userManager;
        _file = file;
        _mediator = mediator;
    }

    public async Task<string> Handle(
        UserRegisterModel request,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return "User already exists";

        if (request.Password != request.ConfirmPassword)
            return "Passwords do not match";

        var profilePic = await _file.UploadIamge("User", request.Image);

        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Bio = request.Bio,
            CreatedAt = DateTime.UtcNow,
            ProfilePictureUrl = profilePic.ToString(),
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return errors;
        }

        await _userManager.AddToRoleAsync(user, "User");

        await _mediator.Send(
            new CreateOtpEmailCommand(user.Id, user.Email),
            cancellationToken
        );

        return "User registered successfully. OTP sent to email.";
    }
}
