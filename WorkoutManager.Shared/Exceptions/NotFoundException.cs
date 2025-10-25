﻿namespace WorkoutManager.Shared.Exceptions;

/// <summary>
/// Represents errors that occur when a requested resource is not found.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException()
        : base("The requested resource was not found.")
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }
}