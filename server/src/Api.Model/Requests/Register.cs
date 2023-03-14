namespace Api.Model.Requests;

public record RegisterUser(
    string UserName,
    string Password,
    string Email
);