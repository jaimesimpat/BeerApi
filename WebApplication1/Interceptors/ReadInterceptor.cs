using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WebApplication1.Interceptors;

public class ReadInterceptor : DbCommandInterceptor
{
    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"==== HERE STARTS ====");
        return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }

    public override ValueTask<InterceptionResult> DataReaderClosingAsync(DbCommand command, DataReaderClosingEventData eventData, InterceptionResult result)
    {
        System.Console.WriteLine($"==== HERE ENDS ====");
        return base.DataReaderClosingAsync(command, eventData, result);
    }
}
