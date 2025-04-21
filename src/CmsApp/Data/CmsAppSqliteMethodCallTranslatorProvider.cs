using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Sqlite.Query.Internal;
using System.Reflection;
using Volo.Abp;

namespace CmsApp.Data
{
    public class CmsAppSqliteMethodCallTranslatorProvider : SqliteMethodCallTranslatorProvider
    {
        public CmsAppSqliteMethodCallTranslatorProvider(
            [NotNull] RelationalMethodCallTranslatorProviderDependencies dependencies)
            : base(dependencies)
        {
            var sqlExpressionFactory = dependencies.SqlExpressionFactory;

            AddTranslators(
                new IMethodCallTranslator[]
                {
                    new SqliteMathTranslator(sqlExpressionFactory),
                    new CmsAppSqliteStringMethodTranslator(sqlExpressionFactory)
                });
        }
    }

    public class CmsAppSqliteStringMethodTranslator : SqliteStringMethodTranslator
    {
        private static readonly MethodInfo _containsMethodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.Contains), new[] { typeof(string) });

        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        public CmsAppSqliteStringMethodTranslator(ISqlExpressionFactory sqlExpressionFactory) : base(sqlExpressionFactory)
        {
            _sqlExpressionFactory = sqlExpressionFactory;
        }

        public override SqlExpression Translate(SqlExpression instance, MethodInfo method,
            IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            Check.NotNull(method, nameof(method));
            Check.NotNull(arguments, nameof(arguments));

            if (_containsMethodInfo.Equals(method))
            {
                var pattern = arguments[0];
                var stringTypeMapping = ExpressionExtensions.InferTypeMapping(instance, pattern);

                instance = _sqlExpressionFactory.ApplyTypeMapping(instance, stringTypeMapping);
                pattern = _sqlExpressionFactory.ApplyTypeMapping(pattern, stringTypeMapping);

                // this basically changes query to "instr(upper(left_expression), upper(right_expression))"
                return _sqlExpressionFactory.OrElse(
                    _sqlExpressionFactory.Equal(
                        pattern,
                        _sqlExpressionFactory.Constant(string.Empty, stringTypeMapping)),
                    _sqlExpressionFactory.GreaterThan(
                        _sqlExpressionFactory.Function(
                            "instr",
                            new[]
                            {
                                _sqlExpressionFactory.Function("upper", new[] { instance }, false, new[] { false },
                                    typeof(string)),
                                _sqlExpressionFactory.Function("upper", new[] { pattern }, false, new[] { false },
                                    typeof(string))
                            },
                            false,
                            new[] { false, false },
                            typeof(int)), _sqlExpressionFactory.Constant(0)));
            }

            return base.Translate(instance, method, arguments, logger);
        }
    }
}