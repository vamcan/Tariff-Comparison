namespace TariffComparison.Web.Api.Base.Utility
{
    public static class ReflectionHelper
    {
        public static bool IsAssignableFromBaseTypeGeneric(this object obj, Type type)
        {
            return obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(type);
        }
    }
}
