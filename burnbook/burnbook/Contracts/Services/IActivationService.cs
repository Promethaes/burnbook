namespace burnbook.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
