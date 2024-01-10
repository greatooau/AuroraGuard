using System.Data;
using Dapper;

namespace AuroraGuard.DataAccess.TypeHandlers;

public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid guid)
    {
        parameter.Value = guid.ToString();
    }

    public override Guid Parse(object value) => Guid.Parse((string)value);
}