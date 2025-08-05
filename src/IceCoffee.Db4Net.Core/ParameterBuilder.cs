using IceCoffee.Db4Net.Core.SqlAdapters;
using System.Collections;

namespace IceCoffee.Db4Net.Core
{
    public class ParameterBuilder
    {
        public static string ParameterNamePrefix { get; set; } = "p";
        public static bool ReuseParameters { get; set; } = true;
        private static string NullParameterName => ParameterNamePrefix + "0";

        private bool _isIncludeNullParameter;
        private Dictionary<object, string>? _namedParameterMap;
        private List<KeyValuePair<string, object?>>? _namedParameterList;
        private List<object?>? _dynamicParameters;

        public object? Entities { get; set; }

        private readonly ISqlAdapter _sqlAdapter;
        public ISqlAdapter SqlAdapter => _sqlAdapter;

        public ParameterBuilder(ISqlAdapter sqlAdapter)
        {
            _sqlAdapter = sqlAdapter;
        }

        public IReadOnlyList<KeyValuePair<string, object?>>? NamedParameters
        {
            get
            {
                if (ReuseParameters)
                {
                    List<KeyValuePair<string, object?>>? result = null;

                    if (_isIncludeNullParameter)
                    {
                        result ??= new List<KeyValuePair<string, object?>>();
                        result.Add(new KeyValuePair<string, object?>(NullParameterName, null));
                    }

                    if (_namedParameterMap != null)
                    {
                        result ??= new List<KeyValuePair<string, object?>>();
                        foreach (var item in _namedParameterMap)
                        {
                            result.Add(new KeyValuePair<string, object?>(item.Value, item.Key));
                        }
                    }

                    return result;
                }
                else
                {
                    return _namedParameterList;
                }
            }
        }
        public IReadOnlyList<object?>? DynamicParameters => _dynamicParameters;

        public string AddNamedParam(object? value)
        {
            if (ReuseParameters)
            {
                if (value == null)
                {
                    _isIncludeNullParameter = true;
                    return _sqlAdapter.Parameter(NullParameterName);
                }

                _namedParameterMap ??= new Dictionary<object, string>();

                // P1, P2, ... etc.
                if (_namedParameterMap.TryGetValue(value, out string? parameterName) == false)
                {
                    parameterName = ParameterNamePrefix + (_namedParameterMap.Count + 1);
                    _namedParameterMap.Add(value, parameterName);
                }

                // @P1, @P2, ... etc.
                return _sqlAdapter.Parameter(parameterName);
            }
            else
            {
                _namedParameterList ??= new List<KeyValuePair<string, object?>>();
                string parameterName = ParameterNamePrefix + _namedParameterList.Count;
                _namedParameterList.Add(new KeyValuePair<string, object?>(parameterName, value));
                return _sqlAdapter.Parameter(parameterName);
            }
        }

        public void AddDynamicParams(object? param)
        {
            if (param == null)
            {
                return;
            }
            _dynamicParameters ??= new List<object?>();

            if (param is IEnumerable objects)
            {
                foreach (var item in objects)
                {
                    _dynamicParameters.Add(item);
                }
            }
            else
            {
                _dynamicParameters.Add(param);
            }
        }
    }
}
