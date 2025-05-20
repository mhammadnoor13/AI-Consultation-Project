using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared;

public readonly record struct Result <T>
{
    public T Value { get; }
    public string? Error { get; }

    private Result(T value) { Value = value;}
    private Result(string error) { Error = error;}

    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;

    // Factory Helpers
    public static Result<T> Success (T value) => new (value);
    public static Result<T> Failure (string message) => new (message);


    // Helpers -- Find only the first Error 
    public Result<K> Bind<K>(Func<T, Result<K>> next)
    => IsFailure ? Result<K>.Failure(Error!) : next(Value);

}
