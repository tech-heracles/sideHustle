namespace AlphaWeb.Core.Common
{
   public class Casting
    {
        public static TGeneric CastConcreteToGeneric<TGeneric, TConcret>(TConcret inputType)
        {
            //TODO this one need to be tested very carefully 
            return (TGeneric)(inputType as object);
        }
    }
}
