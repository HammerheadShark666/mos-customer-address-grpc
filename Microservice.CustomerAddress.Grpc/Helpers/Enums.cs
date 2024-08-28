namespace Microservice.CustomerAddress.Grpc.Helpers;

public class Enums
{
    public enum GrpcStatusCode
    {
        Ok,
        Cancelled,
        Unknown,
        Invalid_Argument,
        Deadline_Exceeded,
        Not_Found,
        Already_Exists,
        Permission_Denied,
        Resource_Exhausted,
        Failed_Precondition,
        Aborted,
        Out_Of_Range,
        Unimplemented,
        Internal,
        Unavailable,
        Data_Loss,
        Unauthenticated
    }
}