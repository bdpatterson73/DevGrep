// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

#if !(PORTABLE40 || NET20 || NET35)
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BLS.JSON.Utilities
{
    internal class ExpressionReflectionDelegateFactory : ReflectionDelegateFactory
    {
        private static readonly ExpressionReflectionDelegateFactory _instance =
            new ExpressionReflectionDelegateFactory();

        internal static ReflectionDelegateFactory Instance
        {
            get { return _instance; }
        }

        public override MethodCall<T, object> CreateMethodCall<T>(MethodBase method)
        {
            ValidationUtils.ArgumentNotNull(method, "method");

            Type type = typeof (object);

            ParameterExpression targetParameterExpression = Expression.Parameter(type, "target");
            ParameterExpression argsParameterExpression = Expression.Parameter(typeof (object[]), "args");

            ParameterInfo[] parametersInfo = method.GetParameters();

            Expression[] argsExpression = new Expression[parametersInfo.Length];

            for (int i = 0; i < parametersInfo.Length; i++)
            {
                Expression indexExpression = Expression.Constant(i);

                Expression paramAccessorExpression = Expression.ArrayIndex(argsParameterExpression, indexExpression);

                paramAccessorExpression = EnsureCastExpression(paramAccessorExpression, parametersInfo[i].ParameterType);

                argsExpression[i] = paramAccessorExpression;
            }

            Expression callExpression;
            if (method.IsConstructor)
            {
                callExpression = Expression.New((ConstructorInfo) method, argsExpression);
            }
            else if (method.IsStatic)
            {
                callExpression = Expression.Call((MethodInfo) method, argsExpression);
            }
            else
            {
                Expression readParameter = EnsureCastExpression(targetParameterExpression, method.DeclaringType);

                callExpression = Expression.Call(readParameter, (MethodInfo) method, argsExpression);
            }

            if (method is MethodInfo)
                callExpression = EnsureCastExpression(callExpression, type);

            LambdaExpression lambdaExpression = Expression.Lambda(typeof (MethodCall<T, object>), callExpression,
                                                                  targetParameterExpression, argsParameterExpression);

            MethodCall<T, object> compiled = (MethodCall<T, object>) lambdaExpression.Compile();
            return compiled;
        }

        public override Func<T> CreateDefaultConstructor<T>(Type type)
        {
            ValidationUtils.ArgumentNotNull(type, "type");

            // avoid error from expressions compiler because of abstract class
            if (type.IsAbstract())
                return () => (T) Activator.CreateInstance(type);

            Type resultType = typeof (T);

            Expression expression = Expression.New(type);

            expression = EnsureCastExpression(expression, resultType);

            LambdaExpression lambdaExpression = Expression.Lambda(typeof (Func<T>), expression);

            Func<T> compiled = (Func<T>) lambdaExpression.Compile();
            return compiled;
        }

        public override Func<T, object> CreateGet<T>(PropertyInfo propertyInfo)
        {
            ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");

            Type instanceType = typeof (T);
            Type resultType = typeof (object);

            ParameterExpression parameterExpression = Expression.Parameter(instanceType, "instance");
            Expression resultExpression;

            MethodInfo getMethod = propertyInfo.GetGetMethod(true);

            if (getMethod.IsStatic)
            {
                resultExpression = Expression.MakeMemberAccess(null, propertyInfo);
            }
            else
            {
                Expression readParameter = EnsureCastExpression(parameterExpression, propertyInfo.DeclaringType);

                resultExpression = Expression.MakeMemberAccess(readParameter, propertyInfo);
            }

            resultExpression = EnsureCastExpression(resultExpression, resultType);

            LambdaExpression lambdaExpression = Expression.Lambda(typeof (Func<T, object>), resultExpression,
                                                                  parameterExpression);

            Func<T, object> compiled = (Func<T, object>) lambdaExpression.Compile();
            return compiled;
        }

        public override Func<T, object> CreateGet<T>(FieldInfo fieldInfo)
        {
            ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");

            ParameterExpression sourceParameter = Expression.Parameter(typeof (T), "source");

            Expression fieldExpression;
            if (fieldInfo.IsStatic)
            {
                fieldExpression = Expression.Field(null, fieldInfo);
            }
            else
            {
                Expression sourceExpression = EnsureCastExpression(sourceParameter, fieldInfo.DeclaringType);

                fieldExpression = Expression.Field(sourceExpression, fieldInfo);
            }

            fieldExpression = EnsureCastExpression(fieldExpression, typeof (object));

            Func<T, object> compiled = Expression.Lambda<Func<T, object>>(fieldExpression, sourceParameter).Compile();
            return compiled;
        }

        public override Action<T, object> CreateSet<T>(FieldInfo fieldInfo)
        {
            ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");

            // use reflection for structs
            // expression doesn't correctly set value
            if (fieldInfo.DeclaringType.IsValueType() || fieldInfo.IsInitOnly)
                return LateBoundReflectionDelegateFactory.Instance.CreateSet<T>(fieldInfo);

            ParameterExpression sourceParameterExpression = Expression.Parameter(typeof (T), "source");
            ParameterExpression valueParameterExpression = Expression.Parameter(typeof (object), "value");

            Expression fieldExpression;
            if (fieldInfo.IsStatic)
            {
                fieldExpression = Expression.Field(null, fieldInfo);
            }
            else
            {
                Expression sourceExpression = EnsureCastExpression(sourceParameterExpression, fieldInfo.DeclaringType);

                fieldExpression = Expression.Field(sourceExpression, fieldInfo);
            }

            Expression valueExpression = EnsureCastExpression(valueParameterExpression, fieldExpression.Type);

            BinaryExpression assignExpression = Expression.Assign(fieldExpression, valueExpression);

            LambdaExpression lambdaExpression = Expression.Lambda(typeof (Action<T, object>), assignExpression,
                                                                  sourceParameterExpression, valueParameterExpression);

            Action<T, object> compiled = (Action<T, object>) lambdaExpression.Compile();
            return compiled;
        }

        public override Action<T, object> CreateSet<T>(PropertyInfo propertyInfo)
        {
            ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");

            // use reflection for structs
            // expression doesn't correctly set value
            if (propertyInfo.DeclaringType.IsValueType())
                return LateBoundReflectionDelegateFactory.Instance.CreateSet<T>(propertyInfo);

            Type instanceType = typeof (T);
            Type valueType = typeof (object);

            ParameterExpression instanceParameter = Expression.Parameter(instanceType, "instance");

            ParameterExpression valueParameter = Expression.Parameter(valueType, "value");
            Expression readValueParameter = EnsureCastExpression(valueParameter, propertyInfo.PropertyType);

            MethodInfo setMethod = propertyInfo.GetSetMethod(true);

            Expression setExpression;
            if (setMethod.IsStatic)
            {
                setExpression = Expression.Call(setMethod, readValueParameter);
            }
            else
            {
                Expression readInstanceParameter = EnsureCastExpression(instanceParameter, propertyInfo.DeclaringType);

                setExpression = Expression.Call(readInstanceParameter, setMethod, readValueParameter);
            }

            LambdaExpression lambdaExpression = Expression.Lambda(typeof (Action<T, object>), setExpression,
                                                                  instanceParameter, valueParameter);

            Action<T, object> compiled = (Action<T, object>) lambdaExpression.Compile();
            return compiled;
        }

        private Expression EnsureCastExpression(Expression expression, Type targetType)
        {
            Type expressionType = expression.Type;

            // check if a cast or conversion is required
            if (expressionType == targetType ||
                (!expressionType.IsValueType() && targetType.IsAssignableFrom(expressionType)))
                return expression;

            return Expression.Convert(expression, targetType);
        }
    }
}

#endif