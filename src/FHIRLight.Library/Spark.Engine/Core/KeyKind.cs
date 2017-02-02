namespace FHIRLight.Library.Spark.Engine.Core
{
    /// <summary>
    ///  Any <see cref="ILocalhost"/> will be triaged by an <see cref="IKey"/> as one of these.
    /// </summary>
    public enum KeyKind
    {
        /// <summary>
        /// absolute url, where base is not localhost
        /// </summary>
        Foreign,

        /// <summary>
        /// temporary id, URN, but not a URL. 
        /// </summary>
        Temporary,

        /// <summary>
        /// absolute url, but base is (any of the) localhost(s)
        /// </summary>
        Local,

        /// <summary>
        /// Relative url, for internal references
        /// </summary>
        Internal
    }
}