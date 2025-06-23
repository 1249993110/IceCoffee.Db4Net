namespace IceCoffee.Db4Net.Core.SqlBuilders
{
    public interface ISqlBuilder
    {
        SqlResult SqlResult { get; }
        internal string DatabaseName { get; }
    }
}
