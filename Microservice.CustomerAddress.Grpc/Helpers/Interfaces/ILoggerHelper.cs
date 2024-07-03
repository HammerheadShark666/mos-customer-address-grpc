﻿namespace Microservice.Customer.Address.Grpc.Helpers.Interfaces;

public interface ILoggerHelper<T>
{
    public void LogMessage(string message);
    public void LogErrorMessage(string error, Guid id); 
}